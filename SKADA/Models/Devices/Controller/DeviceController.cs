using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SKADA.Models.Devices.Model;
using SKADA.Models.Devices.Service;
using SKADA.Models.DTOS;
using SKADA.Models.Users.Service;

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
        public async Task<List<string>> GetAll()
        { 
             return await _deviceService.GetAllDeviceAddresses();
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