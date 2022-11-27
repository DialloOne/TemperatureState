using System.Linq;
using SensorState.Models;

namespace SensorState.Services;

public interface ITemperatureService
{
    Task<IEnumerable<Temperature>> GetAll();
    Task<Temperature> BuildTemperatureEntity(int degree);
}
