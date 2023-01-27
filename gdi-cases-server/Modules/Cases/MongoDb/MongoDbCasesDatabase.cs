using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;
using MongoDB.Driver;

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
        UpdateTime = DateTime.Now
    };

    public IEnumerable<Case> ListCasesBySubject(string subjectId)
    {
        return (
            from record in Collection.Find<MongoDbCaseRecord>(record => record.SubjectId == subjectId).ToEnumerable<MongoDbCaseRecord>()
            select record.Case)
               .ToList();
    }

    public void UpdateCases(Bundle bundle)
    {
        Collection.BulkWrite(
            from c in bundle.Cases
            let record = CreateCaseRecord(bundle, c)
            let filter = Builders<MongoDbCaseRecord>.Filter.Where(rec => rec.RecordId == record.RecordId)
            let writeModel = c.IsDeleted
                ? new DeleteOneModel<MongoDbCaseRecord>(filter) as WriteModel<MongoDbCaseRecord>
                : new ReplaceOneModel<MongoDbCaseRecord>(filter, record) { IsUpsert = true }
            select writeModel
            );
    }
}
