using System;
using System.Windows.Forms;

namespace EU4_Province_Creator
{
    internal static class Program
    {
        private static MainWindow _mainWindow;
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                _mainWindow = new MainWindow();
                Application.Run(_mainWindow);
            }
            catch (Exception e)
            {
                File.WriteAllText("error.txt", e.ToString());
                throw;
            }
        }

        public static void EnableMainWindowForm()
        {
            _mainWindow.Enabled = true;
        }
    }
}
