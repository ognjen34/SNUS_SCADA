using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public class DigitalReadInstanceRepository : IDigitalReadInstanceRepository
    {

        protected AppDbContext _context;

        public DigitalReadInstanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DigitalReadInstance> Create(DigitalReadInstance digitalReadInstance)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                _context.DigitalReadInstances.Add(digitalReadInstance);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstance;
        }

        public async Task<DigitalReadInstance> Delete(Guid id)
        {
            DigitalReadInstance digitalReadInstance;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                digitalReadInstance = _context.DigitalReadInstances.FirstOrDefault(e => e.Id == id);
                if (digitalReadInstance != null)
                {
                    _context.DigitalReadInstances.Remove(digitalReadInstance);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstance;
        }

        public async Task<DigitalReadInstance> Get(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            DigitalReadInstance digitalReadInstance;
            try
            {
                digitalReadInstance = _context.DigitalReadInstances.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstance;
        }

        public async Task<DigitalReadInstance> GetByTagId(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            DigitalReadInstance digitalReadInstance;
            try
            {
                digitalReadInstance = _context.DigitalReadInstances.FirstOrDefault(e => e.TagId == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstance;
        }


        public async Task<IEnumerable<DigitalReadInstance>> GetAllSorted(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<DigitalReadInstance> digitalReadInstances;
            try
            {
                digitalReadInstances = _context.DigitalReadInstances
                    .Where(x => x.TagId == id)
                    .OrderByDescending(x => x.Timestamp)
                    .ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstances;
        }


        public async Task<IEnumerable<DigitalReadInstance>> GetAll()
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<DigitalReadInstance> digitalReadInstances;
            try
            {
                digitalReadInstances = _context.DigitalReadInstances.ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return digitalReadInstances;
        }

        public async Task<DigitalReadInstance> Update(DigitalReadInstance digitalReadInstance)
        {
            DigitalReadInstance oldInstance;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldInstance = _context.DigitalReadInstances.FirstOrDefault(e => e.Id == digitalReadInstance.Id);
                if (digitalReadInstance != null)
                {
                    _context.Entry(oldInstance).CurrentValues.SetValues(digitalReadInstance);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return oldInstance;
        }
    }
}
