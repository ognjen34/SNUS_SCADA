using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IDigitalInputService
    {
        Task<DigitalInput> GetAll();
        Task<DigitalInput> Get(Guid id);
        Task<DigitalInput> Create(DigitalInput input);
        Task<DigitalInput> Update(DigitalInput input);
        Task<DigitalInput> Delete(Guid id);
    }
}
