using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.Alarms.Model;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;
using SKADA.Models.DTOS;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Service;

namespace SKADA.Models.Inputs.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalogInputController : ControllerBase
    {
        private readonly IAnalogInputService _analogInputService;

        public AnalogInputController(IAnalogInputService analogInputService)
        {
            _analogInputService = analogInputService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<List<AnalogInput>> GetAll()
        {
            return (List<AnalogInput>)await _analogInputService.GetAll();
        }

        [AllowAnonymous]
        [HttpPost("analog")]
        public async Task<IActionResult> CreateAnalogInput([FromBody] AnalogInputDTO analogDTO)
        {
            AnalogInput analog = new AnalogInput();
            analog.Id = new Guid();
            analog.IOAddress = analogDTO.IOAddress;
            analog.Description = analogDTO.Description;
            analog.ScanTime = analogDTO.ScanTime;
            analog.Scan = analogDTO.Scan;
            analog.Units = analogDTO.Unit;
            analog.Alarms = new List<Alarm>();

            await _analogInputService.Create(analog);
            return Ok();
        }
    }



}