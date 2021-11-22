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
    public partial class retrofill_side_reprint : Form
    {
        public string ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
        private label_files label_paths { get; set; }
        private Thread form_initialization_thread;
        public retrofill_side_reprint()
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
                find_printers(combo_side, label_paths.retrofill_side_label);
                preview_label(picbox_side, label_paths.retrofill_side_label);
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

        private void preview_side_label()
        {
            try
            {


                ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_side_label);
                label.Variables["A9"].SetValue(txt_cat_1.Text);
                label.Variables["A43"].SetValue(txt_type_1.Text);
                label.Variables["A10"].SetValue(txt_cat_2.Text);
                label.Variables["A44"].SetValue(txt_type_2.Text);
                label.Variables["A11"].SetValue(txt_cat_3.Text);
                label.Variables["A45"].SetValue(txt_type_3.Text);
                label.Variables["A35"].SetValue(txt_a_35.Text);
                label.Variables["A36"].SetValue(txt_a_36.Text);
                label.Variables["val_37"].SetValue(txt_a_37.Text);
                label.Variables["val_38"].SetValue(txt_a_38.Text);
                label.Variables["val_w"].SetValue(txt_val_w.Text);
                label.Variables["P"].SetValue(txt_val_p.Text);
                ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
                labelPreviewSettings.ImageFormat = "PNG";
                labelPreviewSettings.Width = this.picbox_side.Width;
                labelPreviewSettings.Height = this.picbox_side.Height;
                labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

                // Generate Preview File
                object imageObj = label.GetLabelPreview(labelPreviewSettings);

                // Display image in UI
                if (imageObj is byte[])
                {
                    // When PrintToFiles = false
                    // Convert byte[] to Bitmap and set as image source for PictureBox control
                    this.picbox_side.Image = this.ByteToImage((byte[])imageObj);
                }
                else if (imageObj is string)
                {
                    // When PrintToFiles = true
                    this.picbox_side.ImageLocation = (string)imageObj;
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message.ToString());
            }
        }

        private void print_side_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_side_label);
            label.PrintSettings.PrinterName = combo_side.Text.Trim();
            label.Variables["A9"].SetValue(txt_cat_1.Text);
            label.Variables["A43"].SetValue(txt_type_1.Text);
            label.Variables["A10"].SetValue(txt_cat_2.Text);
            label.Variables["A44"].SetValue(txt_type_2.Text);
            label.Variables["A11"].SetValue(txt_cat_3.Text);
            label.Variables["A45"].SetValue(txt_type_3.Text);
            label.Variables["A35"].SetValue(txt_a_35.Text);
            label.Variables["A36"].SetValue(txt_a_36.Text);
            label.Variables["val_37"].SetValue(txt_a_37.Text);
            label.Variables["val_38"].SetValue(txt_a_38.Text);
            label.Variables["val_w"].SetValue(txt_val_w.Text);
            label.Variables["P"].SetValue(txt_val_p.Text);
            label.Print(1);
        }

        private void txt_cat_1_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_type_1_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_cat_2_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_type_2_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_cat_3_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_type_3_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_val_p_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_val_w_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_a_35_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_a_36_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_a_37_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_a_38_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
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

        private void button1_Click(object sender, EventArgs e)
        {
            var retrofill_fuzed = new retrofill_reprint();
            Hide();
            retrofill_fuzed.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var retrofill_unfuzed = new retrofill_un_fuzed_re_print();
            Hide();
            retrofill_unfuzed.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var powerbreak_reprint = new powerbreak_reprint();
            Hide();
            powerbreak_reprint.Show();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {

        }
    }
}
