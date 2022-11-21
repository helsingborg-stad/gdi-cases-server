using System.Text.Json;
using System.Text.Json.Serialization;
using gdi_cases_server.Converters;
using gdi_cases_server.Modules.Cases.Models;

namespace gdi_cases_server.tests;
[TestClass]
public class RelaxedEnumConverterTests {
    [JsonConverter(typeof(RelaxedEnumConverter<TestEnum>))]
    public enum TestEnum
    {
        First = 0,
        Second = 1,
        Third = 3
    }

    public class TestModel {
        [JsonConverter(typeof(RelaxedEnumConverter<TestEnum>))]
        public TestEnum Value { get; set; } = TestEnum.First;
    }

    [TestMethod]
    public void DefaultEnumMemberIsChosenWhenInDoubt ()
    {
        Assert.AreEqual(TestEnum.First, ToAndFromJSON<TestModel>(new { })?.Value);
        Assert.AreEqual(TestEnum.First, ToAndFromJSON<TestModel>(new { Value = "something weird" })?.Value);
    }

    [TestMethod]
    public void ParsingIsCaseInsesitive()
    {
        Assert.AreEqual(TestEnum.Second, ToAndFromJSON<TestModel>(new { Value = "sEconD"})?.Value);
        Assert.AreEqual(TestEnum.Third, ToAndFromJSON<TestModel>((new { Value = "third" }))?.Value);
    }

    [TestMethod]
    public void ValuesAreWrittenInLowerCase()
    {
        Assert.AreEqual("first", ToAndFromJSON<Dictionary<string, string>>(new TestModel { })?["Value"]);
        Assert.AreEqual("second", ToAndFromJSON<Dictionary<string, string>>(new TestModel { Value = TestEnum.Second })?["Value"]);
        Assert.AreEqual("third", ToAndFromJSON<Dictionary<string, string>>(new TestModel { Value = TestEnum.Third })?["Value"]);
    }

    private static string ToJSON(object v) => JsonSerializer.Serialize(v);
    private static T? FromJSON<T>(string json) => JsonSerializer.Deserialize<T>(json);
    private static T? ToAndFromJSON<T>(object v) => FromJSON<T>(ToJSON(v));
}
