namespace SKADA.Models.Globals
{
    public class Globals
    {
        public static SemaphoreSlim _dBSemaphore = new SemaphoreSlim(1);
        public static int DeviceRefresh = 2000;
    }
}
