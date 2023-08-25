using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public interface IDigitalReadInstanceRepository
    {
        Task<IEnumerable<DigitalReadInstance>> GetAll();
        Task<DigitalReadInstance> Get(Guid id);
        Task<DigitalReadInstance> Create(DigitalReadInstance input);
        Task<DigitalReadInstance> Update(DigitalReadInstance input);
        Task<DigitalReadInstance> Delete(Guid id);
    }
}
