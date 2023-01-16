using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;

namespace gdi_cases_server.Modules.Cases.Models
{
    public static class JsonDto
    {
        public static CasesBundleJsonDto FromXmlDto(Bundle bundle) => new CasesBundleJsonDto
        {
            Cases = ToList(bundle.Cases, FromXmlDto)
        };

        public static CaseJsonDto FromXmlDto(CaseXmlDto c) => new CaseJsonDto
        {
            Actions = ToList(c.Actions, FromXmlDto),
            Events = ToList(c.Events, FromXmlDto),
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

        public static CaseActionJsonDto FromXmlDto(CaseActionXmlDto c) => new CaseActionJsonDto()
        {
            Label = c.Label,
            Url = c.Url,
            TypeHint = c.TypeHint
        };

        public static CaseEventJsonDto FromXmlDto(CaseEventXmlDto c) => new CaseEventJsonDto()
        {
            Actions = ToList(c.Actions, FromXmlDto),
            UpdateTime = c.UpdateTime,
            Label = c.Label,
            Description = c.Description,
            Status = c.Status,
            StatusHint = c.StatusHint
        };

        private static List<TDest> ToList<TSource, TDest>(List<TSource>? e, Func<TSource, TDest> map)
        {
            return (e ?? Enumerable.Empty<TSource>()).Select(map).ToList();
        }
    }
}

