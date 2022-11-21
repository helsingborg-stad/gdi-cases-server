using System.Net.NetworkInformation;
using gdi_cases_server.Modules.Cases.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace gdi_cases_server.Modules.Cases.Controllers;

[ApiController]
[Route("/api/v1/sample-cases")]
public class SampleCasesController: Controller {
    const int MinEvents = 1;
    const int MaxEvents = 12;
    const int MinCases = 2;
    const int MaxCases = 12;

    static readonly string[] CaseTypes = new[] {
        "Ansökan om bygglov",
        "Anmälan om ändrade förhållanden"
    };

    static readonly string[] StatusHintDistribution = WeightedValues(
        new Dictionary<string, int> {
            {
                "", 2
            },
            {
                StatusHint.Approved.ToString(), 10
            },
            {
                StatusHint.Incomplete.ToString(), 2
            },
            {
                StatusHint.Rejected.ToString(), 2
            },
            {
                StatusHint.Closed.ToString(), 5
            }
        });

    private static T[] WeightedValues<T>(Dictionary<T, int> relativeFrequencies) where T: notnull
    {
        return (
            from kv in relativeFrequencies
            let value = kv.Key
            let freq = kv.Value
            from f in Enumerable.Range(0, freq)
            select value
            ).ToArray();
    }

    [HttpGet("generate-sample-bundle", Name = "generateSampleBundleOperation")]
    public CasesBundle GenerateSampleBundle(string subjectId, int randomSeed = 0)
    {
        return new CasesBundle
        {
            PublisherId = "gdi-cases-server",
            SystemId = "samples",
            Cases = GenerateSampleCases(subjectId, new Random(randomSeed))
        };
    }

    private IEnumerable<Case> GenerateSampleCases(string subjectId, Random random)
    {
        var beginningOfTime = DateTime.Now.AddYears(-2);
        return from caseDate in GetRandomTimes(random, MinCases, MaxCases, beginningOfTime, DateTime.Now)
               let label = GetRandomElement(CaseTypes, random)
               
               let events = (from eventDate in GetRandomTimes(random, MinEvents, MaxEvents, beginningOfTime, caseDate)
                             let statusHint = GetRandomElement(StatusHintDistribution, random)
                            select new CaseEvent
                            {
                                UpdateTime = FormatDate(eventDate),
                                Label = $"Handläggarens notering {FormatDate(eventDate)}",
                                Description = $"Handläggarens notering {FormatDate(eventDate)}",
                                StatusHint = statusHint,
                                Status = statusHint,
                                Actions = from i in Enumerable.Range(0, random.Next(2))
                                          select new CaseAction
                                          {
                                              Label = $"E-tjänst {i}",
                                              Url = "https://www.example.com",
                                              TypeHint = ActionTypeHint.Link.ToString()
                                          }
                            })
                            .ToList()
               select new Case
               {
                   CaseId = $"case-{random.Next(1001, 9999)}",
                   SubjectId = subjectId,
                   // PublisherStatus = status,
                   Status = events.Last()?.Status,
                   StatusHint = events.Last()?.StatusHint,
                   UpdateTime = FormatDate(caseDate),
                   Label = label,
                   Description = label,
                   Events = events,
                   Actions = from i in Enumerable.Range(0, random.Next(2))
                             select new CaseAction
                             {
                                 Label = $"Visa i E-tjänsten ({i+1})",
                                 Url = "https://www.example.com",
                                 TypeHint = ActionTypeHint.Link.ToString()
                             }
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
