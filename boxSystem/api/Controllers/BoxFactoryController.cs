using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;

namespace box_company_back.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxFactoryController : ControllerBase
{
    private NpgsqlDataSource _dataSource;


    private NpgsqlConnection GiveMeAConnection()
    {
        var Uri = new Uri(Environment.GetEnvironmentVariable("pgconn")!);
        return new NpgsqlConnection(string.Format(
            "Server={0};Database={1};User Id={2};Password={3};Port={4};Pooling=false;",
            Uri.Host,
            Uri.AbsolutePath.Trim('/'),
            Uri.UserInfo.Split(':')[0],
            Uri.UserInfo.Split(':')[1],
            Uri.Port > 0 ? Uri.Port : 5432));
        
    }

    public BoxFactoryController()
    {

    }
    [HttpGet]
    [Route("/boxes")]
    public object GetFeed()
    {
        var conn = GiveMeAConnection();
        conn.Open();
            var query = "SELECT * FROM public.boxes";
            var boxes = conn.Query<Boxes>(query);
            conn.Close();
            return Ok(boxes);

    }
        [HttpGet]
    [Route("/box/{boxId}")]
    public object GetBoxById(int boxId)
    {
        var conn = GiveMeAConnection();
        conn.Open();
            var query = "SELECT * FROM public.boxes WHERE boxId = @boxId";
            var box = conn.QuerySingleOrDefault<Boxes>(query, new { BoxId = boxId });

            if (box == null)
            {
                return NotFound($"Box with ID {boxId} not found.");
            }
            conn.Close();
            return Ok(box);

        
    }
    [HttpPut]
    [Route("/box/{boxId}")]
    public object UpdateBoxById(int boxId, [FromBody] BoxesDTO.BoxUpdateDto boxDto)
    {
        var conn = GiveMeAConnection();
        conn.Open();
            var updateQuery = "UPDATE public.boxes " +
                              "SET boxName = @BoxName, material = @Material, " +
                              "width = @Width, height = @Height, depth = @Depth " +
                              "WHERE boxId = @BoxId";

            boxDto.BoxId = boxId;

            var rowsAffected = conn.Execute(updateQuery, boxDto);

            if (rowsAffected == 0)
            {
                conn.Close();
                return NotFound($"Box with ID {boxId} not found.");
            }
            conn.Close();
            return Ok(boxDto);
        
    }


    [HttpDelete]
    [Route("/box/{boxId}")]
    public IActionResult DeleteBoxById(int boxId)
    {
        var conn = GiveMeAConnection();
        conn.Open();

        if (boxId <= 0)
        {
            conn.Close();
            return BadRequest("Invalid BoxId.");
        }
            var query = "DELETE FROM public.boxes WHERE BoxId = @boxId"; 
            var rowsAffected = conn.Execute(query, new { BoxId = boxId });
            if (rowsAffected == 0)
            {
                conn.Close();
                return NotFound($"Box with ID {boxId} not found.");
            }

            conn.Close();
            return NoContent();
    }
    
    
    [HttpPost]
    [Route("/box")]
    public IActionResult CreateBox([FromBody] BoxesDTO.BoxCreateDto boxDto)
    {

        var conn = GiveMeAConnection();
        conn.Open();
        if (boxDto == null)
        {
            conn.Close();
            return BadRequest("Invalid box data.");
        }
            var insertQuery = "INSERT INTO public.boxes (boxName, material, width, height, depth) " +
                              "VALUES (@BoxName, @Material, @Width, @Height, @Depth) RETURNING boxId";

            var result = conn.ExecuteScalar<int>(insertQuery, boxDto);

            if (result <= 0)
            {
                conn.Close();
                return BadRequest("Failed to create a new box.");
            }

            conn.Close();
            return Created($"/box/{result}", result);
        }
    }


