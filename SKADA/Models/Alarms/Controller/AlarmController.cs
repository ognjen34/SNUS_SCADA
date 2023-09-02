using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.Alarms.Model;
using SKADA.Models.Alarms.Service;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;
using SKADA.Models.DTOS;
using SKADA.Models.Users.Service;
using System.Security.Claims;

namespace SKADA.Models.Devices.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlarmController : ControllerBase
    {
        private readonly IAlarmService _alarmService;

        public AlarmController(IAlarmService alarmService)
        {
            _alarmService = alarmService;
        }
        [AllowAnonymous]
        [HttpGet("data")]
        public async Task<List<AlarmInstance>> GetAll([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return await _alarmService.GetAllInDateRange(startDate,endDate);
        }
      

       
    }



}