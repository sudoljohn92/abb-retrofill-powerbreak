using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using abb_retrofill_powerbreak.labels;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;
using NiceLabel.SDK;
using System.Reflection;

namespace abb_retrofill_powerbreak.powerbreak
{
    public partial class powerbreak_main : Form
    {
        public string char_2 { get; set; }
        public string sensor_size { get; set; }
        public string char_5 { get; set; }
        public string char_9 { get; set; }
        public string char_16 { get; set; }
        public string rating_plug { get; set; }
        public string interrupting_capacity { get; set; }
        public string amps_max { get; set; }
        public string sk240 { get; set; }
        public string sk480 { get; set; }
        public string sk600 { get; set; }
        public string hk240 { get; set; }
        public string hk480 { get; set; }
        public string hk600 { get; set; }
        public string short_time { get; set; }
        public string config_number { get; set; }
        public string generated_new_date_code { get; set; }
        public string issue_number { get; set; }
        public string ul_path { get; set; }
        private List<string> powerbreak_char_4_list { get; set; }
        private label_files powerbreak_label { get; set; }
        private database powerbreak_char_4_action { get; set; }
        private data_handler new_date_code { get; set; }
        public powerbreak_main()
        {
            InitializeComponent();
            InitializePrintEngine();
            powerbreak_label = new label_files();
            powerbreak_char_4_action = new database();
            new_date_code = new data_handler();
            generated_new_date_code = "RT" + new_date_code.generate_new_date_code();
            find_printers();
            read_issue_num();
            preview_label();
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

        private void find_printers()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(powerbreak_label.powerbreak_label);

             foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
             {
                 combo_printer.Items.Add(printer.ToString());
             }
            foreach (var item in combo_printer.Items)
            {
                if (item.ToString() == label.PrintSettings.PrinterName)
                {
                    combo_printer.SelectedItem = item.ToString();
                    break;
                }
                else
                {
                    combo_printer.Text = "Select Printer Here";
                }
            }
        }
        private void preview_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(powerbreak_label.powerbreak_label);
            label.Variables["NewDatecode"].SetValue(generated_new_date_code);
            label.Variables["IssueNO"].SetValue(issue_number);
            ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
            labelPreviewSettings.ImageFormat = "PNG";
            labelPreviewSettings.Width = this.picbox_powerbreak.Width;
            labelPreviewSettings.Height = this.picbox_powerbreak.Height;
            labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

            // Generate Preview File
            object imageObj = label.GetLabelPreview(labelPreviewSettings);

            // Display image in UI
            if (imageObj is byte[])
            {
                // When PrintToFiles = false
                // Convert byte[] to Bitmap and set as image source for PictureBox control
                this.picbox_powerbreak.Image = this.ByteToImage((byte[])imageObj);
            }
            else if (imageObj is string)
            {
                // When PrintToFiles = true
                this.picbox_powerbreak.ImageLocation = (string)imageObj;
            }
        }

        public void read_issue_num()
        {
            issue_number = System.IO.File.ReadAllText(@"C:\Projects\abb-retrofill-powerbreak\abb-retrofill-powerbreak\abb-retrofill-powerbreak\system_files\ul_issue_num.txt");
        }
        private void txt_config_Leave(object sender, EventArgs e)
        {
            parse_catalog_number(txt_cat_number.Text);
        }

        private void parse_catalog_number(string catalog_number)
        {
            //TSSD1C3F2N00G088
            config_number = txt_config.Text.Replace(" ", String.Empty);
            if(config_number.Substring(0,1) != "T")
            {
                config_number = "T" + txt_config.Text;
            }
            char_2 = config_number.Substring(1, 1);
            sensor_size = config_number.Substring(4, 1);
            char_5 = config_number.Substring(5, 1);
            char_9 = config_number.Substring(9, 1);
            char_16 = config_number.Substring(15, 1);
            generate_char_2_value(char_2);
            generate_char_4_data(sensor_size);
            rating_plug = powerbreak_char_4_action.generate_ratings_plug(char_5, char_9);
            ul_path = new_date_code.ul_viewable(char_16);
            update_powerbreak_label(); 
        }

        private void generate_char_4_data(string char_4)
        {
            powerbreak_char_4_list = powerbreak_char_4_action.find_powerbreak_char_4(char_4);
            amps_max = powerbreak_char_4_list[4];
            if (char_2 == "H")
            {
                sk240 = powerbreak_char_4_list[8];
                sk480 = powerbreak_char_4_list[9];
                sk600 = powerbreak_char_4_list[10];
            }
            else if (char_2 == "S")
            {
                sk240 = powerbreak_char_4_list[5];
                sk480 = powerbreak_char_4_list[6];
                sk600 = powerbreak_char_4_list[7];
            }
            short_time = powerbreak_char_4_list[11];
        }
        private void generate_char_2_value(string char_2)
        {
            switch (char_2)
            {
                case "H":
                    interrupting_capacity = "HI-BREAK CIRCUIT BREAKER";
                    break;
                case "S":
                    interrupting_capacity = "STD. BREAK CIRCUIT BREAKER";
                    break;
                default:
                    break;
            }
        }
        private void update_powerbreak_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(powerbreak_label.powerbreak_label);
            label.Variables["InterruptingCapacity"].SetValue(interrupting_capacity);
            label.Variables["CatalogNo"].SetValue(txt_cat_number.Text.Replace(" ",String.Empty));
            label.Variables["SerialNO"].SetValue(txt_serial_number.Text.Replace(" ", String.Empty));
            label.Variables["ConfigNO"].SetValue(txt_config.Text.Replace(" ", String.Empty));
            label.Variables["RatingPlug"].SetValue(rating_plug);
            label.Variables["AmpsMax"].SetValue(amps_max);
            label.Variables["SK240"].SetValue(sk240);
            label.Variables["SK480"].SetValue(sk480);
            label.Variables["SK600"].SetValue(sk600);
            label.Variables["ShortTime"].SetValue(short_time);
            label.Variables["NewDatecode"].SetValue("RT"+new_date_code.generate_new_date_code());
            label.Variables["ULFilePath"].SetValue(ul_path);
            label.Variables["IssueNO"].SetValue(issue_number);
            ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
            labelPreviewSettings.ImageFormat = "PNG";
            labelPreviewSettings.Width = this.picbox_powerbreak.Width;
            labelPreviewSettings.Height = this.picbox_powerbreak.Height;
            labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

            // Generate Preview File
            object imageObj = label.GetLabelPreview(labelPreviewSettings);

            // Display image in UI
            if (imageObj is byte[])
            {
                // When PrintToFiles = false
                // Convert byte[] to Bitmap and set as image source for PictureBox control
                this.picbox_powerbreak.Image = this.ByteToImage((byte[])imageObj);
            }
            else if (imageObj is string)
            {
                // When PrintToFiles = true
                this.picbox_powerbreak.ImageLocation = (string)imageObj;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void txt_cat_number_Leave(object sender, EventArgs e)
        {
            update_powerbreak_label();
        }

        private void txt_serial_number_Leave(object sender, EventArgs e)
        {
            update_powerbreak_label();
        }

        private void txt_old_dc_Leave(object sender, EventArgs e)
        {
            update_powerbreak_label();
        }

        private void ul_check_box_CheckedChanged(object sender, EventArgs e)
        {
            if (ul_check_box.Checked)
            {
                ul_path = @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
            }
            else if(ul_check_box.Checked == false)
            {
                ul_path = "";
            }
            update_powerbreak_label();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            print_label();
        }

        private void print_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(powerbreak_label.powerbreak_label);
            label.PrintSettings.PrinterName = combo_printer.Text.Trim();
            label.Variables["InterruptingCapacity"].SetValue(interrupting_capacity);
            label.Variables["CatalogNo"].SetValue(txt_cat_number.Text);
            label.Variables["SerialNO"].SetValue(txt_serial_number.Text);
            label.Variables["ConfigNO"].SetValue(txt_config.Text);
            label.Variables["RatingPlug"].SetValue(rating_plug);
            label.Variables["AmpsMax"].SetValue(amps_max);
            label.Variables["SK240"].SetValue(sk240);
            label.Variables["SK480"].SetValue(sk480);
            label.Variables["SK600"].SetValue(sk600);
            label.Variables["ShortTime"].SetValue(short_time);
            label.Variables["NewDatecode"].SetValue("RT" + new_date_code.generate_new_date_code());
            label.Variables["ULFilePath"].SetValue(ul_path);
            label.Variables["IssueNO"].SetValue(issue_number);
            label.Print(1);
        }
    }
}
