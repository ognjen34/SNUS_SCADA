using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IDigitalInputService
    {
        public Task Create(DigitalInput input);
        public Task startDigitalDataReading();
        Task<IEnumerable<DigitalInput>> GetAll();
        Task<IEnumerable<DigitalReadInstance>> GetAllDigitalReads();
        public Task Delete(Guid id);
        Task Update(DigitalInput newDigitalInput);
    }
}
