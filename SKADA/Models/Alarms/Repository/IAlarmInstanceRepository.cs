using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Repository
{
    public interface IAlarmInstanceRepository
    {
        Task<AlarmInstance> GetById(Guid id);
        Task Add(AlarmInstance alarm);
        Task Update(AlarmInstance alarm);
        Task Delete(AlarmInstance alarm);
    }
}
