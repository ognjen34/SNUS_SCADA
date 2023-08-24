using SKADA.Models.Inputs.Model;

namespace SKADA.Models.Inputs.Repository
{
    public class DigitalInputRepository : IDigitalInputRepository
    {
        protected AppDbContext _context;

        public DigitalInputRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DigitalInput> Create(DigitalInput input)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                _context.DigitalInput.Add(input);
                _context.SaveChanges();
                await Task.Delay(1);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<DigitalInput> Delete(Guid id)
        {
            DigitalInput input;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                input = _context.DigitalInput.FirstOrDefault(e => e.Id == id);
                if (input != null)
                {
                    _context.DigitalInput.Remove(input);
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

        public async Task<DigitalInput> Get(Guid id)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            DigitalInput input;
            try
            {
                input = _context.DigitalInput.FirstOrDefault(e => e.Id == id);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return input;
        }

        public async Task<IEnumerable<DigitalInput>> GetAll()
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<DigitalInput> allInputs;
            try
            {
                allInputs = _context.DigitalInput.ToList();
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return allInputs;
        }

        public async Task<IEnumerable<DigitalInput>> GetByIOAddress(string IOAddress)
        {
            await Globals.Globals._dBSemaphore.WaitAsync();
            IEnumerable<DigitalInput> allInputs;
            try
            {
                allInputs = _context.DigitalInput.ToList().Where(input=>input.IOAddress == IOAddress);
            }
            finally
            {
                Globals.Globals._dBSemaphore.Release();
            }
            return allInputs;
        }

        public async Task<DigitalInput> Update(DigitalInput input)
        {
            DigitalInput oldInput;
            await Globals.Globals._dBSemaphore.WaitAsync();

            try
            {
                oldInput = _context.DigitalInput.FirstOrDefault(e => e.Id == input.Id);
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
