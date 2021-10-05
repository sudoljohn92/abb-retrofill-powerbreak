using System;
using abb_retrofill_powerbreak.misc_forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace abb_retrofill_powerbreak
{
    public partial class load_form : Form
    {
        public load_form()
        {
            InitializeComponent();
            wait();
        }

        private async void wait()
        {
            loadForm();
            await Task.Delay(3000);
            openForm();
        }
        void fadeIn(object sender, EventArgs e)
        {
            if (Opacity >= 1)
                timer1.Stop();
            else
                Opacity += 0.05;
        }

        private void loadForm()
        {
            Opacity = 0;
            timer1.Interval = 10;
            timer1.Tick += new EventHandler(fadeIn);
            timer1.Start();
        }

        private void openForm()
        {
            var login = new login_form();
            Hide();
            login.Show();
        }
    }
}
