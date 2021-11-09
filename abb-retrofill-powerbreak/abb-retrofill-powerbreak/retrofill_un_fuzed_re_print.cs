using System;
using System.Globalization;
using SQLitePCL;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;
using System.Data.SQLite;
using abb_retrofill_powerbreak.menu;
using abb_retrofill_powerbreak.powerbreak;
using abb_retrofill_powerbreak.misc_forms;
using System.Collections.Generic;
using NiceLabel.SDK;
using abb_retrofill_powerbreak.labels;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Threading;
namespace abb_retrofill_powerbreak
{
    public partial class retrofill_un_fuzed_re_print : Form
    {
        public string ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
        private label_files label_paths { get; set; }
        private Thread form_initialization_thread;
        public retrofill_un_fuzed_re_print()
        {
            InitializeComponent();
            form_initialization_thread = new Thread(new ThreadStart(form_initialize_function));
            form_initialization_thread.IsBackground = true;
            form_initialization_thread.Start();
        }

        private void form_initialize_function()
        {
            try
            {
                CheckForIllegalCrossThreadCalls = false;
                load_pic_box.Visible = true;
                circularProgressBar2.Visible = true;
                InitializePrintEngine();
                label_paths = new label_files();
                find_printers(combo_printer, label_paths.powerbreak_label);
                preview_label(picbox_retro, label_paths.retrofill_label);
                load_pic_box.Visible = false;
                circularProgressBar2.Visible = false;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }
        private void InitializePrintEngine()
        {
            try
            {
                string sdkFilesPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "..\\..\\..\\SDKFiles");
                if (Directory.Exists(sdkFilesPath))
                {
                    PrintEngineFactory.SDKFilesPath = sdkFilesPath;
                }
                PrintEngineFactory.PrintEngine.Initialize();
            }
            catch (SDKException exception)
            {
                MessageBox.Show("Initialization of the SDK failed." + Environment.NewLine + Environment.NewLine + exception.ToString());
                Application.Exit();
            }
        }

        private Bitmap ByteToImage(byte[] bytes)
        {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(bytes, 0, Convert.ToInt32(bytes.Length));
            Bitmap bm = new Bitmap(memoryStream, false);
            memoryStream.Dispose();
            return bm;
        }

        private void preview_label(PictureBox picbox, string label_path)
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_path);
            label.Variables["ul_path"].SetValue(ul_path);
            ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
            labelPreviewSettings.ImageFormat = "PNG";
            labelPreviewSettings.Width = picbox.Width;
            labelPreviewSettings.Height = picbox.Height;
            labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

            // Generate Preview File
            object imageObj = label.GetLabelPreview(labelPreviewSettings);

            // Display image in UI
            if (imageObj is byte[])
            {
                // When PrintToFiles = false
                // Convert byte[] to Bitmap and set as image source for PictureBox control
                picbox.Image = this.ByteToImage((byte[])imageObj);
            }
            else if (imageObj is string)
            {
                // When PrintToFiles = true
                picbox.ImageLocation = (string)imageObj;
            }
        }

        private void find_printers(ComboBox printer_combo, string label_file)
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_file);

            foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                printer_combo.Items.Add(printer.ToString());
            }
            foreach (var item in printer_combo.Items)
            {
                if (item.ToString() == label.PrintSettings.PrinterName)
                {
                    printer_combo.SelectedItem = item.ToString();
                    break;
                }
                else
                {
                    printer_combo.Text = "Select Printer Here";
                }
            }
        }

        private void preview_retro_fac_unfuzede_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofil_unfuzed_label);
            label.Variables["CatNum"].SetValue(txt_catalog_number.Text);
            label.Variables["SerialNum"].SetValue(txt_sn.Text);
            label.Variables["ValB"].SetValue(txt_val_b.Text);
            label.Variables["ValC"].SetValue(txt_val_c.Text);
            label.Variables["ValD"].SetValue(txt_val_d.Text);
            label.Variables["ValE"].SetValue(txt_val_e.Text);
            label.Variables["ValF"].SetValue(txt_val_f.Text);
            label.Variables["ValG"].SetValue(txt_val_g.Text);
            label.Variables["ValH"].SetValue(txt_val_h.Text);
            label.Variables["ValI"].SetValue(txt_val_i.Text);
            label.Variables["ValJ"].SetValue(txt_val_j.Text);
            label.Variables["ValP"].SetValue(txt_val_p.Text);
            label.Variables["ValT"].SetValue(txt_val_t.Text);
            label.Variables["ul_path"].SetValue(ul_path);
            ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
            labelPreviewSettings.ImageFormat = "PNG";
            labelPreviewSettings.Width = this.picbox_retro.Width;
            labelPreviewSettings.Height = this.picbox_retro.Height;
            labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

            // Generate Preview File
            object imageObj = label.GetLabelPreview(labelPreviewSettings);

            // Display image in UI
            if (imageObj is byte[])
            {
                // When PrintToFiles = false
                // Convert byte[] to Bitmap and set as image source for PictureBox control
                this.picbox_retro.Image = this.ByteToImage((byte[])imageObj);
            }
            else if (imageObj is string)
            {
                // When PrintToFiles = true
                this.picbox_retro.ImageLocation = (string)imageObj;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }



        private void print_unfuzed_retrofill()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofil_unfuzed_label);
            label.PrintSettings.PrinterName = combo_printer.Text.Trim();
            label.Variables["CatNum"].SetValue(txt_catalog_number.Text);
            label.Variables["SerialNum"].SetValue(txt_sn.Text);
            label.Variables["ValB"].SetValue(txt_val_b.Text);
            label.Variables["ValC"].SetValue(txt_val_c.Text);
            label.Variables["ValD"].SetValue(txt_val_d.Text);
            label.Variables["ValE"].SetValue(txt_val_e.Text);
            label.Variables["ValF"].SetValue(txt_val_f.Text);
            label.Variables["ValG"].SetValue(txt_val_g.Text);
            label.Variables["ValH"].SetValue(txt_val_h.Text);
            label.Variables["ValI"].SetValue(txt_val_i.Text);
            label.Variables["ValJ"].SetValue(txt_val_j.Text);
            label.Variables["ValP"].SetValue(txt_val_p.Text);
            label.Variables["ValT"].SetValue(txt_val_t.Text);
            label.Variables["ul_path"].SetValue(ul_path);
            label.Print(1);
        }

        private void ul_check_box_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txt_catalog_number_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void ul_check_box_CheckedChanged_1(object sender, EventArgs e)
        {
            if (ul_check_box.Checked)
            {
                ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
            }
            else if (ul_check_box.Checked == false)
            {
                ul_path = "";
            }
            preview_retro_fac_unfuzede_label();
        }

        private void txt_sn_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_ren_parts_bull_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_inst_book_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_outline_dwg_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_mfg_dc_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_new_dc_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_p_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_t_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_b_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_c_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_d_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_e_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_h_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_g_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_f_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_i_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void txt_val_j_Leave(object sender, EventArgs e)
        {
            preview_retro_fac_unfuzede_label();
        }

        private void btn_print_Click_1(object sender, EventArgs e)
        {
            print_unfuzed_retrofill();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var retrofill_fuzed = new retrofill_reprint();
            Hide();
            retrofill_fuzed.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var retrofill_side = new retrofill_side_reprint();
            Hide();
            retrofill_side.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var powerbreak_reprint = new powerbreak_reprint();
            Hide();
            powerbreak_reprint.Show();
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
            var interrupt_values = new misc_forms.retrofill_interrupt_add();
            Hide();
            interrupt_values.Show();
        }
    }
}
