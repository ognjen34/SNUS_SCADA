using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Service
{
    public interface IDigitalReadInstanceService
    {
        
        Task<List<DigitalReadInstance>> GetAllTagsValues(string id);
        Task<List<DigitalReadInstance>> GetAllTagsInDateRange(DateTime start, DateTime end);

    }
}
