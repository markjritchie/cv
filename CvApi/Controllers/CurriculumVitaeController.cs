using System.Threading.Tasks;
using CvApi.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CvApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurriculumVitaeController(ILogger<CurriculumVitaeController> logger, ICurriculumVitaeQueries curriculumVitaeQueries) : ControllerBase
{


    [HttpGet]
    public async Task<CurriculumVitae[]> Get(CancellationToken cancellationToken)
    {
      return await curriculumVitaeQueries.GetAll(cancellationToken);   
    }
}
