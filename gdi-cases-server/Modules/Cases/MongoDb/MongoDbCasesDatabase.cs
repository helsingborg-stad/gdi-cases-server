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

    public void UpdateCases(CasesBundle bundle)
    {
        var caseRecords = from c in bundle.Cases
                          select new MongoDbCaseRecord
                          {
                              RecordId = $"{bundle.PublisherId}:{bundle.SystemId}:{c.CaseId}",
                              SubjectId = c.SubjectId,
                              Case = c
                          };

        Collection.BulkWrite(
            from caseRecord in caseRecords
            let writeModel = new ReplaceOneModel<MongoDbCaseRecord>(
                Builders<MongoDbCaseRecord>.Filter.Where(rec => rec.RecordId == caseRecord.RecordId),
                caseRecord)
            { IsUpsert = true }
            select writeModel);
    }
}

