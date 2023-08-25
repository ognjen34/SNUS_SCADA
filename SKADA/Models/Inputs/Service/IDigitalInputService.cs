using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IDigitalInputService
    {
        public Task Create(DigitalInput input);
        public Task startDigitalDataReading();
        Task<IEnumerable<DigitalInput>> GetAll();

    }
}
