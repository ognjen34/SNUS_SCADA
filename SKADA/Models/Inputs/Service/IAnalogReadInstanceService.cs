using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IAnalogReadInstanceService
    {
        Task<AnalogReadInstance> Get(Guid id);
        Task<AnalogReadInstance> GetByTagId(Guid id);
        Task Create(AnalogReadInstance input);
        Task<List<AnalogReadInstance>> GetAllTagsValues(string id);
        Task<List<AnalogReadInstance>> GetAllTagsInDateRange(DateTime start, DateTime end);
    }
}
