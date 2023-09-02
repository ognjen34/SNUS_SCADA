using SKADA.Models.Inputs.Model;
using SKADA.Models.Inputs.Repository;

namespace SKADA.Models.Inputs.Service
{
    public class DigitalReadInstanceService : IDigitalReadInstanceService
    {
        private readonly IDigitalReadInstanceRepository _digitalReadInstanceRepository;
        public DigitalReadInstanceService(IDigitalReadInstanceRepository digitalReadInstanceRepository)
        {
            _digitalReadInstanceRepository = digitalReadInstanceRepository;
        }

        public async Task<List<DigitalReadInstance>> GetAllTagsValues(string id)
        {
            List<DigitalReadInstance> tags = await _digitalReadInstanceRepository.GetAllTagsValues(Guid.Parse(id));
            tags = tags.OrderBy(tag => tag.Value).ToList();

            return tags;
        }
        public async Task<List<DigitalReadInstance>> GetAllTagsInDateRange(DateTime start, DateTime end)
        {
            List<DigitalReadInstance> ari = await _digitalReadInstanceRepository.GetAllTagsInDateRange(start, end);
            ari = ari.OrderBy(tag => tag.Timestamp).ToList();
            return ari;

        }
    }
}