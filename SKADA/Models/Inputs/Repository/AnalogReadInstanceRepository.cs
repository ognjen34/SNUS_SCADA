
using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public class AnalogReadInstanceRepository : IAnalogReadInstanceRepository
    {
        protected AppDbContext _context;

        public AnalogReadInstanceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AnalogReadInstance> Create(AnalogReadInstance input)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                _context.AnalogReadInstance.Add(input);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<AnalogReadInstance> Get(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            AnalogReadInstance input;
            try
            {
                input = _context.AnalogReadInstance.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public Task<IEnumerable<AnalogReadInstance>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<AnalogReadInstance>> GetAllTagsInDateRange(DateTime start,DateTime end)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            try
            {
                var ari = _context.AnalogReadInstance
                    .Where(ai => ai.Timestamp >= start && ai.Timestamp <= end)
                    .ToList();

                return ari;
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
        }

        public async Task<List<AnalogReadInstance>> GetAllTagsValues(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            List<AnalogReadInstance> inputs;
            try
            {
                inputs = _context.AnalogReadInstance.Where(e => e.TagId == id).ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return inputs;
        }

        public async Task<AnalogReadInstance> GetByTag(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            AnalogReadInstance input;
            try
            {
                input = _context.AnalogReadInstance.FirstOrDefault(e => e.TagId == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public Task<AnalogReadInstance> GetByTagId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}