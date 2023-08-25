using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Inputs.Service
{
    public class AnalogReadInstanceService : IAnalogReadInstanceService
    {
        private readonly IAnalogReadInstanceRepository _analogReadInstanceRepository;
        public AnalogReadInstanceService(IAnalogReadInstanceRepository analogReadInstanceRepository)
        {
            _analogReadInstanceRepository = analogReadInstanceRepository;
        }
        public async Task Create(AnalogReadInstance input)
        {
            await _analogReadInstanceRepository.Create(input);
        }

        public Task<AnalogReadInstance> Get(Guid id)
        {
            return _analogReadInstanceRepository.Get(id);
        }

        public Task<AnalogReadInstance> GetByTagId(Guid id)
        {
            return _analogReadInstanceRepository.GetByTagId(id);
        }
    }
}
