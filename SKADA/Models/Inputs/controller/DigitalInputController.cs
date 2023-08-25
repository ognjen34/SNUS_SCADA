
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.DTOS;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Service;

namespace SKADA.Models.Inputs.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DigitalInputController : ControllerBase
    {
        private readonly IDigitalInputService _digitalInputService;

        public DigitalInputController(IDigitalInputService digitalInputService)
        {
            _digitalInputService = digitalInputService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<List<DigitalInput>> GetAll()
        {
            return (List<DigitalInput>)await _digitalInputService.GetAll();
        }

        [AllowAnonymous]
        [HttpPost("digital")]
        public async Task<IActionResult> CreateDigitalInput([FromBody] DigitalInputDTO digitalDTO)
        {
            DigitalInput digital = new DigitalInput();
            digital.Id = new Guid();
            digital.IOAddress = digitalDTO.IOAddress;
            digital.Description = digitalDTO.Description;
            digital.ScanTime = digitalDTO.ScanTime;
            digital.Scan = digitalDTO.Scan;

            await _digitalInputService.Create(digital);
            return Ok();
        }
    }



}