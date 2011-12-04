using System;
using System.Windows.Forms;

namespace blue_patcher
{
    static class Program
    {
        public static Interface Interface { get; private set; }

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run((Interface = new Interface()));
        }
    }
}
