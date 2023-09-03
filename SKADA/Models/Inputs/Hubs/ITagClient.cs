using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Hubs
{
    public interface ITagClient
    {
        Task ReceiveAnalogData(AnalogReadInstance data);
        Task ReceiveDigitalData(DigitalReadInstance data);
    }
}
