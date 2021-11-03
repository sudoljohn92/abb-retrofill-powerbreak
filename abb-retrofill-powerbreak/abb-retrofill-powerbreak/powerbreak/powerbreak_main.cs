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
        public string char_9 { get; set; }
        public string interrupting_capacity { get; set; }
        public string amps_max { get; set; }
        public string sk240 { get; set; }
        public string sk480 { get; set; }
        public string sk600 { get; set; }
        public string hk240 { get; set; }
        public string hk480 { get; set; }
        public string hk600 { get; set; }
        private List<string> powerbreak_char_4_list { get; set; }
        private label_files powerbreak_label { get; set; }
        private database powerbreak_char_4_action { get; set; }
        public powerbreak_main()
        {
            InitializeComponent();
            InitializePrintEngine();
            powerbreak_label = new label_files();
            powerbreak_char_4_action = new database();
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

        private void preview_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(powerbreak_label.powerbreak_label);
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

        private void txt_cat_number_Leave(object sender, EventArgs e)
        {
            parse_catalog_number(txt_cat_number.Text);
        }

        private void parse_catalog_number(string catalog_number)
        {
            char_2 = catalog_number.Substring(1, 1);
            sensor_size = catalog_number.Substring(3, 1);
            char_9 = catalog_number.Substring(8, 1);
            generate_char_2_value(char_2);
            generate_char_4_data(sensor_size);
            update_powerbreak_label();
        }

        private void generate_char_4_data(string char_4)
        {
            powerbreak_char_4_list = powerbreak_char_4_action.find_powerbreak_char_4(char_4);
            amps_max = powerbreak_char_4_list[4];
            sk240 = powerbreak_char_4_list[5];
            sk480 = powerbreak_char_4_list[6];
            sk600 = powerbreak_char_4_list[7];
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
            label.Variables["CatalogNo"].SetValue(txt_cat_number.Text);
            label.Variables["SerialNO"].SetValue(txt_serial_number.Text);
            label.Variables["ConfigNO"].SetValue(txt_config.Text);
            label.Variables["AmpsMax"].SetValue(txt_cat_number.Text);
            label.Variables["SK240"].SetValue(sk240);
            label.Variables["SK480"].SetValue(sk480);
            label.Variables["SK600"].SetValue(sk600);
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
    }
}
