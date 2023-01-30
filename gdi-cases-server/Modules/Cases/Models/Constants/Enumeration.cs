using System;
using System.ComponentModel;
using System.Reflection;

namespace gdi_cases_server.Modules.Cases.Models.Constants;

public class Enumeration
{
	public string Value { get; set; } = "";
	public string Description { get; set; } = "";

	public static List<Enumeration> FromEnum<TEnum>() where TEnum : struct, Enum => (
            from  name in typeof(TEnum).GetEnumNames()
		let field = typeof(TEnum).GetField(name)
            let description = (
                from attribute in field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                select (attribute as DescriptionAttribute).Description
                ).FirstOrDefault()
            select new Enumeration {
			Value = name,
			Description = description ?? ""
		}
		).ToList();
}

