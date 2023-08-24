using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IAnalogInputService
    {
        Task<AnalogInput> GetAll();
        Task<AnalogInput> Get(Guid id);
        Task<AnalogInput> Create(AnalogInput input);
        Task<AnalogInput> Update(AnalogInput input);
        Task<AnalogInput> Delete(Guid id);
    }
}
