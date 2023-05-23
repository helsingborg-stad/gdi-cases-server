using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;
using MongoDB.Driver;
using gdi_cases_server.Modules.Cases.Models.Normalization;

namespace gdi_cases_server.Modules.Cases.MongoDb;

public class MongoDbCasesDatabase : ICasesDatabase
{
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
        IsMarkedAsRead = false
    };

    public IEnumerable<AnnotatedCase> ListCasesBySubject(string subjectId)
    {
        return (
            from record in Collection.Find<MongoDbCaseRecord>(record => record.SubjectId == subjectId).ToEnumerable<MongoDbCaseRecord>()
            let c = record.Case
            where c != null
            let ac = c.Normalize<Case, AnnotatedCase>()
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
                        // we explicitly exclude visited fields
                    )
                { IsUpsert = true }
                
            select writeModel
            );
    }
}
