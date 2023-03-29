using ErrorManagement.Data;
using ErrorManagement.Exceptions;
using ErrorManagement.Models;
using ErrorManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ErrorManagement.Services;

public class DriverService : IDriverService
{
    private readonly AppDbContext _dbContext;

    public DriverService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Driver>> GetDrivers()
    {
        return await _dbContext.Drivers.ToListAsync();
    }

    public async Task<Driver?> GetDriverById(int id)
    {

        if (await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id) == null)
        {
            //throw new NotFoundException($"({id}) not found");
            throw new NotFoundException("");
        }
        return await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);

    }

    public async Task<Driver> AddDriver(Driver Driver)
    {
        var result = _dbContext.Drivers.Add(Driver);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    //public async Task<Driver> UpdateDriver(Driver Driver)
    //{
    //    var result = _dbContext.Drivers.Update(Driver);
    //    await _dbContext.SaveChangesAsync();
    //    return result.Entity;
    //}
    public async Task<Driver> UpdateDriver(Driver driver)
    {
        try
        {
            var result = _dbContext.Drivers.Update(driver);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception)
        {
            throw new NotFoundException($"({driver.Id}) not found");
        }
    }

    public async Task<bool> DeleteDriver(int Id)
    {
        //var filteredData = _dbContext.Drivers.FirstOrDefault(x => x.Id == Id);
        var dbHero = await _dbContext.Drivers.FindAsync(Id);

        if (dbHero == null)
        {
            throw new NotFoundException($"A product from the database with ID: {Id} could not be found.");
        }
        var result = _dbContext.Remove(dbHero);
        await _dbContext.SaveChangesAsync();
        return result != null ? true : false;
    }
}