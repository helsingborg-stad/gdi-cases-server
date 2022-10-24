using gdi_cases_server.Modules.Cases.MongoDb;

namespace gdi_cases_server.tests;

[TestClass]
public class MongoDbCasesDatabaseTests: NiceToHaveTestBase
{
    [TestMethod]
    public void CreateCaseRecordsCreateFieldValuesFromBundleAndCase ()
    {
        const int casesCount = 2;
        // Create a bundle with wellknown values in props
        var testBundle = CreateTestCaseBundle(casesCount);

        // sanity test
        Assert.AreEqual(casesCount, testBundle.Cases.Count());

        // asert in a loop over every case
        testBundle.Cases.Select((c, i) =>
        {
            var record = new MongoDbCasesDatabase(null).CreateCaseRecord(testBundle, c);

            // We assert that the record is a simple envlope around the case with...

            // ...suitable composite key
            Assert.AreEqual($"test-publisher:test-systemid:test-case-{i}", record.RecordId);
            // ...and some some props copied
            Assert.AreEqual(c.SubjectId, record.SubjectId);
            Assert.AreSame(c, record.Case);

            return true;
        }).ToList();
    }
}
