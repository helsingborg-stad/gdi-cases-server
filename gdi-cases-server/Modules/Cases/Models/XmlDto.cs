using System;
using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;

namespace gdi_cases_server.Modules.Cases.Models
{
    public static class XmlDto
	{
		public static CasesBundleXmlDto FromJsonDto(Bundle bundle) => new CasesBundleXmlDto
			{
				Cases = ToList(bundle.Cases, FromJsonDto)
			};
		
		public static CaseXmlDto FromJsonDto(Case c) => new CaseXmlDto
			{
				Actions = ToList(c.Actions, FromJsonDto),
				Events = ToList(c.Events, FromJsonDto),
				PublisherId = c.PublisherId,
				SystemId = c.SystemId,
				CaseId = c.CaseId,
				SubjectId = c.SubjectId,
				UpdateTime = c.UpdateTime,
				Label = c.Label,
				Description = c.Description,
				Status = c.Status,
				StatusHint = c.StatusHint,
				IsDeleted = c.IsDeleted
            };

        public static CaseActionXmlDto FromJsonDto(Json.Action c) => new CaseActionXmlDto() {
			Label = c.Label,
			Url = c.Url,
			TypeHint = c.TypeHint
		};

		public static CaseEventXmlDto FromJsonDto(Event c) => new CaseEventXmlDto() {
			Actions = ToList(c.Actions, FromJsonDto),
			UpdateTime = c.UpdateTime,
			Label = c.Label,
			Description = c.Description,
			Status = c.Status,
			StatusHint = c.StatusHint
		};

        private static List<TDest> ToList<TSource, TDest>(IEnumerable<TSource>? e, Func<TSource, TDest> map)
        {
			return (e ?? Enumerable.Empty<TSource>()).Select(map).ToList();
        }
    }
}

