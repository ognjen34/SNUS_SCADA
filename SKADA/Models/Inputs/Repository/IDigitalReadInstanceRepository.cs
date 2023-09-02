using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public interface IDigitalReadInstanceRepository
    {
        Task<IEnumerable<DigitalReadInstance>> GetAll();
        Task<IEnumerable<DigitalReadInstance>> GetAllSorted(Guid id);
        Task<DigitalReadInstance> GetByTagId(Guid id);
        Task<DigitalReadInstance> Get(Guid id);
        Task<DigitalReadInstance> Create(DigitalReadInstance input);
        Task<DigitalReadInstance> Update(DigitalReadInstance input);
        Task<DigitalReadInstance> Delete(Guid id);
    }
}
