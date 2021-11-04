using System;
using abb_retrofill_powerbreak.retrofill;
using abb_retrofill_powerbreak.powerbreak;
using abb_retrofill_powerbreak.menu;
using System.Windows.Forms;

namespace abb_retrofill_powerbreak
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new load_form());
        }
    }
}
