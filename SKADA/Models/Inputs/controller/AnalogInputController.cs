using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.Alarms.Model;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;
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
    public class AnalogInputController : ControllerBase
    {
        private readonly IAnalogInputService _analogInputService;
        private readonly IAnalogReadInstanceService _analogReadInstanceService;
        private readonly IDeviceService _deviceService;
        private readonly IUserService _userService;

        public AnalogInputController(IUserService userService,IAnalogInputService analogInputService, IDeviceService deviceService, IAnalogReadInstanceService analogReadInstanceService)
        {
            _userService = userService;
            _analogInputService = analogInputService;
            _deviceService = deviceService;
            _analogReadInstanceService = analogReadInstanceService;
        }

        [Authorize(Policy = "ClientOnly")]
        [HttpGet]
        public async Task<List<AnalogInput>> GetAllClients()
        {
            var userNameClaim = User.FindFirst(ClaimTypes.Name);
            User user = _userService.GetByEmail(userNameClaim.Value).Result;
            return user.analogInputs.ToList();
        }

        [HttpGet("digitalreads")]
        public async Task<IEnumerable<AnalogReadInstance>> GetAllCAnalogReads()
        {
            return await _analogReadInstanceService.GetAllAnalogReads();
        }

        [HttpGet("all")]
        public async Task<IEnumerable<AnalogInput>> GetAll()
        {

            return await _analogInputService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<List<AnalogReadInstance>> GetAllValues(string id)
        {

            return await _analogReadInstanceService.GetAllTagsValues(id);
        }

        [HttpGet("data")]
        public async Task<List<AnalogReadInstance>> GetAll([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _analogReadInstanceService.GetAllTagsInDateRange(startDate, endDate);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost]
        public async Task<IActionResult> CreateAnalogInput([FromBody] AnalogInputDTO analogDTO)
        {
            AnalogInput analog = new AnalogInput();
            analog.Id = new Guid();
            analog.IOAddress = analogDTO.IOAddress;
            analog.Description = analogDTO.Description;
            analog.ScanTime = analogDTO.ScanTime;
            analog.Scan = analogDTO.Scan;
            analog.Units = analogDTO.Unit;
            List<Alarm> alarmsToAdd = new List<Alarm>();
            foreach(AlarmDTO alarmDTO in analogDTO.Alarms)
            {
                Alarm alarm = new Alarm();
                alarm.Id = Guid.NewGuid();
                alarm.CriticalValue = alarmDTO.CriticalValue;
                alarm.Units = alarmDTO.Units;
                if(alarmDTO.Priority == 1)
                {
                    alarm.Priority = Alarm.AlarmPriority.LOW;

                }
                else if(alarmDTO.Priority == 2)
                {
                    alarm.Priority = Alarm.AlarmPriority.MEDIUM;

                }
                else if(alarmDTO.Priority == 3)
                {
                    alarm.Priority = Alarm.AlarmPriority.HIGH;

                }
                else
                {
                    return BadRequest();
                }

                if (alarmDTO.Type == 0)
                {
                    alarm.Type = Alarm.AlarmType.LOW;

                }
                else if (alarmDTO.Type == 1)
                {
                    alarm.Type = Alarm.AlarmType.HIGH;

                }
                else
                {
                    return BadRequest();
                }
                alarmsToAdd.Add(alarm);

            }
            analog.Alarms = alarmsToAdd;

            await _analogInputService.Create(analog);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPut("")]
        public async Task<IActionResult> UpdateAnalogInput([FromBody] UpdateAnalogInputDTO newAnalogInputDTO)
        {
            AnalogInput newAnalogInput = new AnalogInput();
            newAnalogInput.Id = Guid.Parse(newAnalogInputDTO.id);
            newAnalogInput.IOAddress = newAnalogInputDTO.IOAddress;
            newAnalogInput.ScanTime = newAnalogInputDTO.ScanTime;
            newAnalogInput.Scan = newAnalogInputDTO.Scan;
            newAnalogInput.Description = newAnalogInputDTO.Description;
            newAnalogInput.Units = newAnalogInputDTO.Unit;
            List<Alarm> alarmsToAdd = new List<Alarm>();
            foreach (UpdateAlarmDTO alarmDTO in newAnalogInputDTO.Alarms)
            {
                Alarm alarm = new Alarm();
                if(newAnalogInputDTO.id == "")
                {
                    alarm.Id = Guid.NewGuid();
                }
                else
                {
                    alarm.Id = Guid.Parse(alarmDTO.id);
                }
                alarm.CriticalValue = alarmDTO.CriticalValue;
                alarm.Units = alarmDTO.Units;
                if (alarmDTO.Priority == 1)
                {
                    alarm.Priority = Alarm.AlarmPriority.LOW;

                }
                else if (alarmDTO.Priority == 2)
                {
                    alarm.Priority = Alarm.AlarmPriority.MEDIUM;

                }
                else if (alarmDTO.Priority == 3)
                {
                    alarm.Priority = Alarm.AlarmPriority.HIGH;

                }
                else
                {
                    return BadRequest();
                }

                if (alarmDTO.Type == 0)
                {
                    alarm.Type = Alarm.AlarmType.LOW;

                }
                else if (alarmDTO.Type == 1)
                {
                    alarm.Type = Alarm.AlarmType.HIGH;

                }
                else
                {
                    return BadRequest();
                }
                alarmsToAdd.Add(alarm);
            }
            newAnalogInput.Alarms = alarmsToAdd;
            await _analogInputService.Update(newAnalogInput);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _analogInputService.Delete(Guid.Parse(id));

            return Ok();
        }
    }



}