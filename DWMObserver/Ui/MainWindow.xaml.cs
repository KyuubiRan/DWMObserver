using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DWMObserver.Util;

namespace DWMObserver.Ui
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Loop();
        }

        private bool _notify = true;
        private Process _proc;

        private async void Loop()
        {
            while (true)
            {
                _proc = ProcUtil.Dwm;
                var mem = _proc?.PrivateMemorySize64 / 1024.0 / 1024.0 ?? 0.0;
                MemUsage.Text = $"{mem:F} Mib";
                if (mem > 1024.0)
                {
                    if (AutoMode.IsChecked ?? false)
                    {
                        _proc?.Kill();
                        _notify = true;
                    }
                    else if (_notify)
                    {
                        MemUsage.Foreground = new SolidColorBrush(Colors.Red);
                        var mbr = MessageBox.Show(caption: "检测到溢出",
                                                  messageBoxText: "检测到dwm.exe内存大于1Gib,是否立刻结束进程?",
                                                  button: MessageBoxButton.OKCancel);
                        if (mbr == MessageBoxResult.OK)
                        {
                            _proc?.Kill();
                        }
                        else
                        {
                            _notify = false;
                        }
                    }
                }
                else
                {
                    MemUsage.Foreground = new SolidColorBrush(Colors.Black);
                }

                await Task.Delay(5000);
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Terminate_Click(object sender, RoutedEventArgs e)
        {
            var mbr = MessageBox.Show(caption: "结束进程?",
                                      messageBoxText: "是否结束dwm.exe?",
                                      button: MessageBoxButton.OKCancel);
            if (mbr != MessageBoxResult.OK) return;
            if (_proc != null)
            {
                _proc.Kill();
            }
            else
            {
                ProcUtil.Dwm?.Kill();
            }

            _notify = true;
        }
    }
}