using System;
using abb_retrofill_powerbreak.retrofill;
using abb_retrofill_powerbreak.powerbreak;
using abb_retrofill_powerbreak.menu;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;

namespace abb_retrofill_powerbreak.misc_forms
{
    public partial class retrofill_interrupt_add : Form
    {
        public bool interrupt_bool { get; set; }
        private database database_class_action { get; set; }
        public retrofill_interrupt_add()
        {
            InitializeComponent();
            database_class_action = new database();
        }

        private void txt_b_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_b);
        }

        private void txt_c_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_c);
        }

        private void txt_d_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_d);
        }

        private void txt_e_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_e);
        }

        private void txt_f_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_f);
        }

        private void txt_g_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_g);
        }

        private void txt_h_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_h);
        }

        private void txt_i_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_i);
        }

        private void txt_j_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_j);
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            var menu = new main_menu();
            Hide();
            menu.Show();
        }

        private void btn_retro_Click(object sender, EventArgs e)
        {
            var retrofill = new retrofill.retrofill();
            Hide();
            retrofill.Show();
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            var reports = new abb_reports();
            Hide();
            reports.Show(); 
        }

        private void btn_pwbreak_Click(object sender, EventArgs e)
        {
            var powerbreak = new powerbreak_main();
            Hide();
            powerbreak.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txt_six_dig_Enter(object sender, EventArgs e)
        {
            txt_six_dig.Text = "";
        }

        private void txt_six_dig_TextChanged(object sender, EventArgs e)
        {
            change_text_to_non_italic(txt_six_dig);
        }

        private void change_text_to_non_italic(TextBox txtbox)
        {
            txtbox.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold))));
        }

        private void txt_b_Enter(object sender, EventArgs e)
        {
            txt_b.Text = "";
        }

        private void txt_c_Enter(object sender, EventArgs e)
        {
            txt_c.Text = "";
        }

        private void txt_d_Enter(object sender, EventArgs e)
        {
            txt_d.Text = "";
        }

        private void txt_e_Enter(object sender, EventArgs e)
        {
            txt_e.Text = "";
        }

        private void txt_f_Enter(object sender, EventArgs e)
        {
            txt_f.Text = "";
        }

        private void txt_g_Enter(object sender, EventArgs e)
        {
            txt_g.Text = "";
        }

        private void txt_h_Enter(object sender, EventArgs e)
        {
            txt_h.Text = "";
        }

        private void txt_i_Enter(object sender, EventArgs e)
        {
            txt_i.Text = "";
        }

        private void txt_j_Enter(object sender, EventArgs e)
        {
            txt_j.Text = "";
        }

        private void btn_enter_Click(object sender, EventArgs e)
        {
            interrupt_bool = database_class_action.add_interrupt_check(txt_six_dig.Text);
            if (interrupt_bool == false)
            {
                database_class_action.add_interrupt_values(txt_six_dig.Text, txt_b.Text, txt_c.Text, txt_d.Text, txt_e.Text, txt_f.Text, txt_g.Text, txt_h.Text, txt_i.Text, txt_j.Text);
                alert(txt_six_dig, "Interrupt Value Successfully Added!", 10000);
            }
            else
            {
                alert(txt_six_dig,"The Interrupt Value for the " + txt_six_dig.Text + " have been previously added", 10000);
            }
        }

        private void alert(TextBox txtbox,string message,Int32 time)
        {
            ToolTip tip = new ToolTip();
            tip.IsBalloon = true;
            tip.Show("The Interrupt Value for the " + txt_six_dig.Text + " have been previously added", txt_six_dig, 10000);
        }

        private void btn_free_forms_Click(object sender, EventArgs e)
        {
            var re_prints = new retrofill_reprint();
            Hide();
            re_prints.Show();
        }
    }
}
