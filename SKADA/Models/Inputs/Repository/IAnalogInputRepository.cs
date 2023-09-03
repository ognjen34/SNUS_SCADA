using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public interface IAnalogInputRepository
    {
        Task<IEnumerable<AnalogInput>> GetAll();
        Task<AnalogInput> Get(Guid id);
        Task<IEnumerable<AnalogInput>> GetByIOAddress(string IOAddress);
        Task<AnalogInput> Create(AnalogInput input);
        Task<AnalogInput> Update(AnalogInput input);
        Task<AnalogInput> Delete(Guid id);
    }
}
