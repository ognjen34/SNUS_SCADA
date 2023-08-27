using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public interface IAnalogReadInstanceRepository
    {
        Task<IEnumerable<AnalogReadInstance>> GetAll();
        Task<AnalogReadInstance> Get(Guid id);
        Task<AnalogReadInstance> Create(AnalogReadInstance input);
        Task<AnalogReadInstance> GetByTagId(Guid id);
        
        
    }
}