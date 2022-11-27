using Microsoft.AspNetCore.Mvc;
using SensorState.Exceptions;
using SensorState.Models;
using SensorState.Services;

namespace SensorState.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusBoundsController : ControllerBase
    {
        private readonly IStatusService _iStatusService;

        public StatusBoundsController(IStatusService iStatusService)
        {
            _iStatusService = iStatusService;
        }

        // GET: api/StatusBounds
        [HttpGet]
        public async Task<ActionResult> GetStatusBounds()
        {
            var result = await _iStatusService.GetStatusBoundModelAsync();
            if (result == null)
            {
                throw new NotFoundException("Status bound not found");
            }

            return Ok(result);
        }


        // PUT: api/StatusBounds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusBound(int id, StatusBound statusBound)
        {
            var result = await _iStatusService.RedefineStatusBoundAsync(id, statusBound);

            return Ok(result);
        }

    }
}
