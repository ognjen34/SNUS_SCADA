using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public interface IDigitalInputRepository
    {
        Task<IEnumerable<DigitalInput>> GetAll();
        Task<DigitalInput> Get(Guid id);
        Task<IEnumerable<DigitalInput>> GetByIOAddress(string IOAddress);
        Task<DigitalInput> Create(DigitalInput input);
        Task<DigitalInput> Update(DigitalInput input);
        Task<DigitalInput> Delete(Guid id);
    }
}
