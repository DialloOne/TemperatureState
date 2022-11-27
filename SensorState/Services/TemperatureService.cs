using Microsoft.EntityFrameworkCore;
using SensorState.Context;
using SensorState.Exceptions;
using SensorState.Models;

namespace SensorState.Services;

public class TemperatureService : ITemperatureService
{
    private DatabaseContext _dbContext { get; set; }
    private IStatusService _iStatusService { get; set; }

    public TemperatureService(DatabaseContext dbContext, IStatusService iStatusService)
    {
        _dbContext = dbContext;
        _iStatusService = iStatusService; 
    }

    public async Task<Temperature> BuildTemperatureEntity(int degree)
    {
        var statusBound = await _iStatusService.GetStatusBoundModelAsync();

        if (statusBound != null)
        {
            Temperature temperature = new()
            {
                Value = degree
            };

            if (degree < statusBound.UpperColdBound)
                temperature.StatusId = (int)TemperatureType.COLD;
            else if (degree >= statusBound.LowerHotBound)
                temperature.StatusId = (int)TemperatureType.HOT;
            else
                temperature.StatusId = (int)TemperatureType.WARM;

            if (_dbContext.Temperatures == null)
            {
                throw new NotFoundException("Entity set 'DatabaseContext.Temperatures'  is null.");
            }

            _dbContext.Temperatures.Add(temperature);
            await _dbContext.SaveChangesAsync();

            return temperature;
        }
        else
        {
            throw new ArgumentNullException(nameof(statusBound));
        }

    }

    public async Task<IEnumerable<Temperature>> GetAll()
    {
        return await _dbContext.Temperatures.Include(t => t.Status).ToListAsync();
    }
}
