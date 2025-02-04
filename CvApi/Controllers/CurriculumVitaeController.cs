using CvApi.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CvApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CurriculumVitaeController(ILogger<CurriculumVitaeController> logger, ICurriculumVitaeQueries curriculumVitaeQueries) : ControllerBase
{


    [HttpGet]
    public CurriculumVitae[] Get()
    {
      return curriculumVitaeQueries.GetAll();   
    }
}
