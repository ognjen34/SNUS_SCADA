using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Repository
{
    public class AlarmRepository : IAlarmRepository
    {
        protected AppDbContext _context;

        public AlarmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Alarm alarm)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            try
            {
                _context.Alarms.Add(alarm);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task Delete(Alarm alarm)
        {
            Alarm alarmToDelete;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                alarmToDelete = _context.Alarms.FirstOrDefault(a => a.Id == alarm.Id);
                if (alarmToDelete != null)
                {
                    _context.Alarms.Remove(alarmToDelete);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task<Alarm> GetById(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            Alarm alarm;
            try
            {
                alarm = _context.Alarms.FirstOrDefault(a => a.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return alarm;
        }

        public async Task Update(Alarm alarm)
        {
            Alarm oldAlarm;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldAlarm = _context.Alarms.FirstOrDefault(a => a.Id == alarm.Id);
                if (oldAlarm != null)
                {
                    _context.Entry(oldAlarm).CurrentValues.SetValues(alarm);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }
    }
}
