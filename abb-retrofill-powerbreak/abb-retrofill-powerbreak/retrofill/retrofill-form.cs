﻿using System;
using System.Globalization;
using SQLitePCL;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;
using System.Data.SQLite;
using abb_retrofill_powerbreak.menu;
using abb_retrofill_powerbreak.powerbreak;
using System.Collections.Generic;
using NiceLabel.SDK;
using abb_retrofill_powerbreak.labels;
using System.IO;
using System.Drawing;
using System.Reflection;

namespace abb_retrofill_powerbreak.retrofill
{
    public partial class retrofill : Form
    {
        public string first_six { get; set; }
        public string char_1 { get; set; }
        public string char_2 { get; set; }
        public string char_5 { get; set; }
        public string char_6 { get; set; }
        public string char_13 { get; set; }
        public string fuzed_or_unfuzed_char { get; set; }
        public string sensor_size { get; set; }
        public string frame_size { get; set; }
        public string poles = "3";
        public string interrupt_value_1 { get; set; }
        public string interrupt_value_2 { get; set; }
        public string side_label_cat_1 { get; set; }
        public string side_label_type_1 { get; set; }
        public string side_label_cat_2 { get; set; }
        public string side_label_type_2 { get; set; }
        public string side_label_cat_3 { get; set; }
        public string side_label_type_3 { get; set; }
        public bool isFuzed { get; set; }
        private retrofill_data_handler retro_data_handler { get; set; }
        private database dbConnection { get; set; }
        private label_files label_paths { get; set; }
        private unfuzed_values unfuzed_label_values { get; set; }
        public List<string> unfuzedlist { get; set; }
        public retrofill()
        {
            InitializeComponent();
            InitializePrintEngine();
            retro_data_handler = new retrofill_data_handler();
            dbConnection = new database();
            label_paths = new label_files();
            preview_label(picbox_retro, label_paths.retrofill_label);
            preview_label(picbox_side, label_paths.retrofill_side_label);
            preview_label(picbox_caution, label_paths.retrofill_caution_label);
            txt_new_dc.Text = generate_new_date_code();
        }


        private void parse_catalog_number(string catalog_number)
        {
            first_six = catalog_number.Substring(0, 6);
            char_1 = catalog_number.Substring(0, 1);
            char_2 = catalog_number.Substring(1, 1);
            fuzed_or_unfuzed_char = catalog_number.Substring(2, 1);
            sensor_size = catalog_number.Substring(3, 1);
            char_5 = catalog_number.Substring(4, 1);
            char_6 = catalog_number.Substring(5, 1);
            char_13 = catalog_number.Substring(12, 1);
        }

        private bool fuzed_or_unfuzed(string char_3)
        {
            int i = 0;
            bool result = int.TryParse(char_3, out i);
            if(result == false)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void find_printers()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_label);

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

        protected void CenterScreen()
        {
            Screen screen = Screen.FromControl(this);

            Rectangle workingArea = screen.WorkingArea;
            this.Location = new Point()
            {
                X = Math.Max(workingArea.X, workingArea.Y + (workingArea.Width - this.Width) / 2),
                Y = Math.Max(workingArea.Y, workingArea.Y + (workingArea.Height - this.Height) / 2)
            };
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

        private void preview_retro_face_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_label);
            label.Variables["CatNum"].SetValue(txt_catalog_number.Text);
            label.Variables["SerialNum"].SetValue(txt_sn.Text);
            label.Variables["ValQ"].SetValue(txt_ren_parts_bull.Text);
            label.Variables["ValK"].SetValue(txt_inst_book.Text);
            label.Variables["ValR"].SetValue(txt_outline_dwg.Text);
            label.Variables["MfgDC"].SetValue(txt_mfg_dc.Text);
            label.Variables["ValL"].SetValue(txt_new_dc.Text);
            label.Variables["ValA"].SetValue("3");
            label.Variables["ValC"].SetValue("600");
            label.Variables["ValF"].SetValue("100");
            label.Variables["ValP"].SetValue(frame_size);
            label.Variables["ValT"].SetValue(frame_size);
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

        private void preview_retro_fac_unfuzede_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofil_unfuzed_label);
            label.Variables["CatNum"].SetValue(txt_catalog_number.Text);
            label.Variables["SerialNum"].SetValue(txt_sn.Text);
            label.Variables["ValB"].SetValue(unfuzed_label_values.valB);
            label.Variables["ValC"].SetValue(unfuzed_label_values.valC);
            label.Variables["ValD"].SetValue(unfuzed_label_values.valD);
            label.Variables["ValE"].SetValue(unfuzed_label_values.valE);
            label.Variables["ValF"].SetValue(unfuzed_label_values.valF);
            label.Variables["ValG"].SetValue(unfuzed_label_values.valG);
            label.Variables["ValH"].SetValue(unfuzed_label_values.valH);
            label.Variables["ValI"].SetValue(unfuzed_label_values.valI);
            label.Variables["ValJ"].SetValue(unfuzed_label_values.valJ);
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

        private void preview_side_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_side_label);
            label.Variables["A9"].SetValue(side_label_cat_1);
            label.Variables["A43"].SetValue(side_label_type_1);
            label.Variables["A10"].SetValue(side_label_cat_2);
            label.Variables["A44"].SetValue(side_label_type_2);
            label.Variables["A11"].SetValue(side_label_cat_3);
            label.Variables["A45"].SetValue(side_label_type_3);
            label.Variables["A35"].SetValue(txt_breaker_type.Text);
            label.Variables["A36"].SetValue(frame_size);
            label.Variables["A37"].SetValue("3");
            label.Variables["A38"].SetValue(txt_interrupt_at_508.Text);
            label.Variables["W"].SetValue(first_six);
            label.Variables["P"].SetValue(char_13);
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

        private void preview_caution_label()
        {
            ILabel label = PrintEngineFactory.PrintEngine.OpenLabel(label_paths.retrofill_caution_label);
            label.Variables["Caution_Line1"].SetValue(txt_caution_1.Text);
            label.Variables["Caution_Line2"].SetValue(txt_caution_2.Text);
            ILabelPreviewSettings labelPreviewSettings = new LabelPreviewSettings();
            labelPreviewSettings.ImageFormat = "PNG";
            labelPreviewSettings.Width = this.picbox_caution.Width;
            labelPreviewSettings.Height = this.picbox_caution.Height;
            labelPreviewSettings.FormatPreviewSide = FormatPreviewSide.FrontSide;

            // Generate Preview File
            object imageObj = label.GetLabelPreview(labelPreviewSettings);

            // Display image in UI
            if (imageObj is byte[])
            {
                // When PrintToFiles = false
                // Convert byte[] to Bitmap and set as image source for PictureBox control
                this.picbox_caution.Image = this.ByteToImage((byte[])imageObj);
            }
            else if (imageObj is string)
            {
                // When PrintToFiles = true
                this.picbox_caution.ImageLocation = (string)imageObj;
            }
        }
        private void txt_catalog_number_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                parse_catalog_number(txt_catalog_number.Text);

                //True is Fuzed and False is UnFuzed
                isFuzed = fuzed_or_unfuzed(fuzed_or_unfuzed_char);
                if (isFuzed == true)
                {
                    /*
                     * TEST
                     * R3FLH2HERXR4GXXL4XXO
                     * 
                     * R5XXG1HERXR4GXXL4XXO
                     * R6XXP1HERXR4GXXL4XXO
                     * VARIABLES FFOR FUUZED FASCIA R
                     * VAL A - 3
                     * VAL C - 600 static with fuzed
                     * VAL F - 100 static with fuzed
                     * VAL K - TEXTBOX
                     * VAL L - DATECODE
                     * VAL P - 800A char 5 sensor frame size
                     * VAL R - TEXTBOX
                     * VAL T - 800A char 5 sensor frame size
                     * VAL Q - TEXTBOX
                     */
                    frame_size = retro_data_handler.find_fuzed_frame_sensor(char_5);
                    sensor_size = frame_size;
                    interrupt_value_1 = "600";
                    interrupt_value_2 = "100";
                    List<string> char_list = dbConnection.find_side_label_values(char_13);
                    for (int i = 0; i < char_list.Count; i++)
                    {
                        side_label_cat_1 = char_list[1].ToString();
                        side_label_type_1 = char_list[2].ToString();

                        side_label_cat_2 = char_list[3].ToString();
                        side_label_type_2 = char_list[4].ToString();

                        side_label_cat_3 = char_list[5].ToString();
                        side_label_type_3 = char_list[6].ToString();
                    }
                    //MessageBox.Show(frame_size + sensor_size);
                    //MessageBox.Show(side_label_cat_1 + " " + side_label_type_1 + " " + side_label_cat_2 + " " + " " + side_label_type_2 + " " + " " + side_label_cat_3 + " " + " " + side_label_type_3 + " ");
                    preview_retro_face_label();
                    preview_side_label();
                }
                else if (isFuzed == false)
                {
                    /*
                     * R304I1HERXR4GXXL4XXO
                     * * VAL A - 3
                     * VAL L - DATECODE
                     * VAL P - 800A char 5 sensor frame size
                     * VAL R - TEXTBOX
                     * VAL T - 800A char 5 sensor frame size
                     * VAL Q - TEXTBOX
                     * VAL B - USER DEFINED
                     * VAL C - USER DEFINED
                     * VAL D - USER DEFINED
                     * VAL E - USER DEFINED
                     * VAL F - USER DEFINED
                     * VAL G - USER DEFINED
                     * VAL H - USER DEFINED
                     * VAL I - USER DEFINED
                     * VAL J - USER DEFINED
                     * VAL K - USER DEFINED
                     * INSERT FUNCTION TO FIND ALL THIS DATA
                     */
                    unfuzed_label_values = new unfuzed_values();
                    unfuzedlist = dbConnection.find_unfuzed_values(first_six);
                    if (unfuzedlist.Count > 0)
                    {
                        frame_size = retro_data_handler.find_fuzed_frame_sensor(char_5);
                        sensor_size = frame_size;
                        List<string> char_list = dbConnection.find_side_label_values(char_13);
                        for (int i = 0; i < char_list.Count; i++)
                        {
                            side_label_cat_1 = char_list[1].ToString();
                            side_label_type_1 = char_list[2].ToString();

                            side_label_cat_2 = char_list[3].ToString();
                            side_label_type_2 = char_list[4].ToString();

                            side_label_cat_3 = char_list[5].ToString();
                            side_label_type_3 = char_list[6].ToString();
                        }
                        add_unfuzed_values();
                        preview_retro_fac_unfuzede_label();
                        preview_side_label();

                    }
                    else if(unfuzedlist.Count == 0)
                    {
                        MessageBox.Show("Error: Interrupt Values have not been defined. Please add the Interrupt Values in via the 'Add Interrupt Feature'");
                    }
                    //MessageBox.Show(frame_size + sensor_size);
                    //MessageBox.Show(side_label_cat_1 + " " + side_label_type_1 + " " + side_label_cat_2 + " " + " " + side_label_type_2 + " " + " " + side_label_cat_3 + " " + " " + side_label_type_3 + " ");
                }
            }
        }

        private void add_unfuzed_values()
        {
            unfuzed_label_values.valB = unfuzedlist[0].ToString();
            unfuzed_label_values.valC = unfuzedlist[1].ToString();
            unfuzed_label_values.valD = unfuzedlist[2].ToString();
            unfuzed_label_values.valE = unfuzedlist[3].ToString();
            unfuzed_label_values.valF = unfuzedlist[4].ToString();
            unfuzed_label_values.valG = unfuzedlist[5].ToString();
            unfuzed_label_values.valH = unfuzedlist[6].ToString();
            unfuzed_label_values.valI = unfuzedlist[7].ToString();
            unfuzed_label_values.valJ = unfuzedlist[8].ToString();
        }
        private string generate_new_date_code()
        {
            string year_a = DateTime.Now.ToString("yyyy").Substring(3, 1);
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            string week_of_year = Convert.ToString(cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday));
            string year_b = calculate_decade_code(DateTime.Now.ToString("yyyy").Substring(2, 1));
            return "RO" + year_a + week_of_year + year_b;
        }

        private string calculate_decade_code(string decade_code)
        {
            switch (decade_code)
            {
                case "4":
                    return "&";
                case "3":
                    return "#";
                case "2":
                    return "@";
                case "1":
                    return "!";
                case "0":
                    return "&";
            }
            return "";
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            preview_retro_face_label();
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            preview_retro_face_label();
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            preview_retro_face_label();
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            preview_retro_face_label();
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            preview_retro_face_label();
        }

        private void txt_breaker_type_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_interrupt_at_508_Leave(object sender, EventArgs e)
        {
            preview_side_label();
        }

        private void txt_caution_1_Leave(object sender, EventArgs e)
        {
            preview_caution_label();
        }

        private void txt_caution_2_Leave(object sender, EventArgs e)
        {
            preview_caution_label();
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

        private void btn_pwbreak_Click(object sender, EventArgs e)
        {
            var powerbreak = new powerbreak_main();
            Hide();
            powerbreak.Show();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            if (check_retro.Checked)
            {
                MessageBox.Show("Print Main Retro");
            }

            if (check_side.Checked)
            {
                MessageBox.Show("Print Side");
            }

            if (check_caution.Checked)
            {
                MessageBox.Show("Print Caution");
            }
        }
    }
}
