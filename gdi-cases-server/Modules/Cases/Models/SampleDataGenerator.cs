using gdi_cases_server.Modules.Cases.Models.Json;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace gdi_cases_server.Modules.Cases.Models
{
    public class SampleDataGenerator
    {
        const int MinEvents = 1;
        const int MaxEvents = 12;
        const int MinCases = 2;
        const int MaxCases = 12;

        static readonly string[] CaseTypes = new[] {
            "Ansökan om bygglov",
            "Anmälan om ändrade förhållanden",
            "Resebidrag"
        };

        static readonly string[] StatusHintDistribution = WeightedValues(
            new Dictionary<string, int> {
            {
                "", 2
            },
            {
                StatusHint.approved.ToString(), 10
            },
            {
                StatusHint.incomplete.ToString(), 2
            },
            {
                StatusHint.rejected.ToString(), 2
            },
            {
                StatusHint.closed.ToString(), 5
            }
            });

        private static T[] WeightedValues<T>(Dictionary<T, int> relativeFrequencies) where T : notnull
        {
            return (
                from kv in relativeFrequencies
                let value = kv.Key
                let freq = kv.Value
                from f in Enumerable.Range(0, freq)
                select value
                ).ToArray();
        }

        public CasesBundleJsonDto GenerateSampleBundle(string subjectId, int randomSeed = 0)
        {
            return new CasesBundleJsonDto
            {
                Cases = GenerateSampleCases(subjectId, new Random(randomSeed)).ToList()
            };
        }

        private IEnumerable<CaseJsonDto> GenerateSampleCases(string subjectId, Random random)
        {
            var beginningOfTime = DateTime.Now.AddYears(-2);
            return from caseDate in GetRandomTimes(random, MinCases, MaxCases, beginningOfTime, DateTime.Now)
                   let label = GetRandomElement(CaseTypes, random)

                   let events = (from eventDate in GetRandomTimes(random, MinEvents, MaxEvents, beginningOfTime, caseDate)
                                 let statusHint = GetRandomElement(StatusHintDistribution, random) ?? ""
                                 select new CaseEventJsonDto
                                 {
                                     UpdateTime = FormatDate(eventDate),
                                     Label = $"Handläggarens notering {FormatDate(eventDate)}",
                                     Description = $"Handläggarens notering {FormatDate(eventDate)}",
                                     StatusHint = statusHint,
                                     Status = statusHint,
                                     Actions = (from i in Enumerable.Range(0, random.Next(2))
                                               select new CaseActionJsonDto
                                               {
                                                   Label = $"E-tjänst {i}",
                                                   Url = "https://www.example.com",
                                                   TypeHint = ActionTypeHint.link.ToString()
                                               }).ToList()
                                 })
                                .ToList()
                   select new CaseJsonDto
                   {
                       PublisherId = "gdi-cases-server",
                       SystemId = "samples",
                       CaseId = $"case-{random.Next(1001, 9999)}",
                       SubjectId = subjectId,
                       // PublisherStatus = status,
                       Status = events.Last()?.Status ?? "",
                       StatusHint = events.Last()?.StatusHint ?? "",
                       UpdateTime = FormatDate(caseDate),
                       Label = label,
                       Description = label,
                       Events = events,
                       Actions = (from i in Enumerable.Range(0, random.Next(2))
                                 select new CaseActionJsonDto
                                 {
                                     Label = $"Visa i E-tjänsten ({i + 1})",
                                     Url = "https://www.example.com",
                                     TypeHint = ActionTypeHint.link.ToString()
                                 }).ToList()
                   };
        }

        private string FormatDate(DateTime caseDate)
        {
            return caseDate.ToString("yyyy-MM-dd");
        }

        private T GetRandomElement<T>(T[] elements, Random random)
        {
            return elements[random.Next() % elements.Length];
        }

        private IEnumerable<DateTime> GetRandomTimes(Random random, int minCount, int maxCount, DateTime minDate, DateTime maxDate)
        {
            return Enumerable.Range(0, random.Next(minCount, maxCount))
                .Select(index => minDate.AddDays(random.Next(0, (maxDate - minDate).Days)))
                .OrderBy(date => date);
        }
    }
}

