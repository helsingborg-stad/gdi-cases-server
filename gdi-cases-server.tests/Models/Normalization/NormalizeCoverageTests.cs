using DeepEqual.Syntax;
using gdi_cases_server.Modules.Cases.Models.Cases;
using gdi_cases_server.Modules.Cases.Models.Normalization;
using gdi_cases_server.tests.TestUtilities.Samples;

namespace gdi_cases_server.tests.Models.Normalization;

[TestClass]
public class NormalizeCoverageTests: NormalizationTestBase
{
    public ISampleInstanceProvider CreateSampleInstanceProvider() => CreateSampleInstanceProvider((s, pi, p) => pi.Name);

    public ISampleInstanceProvider CreateSampleInstanceProvider(PropertyValueMappingFunc<string> mapString) =>
        // Define rules for all occursing property types
        SampleInstance.Create()
            // each string prop is give its name as value
            .Map<string>(mapString)
            // foreach boolean is given non default value true
            .Map<Boolean>((s, pi, p) => true)
            // each list has 3 elements
            .Map<List<Case>>((cases, pi, p) => Enumerable.Range(0, 3).Select(i => p.Map<Case>()).ToList())
            .Map<List<Event>>((cases, pi, p) => Enumerable.Range(0, 3).Select(i => p.Map<Event>()).ToList())
            .Map<List<Modules.Cases.Models.Cases.Action>>((cases, pi, p) => Enumerable.Range(0, 3).Select(i => p.Map<Modules.Cases.Models.Cases.Action>()).ToList())
            .Build();

    [TestMethod]
    public void SampleDataIsGemeratedAsExpected()
    {
        var samples = CreateSampleInstanceProvider();

        // The call to Map will fail if any type is not covered in the mapping above
        var bundle = samples.Map<Bundle>();

        // Check that a bundle has 3 cases, each with 3 actions and 3 events which the has 3 actions
        // We only a subset of the literal properties, since they shuld cover the whole

        Assert.IsNotNull(bundle);
        Assert.AreEqual(3, bundle.Cases.Count);

        bundle.Cases.ForEach(c =>
        {
            Assert.AreEqual("PublisherId", c.PublisherId);
            Assert.AreEqual("SystemId", c.SystemId);
            Assert.AreEqual("CaseId", c.CaseId);
            Assert.AreEqual("SubjectId", c.SubjectId);
            Assert.AreEqual("UpdateTime", c.UpdateTime);
            Assert.AreEqual("Label", c.Label);
            Assert.AreEqual("Description", c.Description);
            Assert.AreEqual("Status", c.Status);
            Assert.AreEqual("StatusHint", c.StatusHint);
            Assert.IsTrue(c.IsDeleted);

            Assert.AreEqual(3, c.Actions.Count);
            c.Actions.ForEach(a => {
                Assert.AreEqual("Label", a.Label);
                Assert.AreEqual("Url", a.Url);
                Assert.AreEqual("TypeHint", a.TypeHint);
            });

            Assert.AreEqual(3, c.Events.Count);
            c.Events.ForEach(e =>
            {
                Assert.AreEqual("UpdateTime", e.UpdateTime);
                Assert.AreEqual("Label", e.Label);
                Assert.AreEqual("Description", e.Description);
                Assert.AreEqual("Status", e.Status);
                Assert.AreEqual("StatusHint", e.StatusHint);

                Assert.AreEqual(3, c.Actions.Count);
                e.Actions.ForEach(a => {
                    Assert.AreEqual("Label", a.Label);
                    Assert.AreEqual("Url", a.Url);
                    Assert.AreEqual("TypeHint", a.TypeHint);
                });
            });
        });
    }

    [TestMethod]
    public void AllPropertiesInBundleAndContainedClassesAreSubjectToNormalization()
    {
        // This test is expected to fail when a new property is added
        // which is then NOT handled in normalization

        // Define rules for all occuring property types
        var samples = CreateSampleInstanceProvider(
            mapString: (s, pi, p) => pi.Name.Contains("Hint") ? "" : pi.Name);

        // The call to Map will fail if any type is not covered in the mapping above
        var bundle = samples.Map<Bundle>();

        Assert.IsTrue(bundle.IsDeepEqual(bundle.Normalize()));
        Assert.AreEqual(ToXml(bundle), ToXml(bundle.Normalize()));
        Assert.AreEqual(ToJson(bundle), ToJson(bundle.Normalize()));
    }

    [TestMethod]
    public void AllPropertiesAreTrimmed()
    {
        // This test is expected to fail when a new property is added
        // which is then NOT handled in normalization

        // Define rules for all occuring property types
        var whitespacedSamples = CreateSampleInstanceProvider(
            mapString: (s, pi, p) => " \t\n\r " + (pi.Name.Contains("Hint") ? "" : pi.Name) + " \t\n\r ");

        var samples = CreateSampleInstanceProvider(
            mapString: (s, pi, p) => pi.Name.Contains("Hint") ? "" : pi.Name);

        // The call to Map will fail if any type is not covered in the mapping above
        var wsBundle = whitespacedSamples.Map<Bundle>();
        var normalizedBundle = samples.Map<Bundle>();

        Assert.AreEqual(
            ToXml(normalizedBundle),
            ToXml(wsBundle.Normalize()));
        Assert.AreEqual(
            ToJson(normalizedBundle),
            ToJson(wsBundle.Normalize()));
    }
}

