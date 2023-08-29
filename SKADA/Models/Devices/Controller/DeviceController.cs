using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;
using SKADA.Models.DTOS;
using SKADA.Models.Users.Service;
using System.Security.Claims;

namespace SKADA.Models.Devices.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<Device>> GetAll()
        { 
             return await _deviceService.GetDevices();
        }
        [HttpDelete("delete/{id}")]
        public async Task<bool> deleteDevice(string id)
        {
            await _deviceService.Delete(id);

            return true;
        }
        
        [AllowAnonymous]
        [HttpPost("device")]
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceDTO deviceDTO)
        {
            Device device = new Device();
            device.Id = new Guid();
            device.Value = 0;
            device.IOAdress = deviceDTO.IOAdress;
            device.deviceType = deviceDTO.deviceType;
            device.deviceConfig = deviceDTO.deviceConfig;


            await _deviceService.Add(device);
            return Ok();
        }
    }

    

}