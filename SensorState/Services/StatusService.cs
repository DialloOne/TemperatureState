using Microsoft.EntityFrameworkCore;
using SensorState.Context;
using SensorState.Exceptions;
using SensorState.Models;
using SensorState.ViewModels;

namespace SensorState.Services
{
    public class StatusService : IStatusService
    {
        private DatabaseContext _dbContext { get; set; }

        public StatusService(DatabaseContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        public async Task<StatusBound> GetStatusBoundModelAsync()
        {
            return await _dbContext.StatusBounds.FirstAsync();
        }

        public async Task<ResponseModel> RedefineStatusBoundAsync(int id, StatusBound statusBound)
        {
            ResponseModel responseModel = new()
            {
                IsSuccess = false,
                Message = "StatusBound not updated"
            };

            var StatusBoundExists = _dbContext.StatusBounds?.Any(e => e.StatusBoundId == id);

            if (!StatusBoundExists.HasValue || !StatusBoundExists.Value)
            {
                throw new NotFoundException("StatusBound item not found");
            }

            if (id != statusBound.StatusBoundId)
            {
                throw new BadRequestException("Bad request");
            }

            try
            {
                _dbContext.Entry(statusBound).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                responseModel.Message = "StatusBound Update Successfully";
                responseModel.IsSuccess = true;
            }
            catch 
            {
                throw;
            }

            return responseModel;
        }
    }
}
