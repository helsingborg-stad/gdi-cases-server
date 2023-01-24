using System.Text.Json;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;

namespace gdi_cases_server.tests.Models;

[TestClass]
public class StringValuesFromEnumConverterTests: ModelTestBase
{
    public enum TestEnum
    {
        First = 0,
        Second = 1,
        Third = 3,
        TheFourth = 4
    }
    public class TestClass
    {
        [JsonConverter(typeof(StringValuesFromEnumConverter<TestEnum>))]
        public string? Value { get; set; }
    }

    [TestMethod]
    public void ValuesAreSerializedAsLowerCase () {
        Assert.AreEqual("first", ToAndFromJSON<TestClass>(new TestClass { Value = "FIRST" })?.Value);
    }

    [TestMethod]
    public void UnknownNamesAreMappedToNull()
    {
        Assert.AreEqual("", ToAndFromJSON<TestClass>(new TestClass { Value = "missing" })?.Value);
    }
}
