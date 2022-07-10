namespace AspDotNetLab2.Services
{
    public class TimerService
    {
        public string GetTime() => System.DateTime.Now.ToString("hh:mm:ss");
    }
}
