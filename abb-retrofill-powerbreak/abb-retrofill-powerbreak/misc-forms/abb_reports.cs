using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abb_retrofill_powerbreak.menu;
using abb_retrofill_powerbreak.retrofill;
using abb_retrofill_powerbreak.misc_forms;
using abb_retrofill_powerbreak.powerbreak;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;
using System.Threading;

namespace abb_retrofill_powerbreak.misc_forms
{
    public partial class abb_reports : Form
    {
        public DataTable report_data_list { get; set; }
        public string date_range { get; set; }
        public string start_date_range { get; set; }
        public string end_date_range { get; set; }
        public string report_type = "retrofill_ul";
        public bool all_date_range = false;
        private database report_class_action { get; set; }
        public abb_reports()
        {
            InitializeComponent();
            report_class_action = new database();
            start_date.Value = DateTime.Now;
            end_date.Value = DateTime.Now;
        }

        private void combo_report_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(combo_report.SelectedItem.ToString() == "Retrofill UL Count")
            {
                report_type = "retrofill_ul";
            }
            else if (combo_report.SelectedItem.ToString() == "Powerbreak UL Count")
            {
                report_type = "powerbreak_ul";
            }
            else if (combo_report.SelectedItem.ToString() == "Retrofill All")
            {
                report_type = "retro_all";
            }
                        else if (combo_report.SelectedItem.ToString() == "Powerbreak All")
            {
                report_type = "power_all";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            data_grid_report.DataSource = "";
            start_date_range = start_date.Value.ToString("yyyy-MM-dd");
            end_date_range = end_date.Value.ToString("yyyy-MM-dd");
            if (all_date_range == true)
            {
                date_range = "all";
            }
            lbl_report.Text = combo_report.SelectedItem.ToString();
            data_grid_report.DataSource = generate_report_data(report_type, date_range);
            styleDataGrid();
            sizeUpDataGrid();
            if (report_type == "retrofill_ul" || report_type == "powerbreak_ul")
            {
                lbl_ul_count.Text = Convert.ToString(report_data_list.Rows.Count);
            }
            else
            {
                lbl_ul_count.Text = "0";
            }
            this.Size = new Size(800, 861);
            pictureBox5.Visible = true;
            pictureBox6.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            lbl_report.Visible = true;
            lbl_ul_count.Visible = true;

            CenterToScreen();
        }
        private void form_initialize_function()
        {

        }
        private void sizeUpDataGrid()
        {
            data_grid_report.Columns[0].Width = 50;
            data_grid_report.Columns[1].Width = 190;
            data_grid_report.Columns[2].Width = 100;
            data_grid_report.Columns[3].Width = 50;
            data_grid_report.Columns[4].Width = 141;
        }
        private void styleDataGrid()
        {
            foreach (DataGridViewColumn c in data_grid_report.Columns)
            {
                c.DefaultCellStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
                c.DefaultCellStyle.BackColor = Color.White;
                c.DefaultCellStyle.ForeColor = Color.Black;
            }

            //SET UP HEADER STYLES
            DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
            columnHeaderStyle.ForeColor = Color.Black;
            columnHeaderStyle.Font = new Font("Segoe UI Semibold", 10, FontStyle.Bold);
            data_grid_report.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
        }

        public DataTable generate_report_data(string report_type,string date_range)
        {
            report_data_list = new DataTable();
            switch (report_type)
            {
                case "retrofill_ul":
                    if(all_date_range == true)
                    {
                        report_data_list = report_class_action.retrofill_ul_list_all();
                        break;
                    }
                    else
                    {
                        report_data_list = report_class_action.retrofill_ul_list_date_range(start_date_range, end_date_range);
                        break;
                    }
                case "powerbreak_ul":
                    if (all_date_range == true)
                    {
                        report_data_list = report_class_action.powerbreak_ul_list_all();
                        break;
                    }
                    else
                    {
                        report_data_list = report_class_action.powerbreak_ul_list_date_range(start_date_range, end_date_range);
                        break;
                    }
                case "retro_all":
                    if (all_date_range == true)
                    {
                        report_data_list = report_class_action.all_retrofill_print_history();
                        break;
                    }
                    break;
                case "power_all":
                    if (all_date_range == true)
                    {
                        report_data_list = report_class_action.powerbreak_list_all();
                        break;
                    }
                    break;
            }
            return report_data_list;
        }
        private void check_all_dates_CheckedChanged(object sender, EventArgs e)
        {
            if (check_all_dates.Checked)
            {
                all_date_range = true;
            }
            else
            {
                all_date_range = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void start_date_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            var main = new main_menu();
            Hide();
            main.Show();
        }

        private void btn_retro_Click(object sender, EventArgs e)
        {
            var retrofill = new retrofill.retrofill();
            Hide();
            retrofill.Show();
        }

        private void btn_interrupt_Click(object sender, EventArgs e)
        {
            var interrupts = new retrofill_interrupt_add();
            Hide();
            interrupts.Show();
        }

        private void btn_reports_Click(object sender, EventArgs e)
        {
            var reprints = new retrofill_reprint();
            Hide();
            reprints.Show();
        }
    }
}
