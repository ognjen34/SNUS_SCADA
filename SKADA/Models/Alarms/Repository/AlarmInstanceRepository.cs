using SKADA.Models.Alarms.Model;

namespace SKADA.Models.Alarms.Repository
{
    public class AlarmInstanceRepository : IAlarmInstanceRepository
    {
        protected AppDbContext _context;

        public AlarmInstanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(AlarmInstance alarmInstance)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            try
            {
                _context.AlarmInstances.Add(alarmInstance);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task Delete(AlarmInstance alarmInstance)
        {
            AlarmInstance instanceToDelete;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                instanceToDelete = _context.AlarmInstances.FirstOrDefault(ai => ai.Id == alarmInstance.Id);
                if (instanceToDelete != null)
                {
                    _context.AlarmInstances.Remove(instanceToDelete);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task<AlarmInstance> GetById(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            AlarmInstance alarmInstance;
            try
            {
                alarmInstance = _context.AlarmInstances.FirstOrDefault(ai => ai.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return alarmInstance;
        }

        public async Task Update(AlarmInstance alarmInstance)
        {
            AlarmInstance oldAlarmInstance;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldAlarmInstance = _context.AlarmInstances.FirstOrDefault(ai => ai.Id == alarmInstance.Id);
                if (oldAlarmInstance != null)
                {
                    _context.Entry(oldAlarmInstance).CurrentValues.SetValues(alarmInstance);
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
