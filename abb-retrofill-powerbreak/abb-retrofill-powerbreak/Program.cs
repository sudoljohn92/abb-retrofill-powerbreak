using System;
using abb_retrofill_powerbreak.retrofill;
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
            Application.Run(new retrofill.retrofill());
        }
    }
}
