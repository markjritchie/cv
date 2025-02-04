namespace CvApi.Queries;

public interface ICurriculumVitaeQueries
{
    public CurriculumVitae[] GetAll();
}

public class CurriculumVitaeQueries : ICurriculumVitaeQueries
{
    public CurriculumVitae[] GetAll()
    {
        return Enumerable.Range(1, 5).Select(index => new CurriculumVitae
        {

        })
        .ToArray();
    }
}