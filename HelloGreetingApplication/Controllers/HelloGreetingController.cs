using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;
using RepositoryLayer.Entity;

namespace HelloGreetingApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloGreetingController : ControllerBase
{
    IGreetingBL _greetingBL;
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();
    ResponseModel<string> responseModel;

    public HelloGreetingController(IGreetingBL greetingBL)
    {
        _greetingBL = greetingBL;
    }

    /// <summary>
    /// Method to list all the greeting messages
    /// </summary>
    /// <returns>List of messages</returns>
    [HttpGet]
    public IActionResult GetGreetings()
    {
        var greetings = _greetingBL.GetGreetings();
        return Ok(greetings);
    }

    /// <summary>
    /// Method used to get greeting message by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Greeting Message</returns>
    [HttpGet("{id}")]
    [Route("GetId")]
    public IActionResult GetGreetingById(int id)
    {
        var Id = _greetingBL.GetGreetingById(id);
        if (Id == null)
        {
            return NotFound("Greeting not found.");
        }
        return Ok(Id);
    }

    /// <summary>
    /// Add Greeting method Saves the greeting messages
    /// </summary>
    /// <param name="greeting"></param>
    /// <returns>id and message</returns>
    [HttpPost]
    [Route("GreetingId")]
    public IActionResult AddGreeting(GreetingEntity greeting)
    {
        if (greeting == null || string.IsNullOrWhiteSpace(greeting.Message))
        {
            return BadRequest("Invalid greeting message.");
        }

        var createdGreeting = _greetingBL.AddGreeting(greeting);
        return CreatedAtAction(nameof(GetGreetings), new { id = createdGreeting.Id }, createdGreeting);             
    }
}
