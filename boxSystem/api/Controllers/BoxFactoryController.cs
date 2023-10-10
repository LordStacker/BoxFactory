using Microsoft.AspNetCore.Mvc;
using infrastructure.Repositories;
using service;

namespace box_company_back.Controllers;

[ApiController]
[Route("[controller]")]
public class BoxFactoryController : ControllerBase
{
   

    private readonly ILogger<BoxFactoryController> _logger;
    private readonly BoxService _boxService;

    public BoxFactoryController(ILogger<BoxFactoryController> logger,
        BoxService boxService)
    {
        _logger = logger;
        _boxService = boxService;
    }
    [HttpGet]
    [Route("/boxes")]
    public object GetFeed()
    {
        return _boxService.GetBoxFeed();
    }
    [HttpGet]
    [Route("/box/{boxId}")]
    public object GetBoxById(int boxId)
    {
        return _boxService.GetBoxById(boxId);;
    }
    
    [HttpGet]
    [Route("/box/search")]
    public object SearchBox([FromHeader] string searchterm)
    {
        return _boxService.SearchBox(searchterm);
    }
    
    
    [HttpPut]
    [Route("/box/{boxId}")]
    public BoxFeedQuery UpdateBoxById(int boxId, [FromBody] BoxUpdateDto boxDto)
    {
        return _boxService.UpdateBoxById(boxDto.BoxId, boxDto.BoxName, boxDto.Material, boxDto.Width, boxDto.Height,
            boxDto.Depth);
    }


    [HttpDelete]
    [Route("/box/{boxId}")]
    public object DeleteBoxById(int boxId)
    {
        _boxService.DeleteBoxById(boxId);
        return new { message = "Box has been removed" };
    }
    
    
    [HttpPost]
    [Route("/box")]
    public object CreateBox([FromBody] BoxCreateDto boxDto)
    {
        return _boxService.CreateBox(boxDto.BoxName, boxDto.Material, boxDto.Width, boxDto.Height,
            boxDto.Depth);
    }
    
    public class BoxCreateDto
    {
        public string BoxName { get; set; }
        public string Material { get; set; }
        public decimal Width { get; set; } 
        public decimal Height { get; set; } 
        public decimal Depth { get; set; } 
    }

    public class BoxUpdateDto
    {
        public int BoxId { get; set; } 
        public string BoxName { get; set; }
        public string Material { get; set; }
        public decimal Width { get; set; } 
        public decimal Height { get; set; } 
        public decimal Depth { get; set; } 
    }
    }


