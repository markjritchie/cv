using System.Threading.Tasks;
using CvApi.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CvApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurriculumVitaeController(ILogger<CurriculumVitaeController> logger, ICurriculumVitaeQueries curriculumVitaeQueries) : ControllerBase
{


    [HttpGet]
    [ProducesResponseType(typeof(CurriculumVitae[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
      return Ok(await curriculumVitaeQueries.GetAll(cancellationToken));   
    }
}
