using gdi_cases_server.Modules.Cases.Models.Validators;

namespace gdi_cases_server.tests.Models.Validators;

[TestClass]
public class IdAttributeTests
{
    [DataRow("")]
    [DataRow(" 123 ")]
    [DataRow("    ")]
    [DataRow(" \t\r\na   ")]
    [DataRow(null)]
    [DataTestMethod]
    public void InvalidValue(object? value)
    {
        Assert.IsFalse(new IdAttribute().IsValid(value));
    }

    [DataRow("123-456")]
    [DataRow("abc#def")]
    [DataRow("x-y-z")]
    [DataRow("p/q&r")]
    [DataTestMethod]
    public void ValidValue(object? value)
    {
        Assert.IsTrue(new IdAttribute().IsValid(value));
    }

}

