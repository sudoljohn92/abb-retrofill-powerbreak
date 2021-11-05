using System;
using abb_retrofill_powerbreak.powerbreak;
using abb_retrofill_powerbreak.retrofill;
using abb_retrofill_powerbreak.misc_forms;
using System.Windows.Forms;

namespace abb_retrofill_powerbreak.menu
{
    public partial class main_menu : Form
    {
        public main_menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_power_break_Click(object sender, EventArgs e)
        {
            var powerbreak = new powerbreak.powerbreak_main();
            Hide();
            powerbreak.Show();
        }

        private void btn_retro_fill_Click(object sender, EventArgs e)
        {
            var retrofill = new retrofill.retrofill();
            Hide();
            retrofill.Show();
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            var powerbreak = new powerbreak_main();
            Hide();
            powerbreak.Show();
        }

        private void btn_retro_Click(object sender, EventArgs e)
        {
            var retrofill = new retrofill.retrofill();
            Hide();
            retrofill.Show();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btn_interrupt_Click(object sender, EventArgs e)
        {
            var add_interrupts = new misc_forms.retrofill_interrupt_add();
            Hide();
            add_interrupts.Show();
        }
    }
}
