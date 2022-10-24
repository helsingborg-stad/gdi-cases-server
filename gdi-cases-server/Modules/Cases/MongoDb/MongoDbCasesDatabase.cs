using gdi_cases_server.Modules.Cases.Models;
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

    public MongoDbCaseRecord CreateCaseRecord(CasesBundle bundle, Case c) => new MongoDbCaseRecord
    {
        RecordId = $"{bundle.PublisherId}:{bundle.SystemId}:{c.CaseId}",
        SubjectId = c.SubjectId,
        Case = c,
        UpdateTime = DateTime.Now
    };

    public void UpdateCases(CasesBundle bundle)
    {
        var caseRecords = bundle.Cases.Select(c => CreateCaseRecord(bundle, c));

        Collection.BulkWrite(
            from caseRecord in caseRecords
            let writeModel = new ReplaceOneModel<MongoDbCaseRecord>(
                Builders<MongoDbCaseRecord>.Filter.Where(rec => rec.RecordId == caseRecord.RecordId),
                caseRecord)
            { IsUpsert = true }
            select writeModel);
    }
}
