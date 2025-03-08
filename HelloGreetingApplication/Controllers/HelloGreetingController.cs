using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
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
    /// UC1- Get method to get the message
    /// </summary>
    /// <returns>"Hello, World</returns>
    [HttpGet]
    [Route("getMethod")]
    public IActionResult Get()
    {
        _logger.Info("Get request received successfully.");
        responseModel = new ResponseModel<string>();

        responseModel.Success = true;
        responseModel.Message = "Hello to Greeting App API Endpoint.";
        responseModel.Data = "Hello, World";
        return Ok(responseModel);
    }

    /// <summary>
    /// UC1- Post method to receive request
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Response model</returns>
    [HttpPost]
    [Route("postMethod")]
    public IActionResult Post(RequestModel requestModel)
    {
        _logger.Info($"Post request received with key: {requestModel.Key} and value: {requestModel.Value}");
        try
        {
            responseModel = new ResponseModel<string>();

            responseModel.Success = true;
            responseModel.Message = "Request received successfully.";
            responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
            return Ok(responseModel);

        }
        catch (Exception ex)
        {
            responseModel.Success = false;
            responseModel.Message = "This is an exception message";
            responseModel.Data = "Exception occured";
        }
        return BadRequest(responseModel);
    }

    /// <summary>
    /// UC1- Put method to update the values
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Response Model</returns>
    [HttpPut]
    [Route("putMethod")]
    public IActionResult Put(RequestModel requestModel)
    {
        _logger.Info($"Request received, deleting greeting with value: {requestModel.Value}");
        responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Request Updated Successfully.";
        responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
        return Ok(responseModel);

    }

    /// <summary>
    /// UC1- Patch method to partially update the values
    /// </summary>
    /// <param name="requestModel"></param>
    /// <returns>Response Model</returns>
    [HttpPatch]
    [Route("patchMethod")]
    public IActionResult Patch(RequestModel requestModel)
    {
        _logger.Info($"Request received, deleting greeting with value: {requestModel.Value}");
        responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Request Updated Successfully.";
        responseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";
        return Ok(responseModel);
    }

    /// <summary>
    /// UC1- Delete method to delete the value
    /// </summary>
    /// <returns>Request Model</returns>
    [HttpDelete]
    [Route("deleteMethod")]
    public IActionResult Delete(RequestModel requestModel)
    {
        _logger.Info($"Removing greeting for key: {requestModel.Key}");
        responseModel = new ResponseModel<string>();
        responseModel.Success = true;
        responseModel.Message = "Greeting deleted successfully.";
        responseModel.Data = string.Empty;
        return Ok(responseModel);
    }

    /// <summary>
    /// UC2- Getmessage method to get the message
    /// </summary>
    /// <returns>Hello World</returns>
    public string GetMessage()
    {
        return _greetingBL.GreetMessage();
    }

    /// <summary>
    /// UC3- Getmessage method to get the message
    /// </summary>
    /// <returns>Hello World</returns>
    [HttpGet]
    [Route("getMessage")]
    public IActionResult GetMessage(string? firstName, string? lastName)
    {
        _logger.Info($"Get request received with Firstname: {firstName}, Lastname: {lastName}");

        string message = _greetingBL.GreetMessage(firstName, lastName);

        ResponseModel<string> responseModel = new ResponseModel<string>()
        {
            Success = true,
            Message = "Greet Message generated",
            Data = message
        };
        return Ok(responseModel);
    }

    /// <summary>
    /// UC4- Add Greeting method Saves the greeting messages
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

    /// <summary>
    /// UC5- Method used to get greeting message by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Greeting Message</returns>
    [HttpGet]
    [Route("GetGreetingById")]
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
    /// UC6- Method to list all the greeting messages
    /// </summary>
    /// <returns>List of messages</returns>
    [HttpGet]
    [Route("ListGreeting")]
    public IActionResult GetGreetings()
    {
        var greetings = _greetingBL.GetGreetings();
        return Ok(greetings);
    }

    /// <summary>
    /// UC7- Method to update the greeting message
    /// </summary>
    /// <param name="id"></param>
    /// <param name="greeting"></param>
    /// <returns>Updated greeting messages</returns>
    [HttpPut]
    [Route("UpdateGreeting")]
    public IActionResult UpdateGreeting(int id, GreetingEntity greeting)
    {
        if (greeting == null || string.IsNullOrWhiteSpace(greeting.Message))
        {
            return BadRequest("Invalid greeting message.");
        }

        var updatedGreeting = _greetingBL.UpdateGreeting(id, greeting);
        if (updatedGreeting == null)
        {
            return NotFound("Greeting not found.");
        }
        return Ok(updatedGreeting);
    }

    /// <summary>
    ///UC8- Method to delete a message
    /// </summary>
    /// <param name="id"></param>
    /// <returns>deleted message</returns>
    [HttpDelete]
    [Route("DeleteId")]
    public IActionResult DeleteGreeting(int id)
    {
        var isDeleted = _greetingBL.DeleteGreeting(id);
        if (!isDeleted)
        {
            return NotFound("Greeting not found.");
        }
        return NoContent();
    }
}
