using BusinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Model;
using NLog;

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
    /// Get method to get the message
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
    /// Post method to receive request
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
    /// Put method to update the values
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
    /// Patch method to partially update the values
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
    /// Delete method to delete the value
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
    /// Getmessage method to get the message
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
}
