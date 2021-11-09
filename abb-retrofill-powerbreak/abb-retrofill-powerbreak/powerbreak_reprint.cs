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
    public partial class powerbreak_reprint : Form
    {
        public string ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
        private label_files label_paths { get; set; }
        private Thread form_initialization_thread;
        public powerbreak_reprint()
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
                preview_label(picbox_retro, label_paths.powerbreak_label);
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
            label.Variables["ULFilePath"].SetValue(ul_path);
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

        private void ul_check_box_CheckedChanged(object sender, EventArgs e)
        {
            if (ul_check_box.Checked)
            {
                ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
            }
            else if (ul_check_box.Checked == false)
            {
                ul_path = "";
            }
            update_powerbreak_reprint_label();
        }

        private void update_powerbreak_reprint_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.powerbreak_label);
            label.Variables["InterruptingCapacity"].SetValue(txt_interrupt_cap.Text);
            label.Variables["CatalogNo"].SetValue(txt_cat_number.Text.Replace(" ", String.Empty));
            label.Variables["SerialNO"].SetValue(txt_seral_number.Text.Replace(" ", String.Empty));
            label.Variables["ConfigNO"].SetValue(txt_config_no.Text.Replace(" ", String.Empty));
            label.Variables["RatingPlug"].SetValue(txt_rating_plug.Text);
            label.Variables["AmpsMax"].SetValue(txt_amps_max.Text);
            label.Variables["SK240"].SetValue(txt_sk240.Text);
            label.Variables["SK480"].SetValue(txt_sk480.Text);
            label.Variables["SK600"].SetValue(txt_sk600.Text);
            label.Variables["ShortTime"].SetValue(txt_short_time.Text);
            label.Variables["NewDatecode"].SetValue(txt_new_dc.Text);
            label.Variables["OldDatecode"].SetValue(txt_old_dc.Text);
            label.Variables["ULFilePath"].SetValue(ul_path);
            label.Variables["IssueNO"].SetValue(txt_issue_no.Text);
            label.Variables["ULFilePath"].SetValue(ul_path);
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

        private void txt_interrupt_cap_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_cat_number_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_amps_max_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_config_no_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_rating_plug_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_sk240_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_sk480_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_sk600_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_issue_no_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_short_time_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_seral_number_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_old_dc_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void txt_new_dc_Leave(object sender, EventArgs e)
        {
            update_powerbreak_reprint_label();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var retrofill_side = new retrofill_side_reprint();
            Hide();
            retrofill_side.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var retrofill_unfuzed = new retrofill_un_fuzed_re_print();
            Hide();
            retrofill_unfuzed.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var retrofill_fuzed = new retrofill_reprint();
            Hide();
            retrofill_fuzed.Show();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            print_label();
        }

        private void print_label()
        {
            try
            {
                ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.powerbreak_label);
                label.PrintSettings.PrinterName = combo_printer.Text.Trim();
                label.Variables["InterruptingCapacity"].SetValue(txt_interrupt_cap.Text);
                label.Variables["CatalogNo"].SetValue(txt_cat_number.Text.Replace(" ", String.Empty));
                label.Variables["SerialNO"].SetValue(txt_seral_number.Text.Replace(" ", String.Empty));
                label.Variables["ConfigNO"].SetValue(txt_config_no.Text.Replace(" ", String.Empty));
                label.Variables["RatingPlug"].SetValue(txt_rating_plug.Text);
                label.Variables["AmpsMax"].SetValue(txt_amps_max.Text);
                label.Variables["SK240"].SetValue(txt_sk240.Text);
                label.Variables["SK480"].SetValue(txt_sk480.Text);
                label.Variables["SK600"].SetValue(txt_sk600.Text);
                label.Variables["ShortTime"].SetValue(txt_short_time.Text);
                label.Variables["NewDatecode"].SetValue(txt_new_dc.Text);
                label.Variables["OldDatecode"].SetValue(txt_old_dc.Text);
                label.Variables["ULFilePath"].SetValue(ul_path);
                label.Variables["IssueNO"].SetValue(txt_issue_no.Text);
                label.Variables["ULFilePath"].SetValue(ul_path);
                label.Print(1);
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
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

        private void button3_Click(object sender, EventArgs e)
        {
            var powerbreak_reprint = new powerbreak_reprint();
            Hide();
            powerbreak_reprint.Show();
        }
    }
}
