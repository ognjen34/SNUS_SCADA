using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public class AnalogInputRepository : IAnalogInputRepository
    {
        protected AppDbContext _context;

        public AnalogInputRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AnalogInput> Create(AnalogInput input)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                _context.AnalogInput.Add(input);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<AnalogInput> Delete(Guid id)
        {
            AnalogInput input;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                input = _context.AnalogInput.FirstOrDefault(e => e.Id == id);
                if (input != null)
                {
                    _context.AnalogInput.Remove(input);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<AnalogInput> Get(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            AnalogInput input;
            try
            {
                input = _context.AnalogInput.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<IEnumerable<AnalogInput>> GetAll()
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<AnalogInput> allInputs;
            try
            {
                allInputs = _context.AnalogInput.ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return allInputs;
        }

        public async Task<IEnumerable<AnalogInput>> GetByIOAddress(string IOAddress)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<AnalogInput> allInputs;
            try
            {
                allInputs = _context.AnalogInput.ToList().Where(input => input.IOAddress == IOAddress); 
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return allInputs;
        }

        public async  Task<AnalogInput> Update(AnalogInput input)
        {
            AnalogInput oldInput;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldInput = _context.AnalogInput.FirstOrDefault(e => e.Id == input.Id);
                if (input != null)
                {
                    _context.Entry(oldInput).CurrentValues.SetValues(input);
                    _context.SaveChanges();
                    await Task.Delay(1);
                }
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return oldInput;
        }
    }
}
