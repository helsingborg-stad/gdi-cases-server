using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;
using MongoDB.Driver;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using System.Text;
using System.Security.Cryptography;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbCasesDatabase : ICasesDatabase
{
    private static T With<T>(T obj, params Action<T>[] setters)
    {
        foreach (var setter in setters)
        {
            setter(obj);
        }
        return obj;
    }

    public MongoDbSession Session { get; }
    public IMongoCollection<MongoDbCaseRecord> Collection => Session.Collection;

    public MongoDbCasesDatabase(MongoDbSession session)
    {
        Session = session;
    }

    public MongoDbCaseRecord CreateCaseRecord(Bundle bundle, Case c) => new MongoDbCaseRecord
    {
        RecordId = $"{c.PublisherId}:{c.SystemId}:{c.CaseId}",
        SubjectId = c.SubjectId,
        Case = c,
        UpdateTime = DateTime.Now,

        ContentHash = CreateCaseHash(c)
    };

    private string CreateCaseHash(Case c)
    {
        using(var algo = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(string.Join('@', c.EnumerateHashValues()));
            var hash = algo.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }

    public IEnumerable<AnnotatedCase> ListCasesBySubject(string subjectId)
    {
        return (
            from r in Collection
                .Find<MongoDbCaseRecord>(r => r.SubjectId == subjectId)
                .ToEnumerable<MongoDbCaseRecord>()
            let c = r.Case
            where c != null
            let ac = With(
                c.Normalize<Case, AnnotatedCase>(),
                c => c.IsMarkedAsRead = r.ContentHash == r.LastReadContentHash,
                c => c.RecordId = r.RecordId)
            select ac)
               .ToList();
    }

    public void UpdateCases(Bundle bundle)
    {
        Collection.BulkWrite(
            from c in bundle.Cases
            let r = CreateCaseRecord(bundle, c)
            let filter = Builders<MongoDbCaseRecord>.Filter.Where(rec => rec.RecordId == r.RecordId)
            let writeModel = c.IsDeleted
                ? new DeleteOneModel<MongoDbCaseRecord>(filter) as WriteModel<MongoDbCaseRecord>
                //: new ReplaceOneModel<MongoDbCaseRecord>(filter, record) { IsUpsert = true }
                : new UpdateOneModel<MongoDbCaseRecord>(
                    filter,
                    Builders<MongoDbCaseRecord>.Update
                        .Set(r => r.RecordId, r.RecordId)
                        .Set(r => r.SubjectId, r.SubjectId)
                        .Set(r => r.SchemaVersion, r.SchemaVersion)
                        .Set(r => r.UpdateTime, r.UpdateTime)
                        .Set(r => r.Case, r.Case)
                        .Set(r => r.ContentHash, r.ContentHash)
                        // we explicitly exclude visited fields
                    )
                { IsUpsert = true }
                
            select writeModel
            );
    }

    public void MarkCaseAsRead(string recordId)
    {
        var filter = Builders<MongoDbCaseRecord>.Filter.Where(rec => rec.RecordId == recordId);
        var set = new BsonDocument("$set",
            new BsonDocument("lastReadContentHash", new BsonString("$contentHash")));
        var pipeline = PipelineDefinition<MongoDbCaseRecord, MongoDbCaseRecord>.Create(set);
        Collection.UpdateOne(filter, pipeline);
    }

    public CasesStats GetStatsBySubject(string subjectId)
    {
        var pipeline = new EmptyPipelineDefinition<MongoDbCaseRecord>()
            .Match(c => c.SubjectId == subjectId)
            .Project(c => new StatsProjection { IsMarkedAsRead = c.ContentHash == c.LastReadContentHash });

        var stats = Collection.Aggregate(pipeline).ToList();

        return new CasesStats
        {
            Count = stats.Count,
            MarkedAsReadCount = stats.Where(r => r.IsMarkedAsRead).Count()
        };
    }

    public class StatsProjection
    {
        [BsonElement("isMarkedAsRead")]
        public bool IsMarkedAsRead { get; set; } = false;
    }
}
