using gdi_cases_server.Modules.Cases.Models.Json;
using gdi_cases_server.Modules.Cases.Models.Xml;

namespace gdi_cases_server.Modules.Cases.Models
{
    public static class JsonDto
    {
        public static Bundle FromXmlDto(CasesBundleXmlDto bundle) => new Bundle
        {
            Cases = ToList(bundle.Cases, FromXmlDto)
        };

        public static Case FromXmlDto(CaseXmlDto c) => new Case
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

        public static Json.Action FromXmlDto(CaseActionXmlDto c) => new Json.Action()
        {
            Label = c.Label,
            Url = c.Url,
            TypeHint = c.TypeHint
        };

        public static Event FromXmlDto(CaseEventXmlDto c) => new Event()
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

