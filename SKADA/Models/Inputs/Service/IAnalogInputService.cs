using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IAnalogInputService
    {
        Task<IEnumerable<AnalogInput>> GetAll();
        Task<AnalogInput> Get(Guid id);
        Task Create(AnalogInput input);
        Task Update(AnalogInput input);
        Task Delete(Guid id);
        Task startAnalogDataReading();

    }
}
