using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using abb_retrofill_powerbreak.menu;
using System.Windows.Forms;

namespace abb_retrofill_powerbreak.misc_forms
{
    public partial class login_form : Form
    {
        public login_form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var main = new main_menu();
            Hide();
            main.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
