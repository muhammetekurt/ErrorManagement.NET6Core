using ErrorManagement.Data;
using ErrorManagement.Exceptions;
using ErrorManagement.Models;
using ErrorManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ErrorManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class DriversController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDriverService _driverServices;
    private readonly AppDbContext _dbContext;

    public DriversController(
        ILogger<WeatherForecastController> logger,
        IDriverService driverServices,
        AppDbContext dbContext)
    {
        _logger = logger;
        _driverServices = driverServices;
        _dbContext = dbContext;
    }

    [HttpGet("driverlist")]
    public async Task<IEnumerable<Driver>> DriverList()
    {
        var driverList = await _driverServices.GetDrivers();
        return driverList;
    }

    [HttpGet("getdriverbyid")]
    public async Task<IActionResult> GetDriverById(int Id)
    {
        //_logger.LogInformation($"Fetch Driver with ID: {Id} from the database");
        var driver = await _driverServices.GetDriverById(Id);
        //if (driver == null)
        //{
        //    throw new Notfound($"Driver ID {Id} not found.");
        //    return NotFound();
        //}
        //_logger.LogInformation($"Returning driver with ID: {driver.Id}.");
        return Ok(driver);
    }

    [HttpPost("adddriver")]
    public async Task<IActionResult> AddDriver(Driver driver)
    {
        var result = await _driverServices.AddDriver(driver);
        return Ok(result);
    }

    [HttpPut("updatedriver")]
    public async Task<IActionResult> UpdateDriver(Driver driver)
    {
        //var result = await _driverServices.UpdateDriver(driver);
        //return Ok(result);
        //var driverServices = new _driverServices(_dbContext);
        var result = await _driverServices.UpdateDriver(driver);
        return Ok(result);


    }



    [HttpDelete("deletedriver")]
    public async Task<bool> DeleteDriver(int Id)
    {
        return await _driverServices.DeleteDriver(Id);
    }
}