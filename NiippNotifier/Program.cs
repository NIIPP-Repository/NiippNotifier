using System;
using System.Threading;
using System.Windows.Forms;

namespace NiippNotifier
{
    static class Program
    {
        private const string _appName = "niipp_notifier";
        private static Mutex _mtx;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            bool tryCreateNewApp;
            _mtx = new Mutex(true, _appName, out tryCreateNewApp);
            if (tryCreateNewApp)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Приложение уже запущено!");
            }
        }
    }
}
