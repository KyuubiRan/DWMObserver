using System.Diagnostics;
using System.Linq;

namespace DWMObserver.Util
{
    public static class ProcUtil
    {
        public static Process Dwm => GetProcess();

        private static Process GetProcess()
        {
            var processes = Process.GetProcessesByName("dwm");
            return processes.FirstOrDefault(proc => proc.ProcessName == "dwm");
        }

        public static void UpPrivilege()
        {
            Process.EnterDebugMode();
        }
    }
}