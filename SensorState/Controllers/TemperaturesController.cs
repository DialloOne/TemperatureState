using Microsoft.AspNetCore.Mvc;
using SensorState.Exceptions;
using SensorState.Services;

namespace SensorState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemperaturesController : ControllerBase
    {
        private readonly ITemperatureService _iTemperatureService;
        private readonly IStatusService _iStatusService;


        public TemperaturesController(ITemperatureService iTemperatureService, IStatusService iStatusService)
        {
            _iTemperatureService = iTemperatureService; 
            _iStatusService = iStatusService;
        }

        // GET: api/Temperatures
        [HttpGet]
        public async Task<ActionResult> GetTemperatures()
        {
            var result = await _iTemperatureService.GetAll();
            if (result == null)
            {
                throw new NotFoundException("No temperature found");
            }

            //Return last fifteen temperatures
            return Ok(result.OrderByDescending(t => t.Id).Take(15));
        }

        // POST: api/Temperatures
        [HttpPost]
        public async Task<ActionResult> PostTemperature(int degree)
        {
            var temperature = await _iTemperatureService.BuildTemperatureEntity(degree);

            return Ok(temperature);
        }

    }
}
