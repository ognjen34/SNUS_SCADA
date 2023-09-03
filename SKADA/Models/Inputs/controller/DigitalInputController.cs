
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.DTOS;
using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Service;
using SKADA.Models.Users.Model;
using SKADA.Models.Users.Service;
using System.Security.Claims;

namespace SKADA.Models.Inputs.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DigitalInputController : ControllerBase
    {
        private readonly IDigitalInputService _digitalInputService;
        private readonly IUserService _userService;
        private readonly IDigitalReadInstanceService _readInstanceService;

        public DigitalInputController(IDigitalInputService digitalInputService, IUserService userService, IDigitalReadInstanceService readInstanceService)
        {
            _digitalInputService = digitalInputService;
            _userService = userService;
            _readInstanceService = readInstanceService;
        }
        [Authorize(Policy = "ClientOnly")]
        [HttpGet]
        public async Task<List<DigitalInput>> GetAllClients()
        {
            var userNameClaim = User.FindFirst(ClaimTypes.Name);
            User user = _userService.GetByEmail(userNameClaim.Value).Result;
            return user.digitalInputs.ToList();
        }

        [HttpGet("digitalreads")]
        public async Task<IEnumerable<DigitalReadInstance>> GetAllCDigitalReads()
        {
            return await _digitalInputService.GetAllDigitalReads();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<DigitalInput>> GetAll()
        {

            return await _digitalInputService.GetAll();
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<IActionResult> CreateDigitalInput([FromBody] DigitalInputDTO digitalDTO)
        {
            DigitalInput digital = new DigitalInput();
            digital.Id = Guid.NewGuid();
            digital.IOAddress = digitalDTO.IOAddress;
            digital.Description = digitalDTO.Description;
            digital.ScanTime = digitalDTO.ScanTime;
            digital.Scan = digitalDTO.Scan;

            await _digitalInputService.Create(digital);
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<List<DigitalReadInstance>> GetAllValues(string id)
        {

            return await _readInstanceService.GetAllTagsValues(id);
        }
        [HttpGet("data")]
        public async Task<List<DigitalReadInstance>> GetAll([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _readInstanceService.GetAllTagsInDateRange(startDate, endDate);
        }

        [AllowAnonymous]
        [HttpPut("")]
        public async Task<IActionResult> UpdateDigitalInput([FromBody] DigitalInput newDigitalInput)
        {
            await _digitalInputService.Update(newDigitalInput);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _digitalInputService.Delete(Guid.Parse(id));

            return Ok();
        }
    }



}