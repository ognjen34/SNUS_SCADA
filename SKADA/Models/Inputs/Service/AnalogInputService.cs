using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Inputs.Service
{
    public class AnalogInputService : IAnalogInputService
    {

        private readonly IAnalogInputRepository _analogInputRepository;
        public AnalogInputService(IAnalogInputRepository analogInputRepository, IDigitalInputRepository digitalInputRepository)
        {
            _analogInputRepository = analogInputRepository;
        }
        public Task<AnalogInput> Create(AnalogInput input)
        {
            throw new NotImplementedException();
        }

        public Task<AnalogInput> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AnalogInput> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AnalogInput> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AnalogInput> Update(AnalogInput input)
        {
            throw new NotImplementedException();
        }
    }
}
