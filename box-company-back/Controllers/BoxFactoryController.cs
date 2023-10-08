using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;

namespace box_company_back.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxFactoryController : ControllerBase
{
    private NpgsqlDataSource _dataSource;

    public BoxFactoryController(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }
    [HttpGet]
    [Route("/boxes")]
    public object GetFeed()
    {
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }
        using (var conn = _dataSource.OpenConnection())
        {
            var query = "SELECT * FROM public.boxes";
            var boxes = conn.Query<Boxes>(query);
            return Ok(boxes);
        }
    }
        [HttpGet]
    [Route("/box/{boxId}")]
    public object GetBoxById(int boxId)
    {
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }

        using (var conn = _dataSource.OpenConnection())
        {
            var query = "SELECT * FROM public.boxes WHERE boxId = @boxId";
            var box = conn.QuerySingleOrDefault<Boxes>(query, new { BoxId = boxId });

            if (box == null)
            {
                return NotFound($"Box with ID {boxId} not found.");
            }
            return Ok(box);

        }
    }
    [HttpPut]
    [Route("/box/{boxId}")]
    public object UpdateBoxById(int boxId, [FromBody] BoxesDTO.BoxUpdateDto boxDto)
    {
        Console.WriteLine($"HERE: {boxDto}");
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }

        using (var conn = _dataSource.OpenConnection())
        {
            var updateQuery = "UPDATE public.boxes " +
                              "SET boxName = @BoxName, material = @Material, " +
                              "width = @Width, height = @Height, depth = @Depth " +
                              "WHERE boxId = @BoxId";

            boxDto.BoxId = boxId;

            var rowsAffected = conn.Execute(updateQuery, boxDto);

            if (rowsAffected == 0)
            {
                return NotFound($"Box with ID {boxId} not found.");
            }
            return Ok(boxDto);
        }
    }


    [HttpDelete]
    [Route("/box/{boxId}")]
    public IActionResult DeleteBoxById(int boxId)
    {
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }

        if (boxId <= 0)
        {
            return BadRequest("Invalid BoxId.");
        }

        using (var conn = _dataSource.OpenConnection())
        {
            var query = "DELETE FROM public.boxes WHERE BoxId = @boxId"; 
            var rowsAffected = conn.Execute(query, new { BoxId = boxId });
            if (rowsAffected == 0)
            {
                return NotFound($"Box with ID {boxId} not found.");
            }

            return NoContent();
        }
    }
    
    
    [HttpPost]
    [Route("/box")]
    public IActionResult CreateBox([FromBody] BoxesDTO.BoxCreateDto boxDto)
    {
        if (string.IsNullOrEmpty(_dataSource.OpenConnection().ToString()))
        {
            return BadRequest("Connection string is not configured.");
        }

        if (boxDto == null)
        {
            return BadRequest("Invalid box data.");
        }

        using (var conn = _dataSource.OpenConnection())
        {
            var insertQuery = "INSERT INTO public.boxes (boxName, material, width, height, depth) " +
                              "VALUES (@BoxName, @Material, @Width, @Height, @Depth) RETURNING boxId";

            var result = conn.ExecuteScalar<int>(insertQuery, boxDto);

            if (result <= 0)
            {
                return BadRequest("Failed to create a new box.");
            }

            return Created($"/box/{result}", result);
        }
    }

}
