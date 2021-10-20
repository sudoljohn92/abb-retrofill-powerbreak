using System;
using SQLitePCL;
using abb_retrofill_powerbreak.data_handlers;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections.Generic;

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
        public retrofill()
        {
            InitializeComponent();
            retro_data_handler = new retrofill_data_handler();
            dbConnection = new database();
        }

        private void btn_submit_Click(object sender, EventArgs e)
        {
            parse_catalog_number(txt_catalog_number.Text);

            //True is Fuzed and False is UnFuzed
            isFuzed = fuzed_or_unfuzed(fuzed_or_unfuzed_char);
            if(isFuzed == true)
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
                for(int i = 0; i < char_list.Count; i++)
                {
                    side_label_cat_1 = char_list[1].ToString();
                    side_label_type_1 = char_list[2].ToString();

                    side_label_cat_2 = char_list[3].ToString();
                    side_label_type_2 = char_list[4].ToString();

                    side_label_cat_3 = char_list[5].ToString();
                    side_label_type_3 = char_list[6].ToString();
                }
                MessageBox.Show(frame_size + sensor_size);
                MessageBox.Show(side_label_cat_1 + " " + side_label_type_1 + " " + side_label_cat_2 + " " + " " + side_label_type_2 + " " + " " + side_label_cat_3 + " " + " " + side_label_type_3 + " ");
            }
            else if(isFuzed == false)
            {
                /*
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
                MessageBox.Show(frame_size + sensor_size);
                MessageBox.Show(side_label_cat_1 + " " + side_label_type_1 + " " + side_label_cat_2 + " " + " " + side_label_type_2 + " " + " " + side_label_cat_3 + " " + " " + side_label_type_3 + " ");
            }
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
    }
}
