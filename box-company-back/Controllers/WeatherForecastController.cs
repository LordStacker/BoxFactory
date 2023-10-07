using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;

namespace box_company_back.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private NpgsqlDataSource _dataSource;

    public WeatherForecastController(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    [HttpGet]
    [Route("/api/feed")]
    public object GetFeed()
    {
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }
        using (var conn = _dataSource.OpenConnection())
        {
            var query = "SELECT * FROM public.boxes";
            var articles = conn.Query<Boxes>(query);
            return Ok(articles);
        }
    }
}
