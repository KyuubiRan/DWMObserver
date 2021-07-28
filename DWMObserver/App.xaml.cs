using DWMObserver.Util;

namespace DWMObserver
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        private App()
        {
            ProcUtil.UpPrivilege();
        }
    }
}