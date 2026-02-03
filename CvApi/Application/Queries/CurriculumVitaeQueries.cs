using System.Text.Json;
using Microsoft.Extensions.AI;

namespace CvApi.Queries;

public interface ICurriculumVitaeQueries
{
    public Task<CurriculumVitae[]> GetAll(CancellationToken cancellationToken);
}

public class CurriculumVitaeQueries(IChatClient chatClient) : ICurriculumVitaeQueries
{
    public async Task< CurriculumVitae[]> GetAll(CancellationToken cancellationToken)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "cv.json");
        var json = File.ReadAllText(path);
        var cv = System.Text.Json.JsonSerializer.Deserialize<CurriculumVitae>(json, new JsonSerializerOptions
{
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
}
        ) ;
        foreach (var job in cv.Work)
        {
            var response = await chatClient.GetResponseAsync("Given " + job.Summary + ". Generate work summary for" + job.Position,  cancellationToken: cancellationToken);
            job.Summary = response.Messages.First().Text;
        }
        //var response = await chatClient.GetResponseAsync("Given Generate work summary for a c# developer", , cancellationToken: cancellationToken);
        //var cv = new CurriculumVitae { Work = new Work[] { new Work { Summary = response.Messages.First().Text } } };
        return new CurriculumVitae[] {cv};
    }
}