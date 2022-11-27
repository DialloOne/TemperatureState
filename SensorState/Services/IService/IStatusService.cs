using SensorState.Models;
using SensorState.ViewModels;

namespace SensorState.Services; 

public interface IStatusService 
{
    Task<ResponseModel> RedefineStatusBoundAsync(int id, StatusBound statusBound);
    Task<StatusBound> GetStatusBoundModelAsync();
}
