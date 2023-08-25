using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Repository
{
    public interface IAlarmRepository
    {
        Task<Alarm> GetById(Guid id);
        Task Add(Alarm alarm);
        Task Update(Alarm alarm);
        Task Delete(Alarm alarm);
    }
}
