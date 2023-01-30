using System.Text.Json;
using System.Xml.Serialization;
using gdi_cases_server.Modules.Cases.Models;
using gdi_cases_server.Modules.Cases.Models.Cases;

namespace gdi_cases_server.tests.Models;

public class ModelTestBase: NiceToHaveTestBase {
    public static Bundle GenerateSampleBundle(string subject = "test-subject", int randomSeed = 0) => new SampleDataGenerator().GenerateSampleBundle(subject, randomSeed);
}
