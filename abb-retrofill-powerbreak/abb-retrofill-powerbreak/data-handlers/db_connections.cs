using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SQLite;
using System;

namespace abb_retrofill_powerbreak.data_handlers
{
    class database
    {
        public List<string> char_4_power_list { get; set; }
        public List<string> char_13_list { get; set; }
        public List<string> unfuzed_list { get; set; }
        public string sql_lite_connection = "Data Source=C:\\Projects\\abb-retrofill-powerbreak\\abb-retrofill-powerbreak\\abb-retrofill-powerbreak\\abb-power-retro-db.db;";


        public List<string> find_side_label_values(string char_13)
        {
            char_13_list = new List<string>();
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT * FROM 'side-label-db' WHERE char_13 LIKE @char_13", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@char_13", char_13);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for(int i = 0; i < 7; i++)
                            {
                                char_13_list.Add(reader[i].ToString());
                            }
                        }
                    }
                }
            }
            return char_13_list;
        }
        public List<string> find_unfuzed_values(string first_six)
        {
            unfuzed_list = new List<string>();
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT b,c,d,e,f,g,h,i,j FROM interrupt_values_db WHERE six_digit_value LIKE @first_six", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@first_six", first_six);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < 9; i++)
                            {
                                unfuzed_list.Add(reader[i].ToString());
                            }
                        }
                    }
                }
            }
            return unfuzed_list;
        }

        public List<string> find_powerbreak_char_4(string char_4)
        {
            char_4_power_list = new List<string>();
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT * FROM tbl_Rating_4 WHERE char_4 LIKE @char4", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@char4", char_4);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < 12; i++)
                            {
                                char_4_power_list.Add(reader[i].ToString());
                            }
                        }
                    }
                }
            }
            return char_4_power_list;
        }

        public string generate_ratings_plug(string char_5,string char_9)
        {
            string char_5_value = find_char_5_ratings_plug_value(char_5);
            string char_9_value = find_char_9_ratings_plug_value(char_9);
            return char_5_value + char_9_value;
        }

        public string find_char_5_ratings_plug_value(string char_5)
        {
            string char_5_ratings_val = "";
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT tu_designation FROM tblTUtype_5 WHERE char_5 LIKE @char5", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@char5", char_5);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            char_5_ratings_val = reader[0].ToString();
                        }
                    }
                }
            }
            return char_5_ratings_val;
        }

        public string find_char_9_ratings_plug_value(string char_9)
        {
            string char_9_ratings_val = "";
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT ratings_plug FROM tbl_TU_rating_9 WHERE char_9 LIKE @char9", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@char9", char_9);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            char_9_ratings_val = reader[0].ToString();
                        }
                    }
                }
            }
            return char_9_ratings_val;
        }

        public void archive_powerbreak(string config, string serial_no,string cat,string rp,string sk240, string sk480, string sk600, string st, bool ul)
        {
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("INSERT INTO powerbreak_ul_history (catalog_no,serial_no,config_no,rating_plug,sk_hk_240,sk_hk_480,sk_hk_600,short_time,ul,print_date) VALUES(@cat,@serial_no,@cf,@rp,@sk240,@sk480,@sk600,@st,@ul,@pd)", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@cat", cat);
                    cmd.Parameters.AddWithValue("@serial_no", serial_no);
                    cmd.Parameters.AddWithValue("@cf", config);
                    cmd.Parameters.AddWithValue("@rp", rp); 
                    cmd.Parameters.AddWithValue("@sk240", sk240);
                    cmd.Parameters.AddWithValue("@sk480", sk480);
                    cmd.Parameters.AddWithValue("@sk600", sk600);
                    cmd.Parameters.AddWithValue("@st", st);
                    cmd.Parameters.AddWithValue("@ul", Convert.ToString(ul));
                    cmd.Parameters.AddWithValue("@pd", DateTime.Now.ToShortDateString());
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch(SQLiteException er)
                    {
                        MessageBox.Show(er.Message.ToString());
                    }
                }
            }
        }

        public void archive_retrofill(string cat,bool ul_value,string serial_number)
        {
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("INSERT INTO retrofill_ul_history (cat_number,date_time_stamp,ul,serial_number) VALUES(@cat,@datetime,@ul,@sn)", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@cat", cat);
                    cmd.Parameters.AddWithValue("@datetime", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ul", Convert.ToString(ul_value));
                    cmd.Parameters.AddWithValue("@sn", serial_number);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException er)
                    {
                        MessageBox.Show(er.Message.ToString());
                    }
                }
            }
        }

        public void add_interrupt_values(string six,string b,string c,string d, string e,string f, string g,string h,string i, string j)
        {
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("INSERT INTO interrupt_values_db (six_digit_value,b,c,d,e,f,g,h,i,j) VALUES(@six,@b,@c,@d,@e,@f,@g,@h,@i,@j)", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@six", six);
                    cmd.Parameters.AddWithValue("@b", b);
                    cmd.Parameters.AddWithValue("@c", c);
                    cmd.Parameters.AddWithValue("@d", d);
                    cmd.Parameters.AddWithValue("@e", e);
                    cmd.Parameters.AddWithValue("@f", f);
                    cmd.Parameters.AddWithValue("@g", g);
                    cmd.Parameters.AddWithValue("@h", h);
                    cmd.Parameters.AddWithValue("@i", i);
                    cmd.Parameters.AddWithValue("@j", j);
                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException er)
                    {
                        MessageBox.Show(er.Message.ToString());
                    }
                }
            }
        }

        public bool add_interrupt_check(string six)
        {
            bool found_sn = false;
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT six_digit_value FROM interrupt_values_db WHERE six_digit_value LIKE @six", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@six", six);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            found_sn = true;
                        }
                    }
                    connection.Close();
                }
            }
            return found_sn;
        }

        public bool serial_number_check_powerbreak(string sn)
        {
            bool found_sn = false;
            using (var connection = new SQLiteConnection(sql_lite_connection))
            {
                using (var cmd = new SQLiteCommand("SELECT * FROM powerbreak_ul_history WHERE serial_no LIKE @sn", connection))
                {
                    connection.Open();
                    cmd.Parameters.AddWithValue("@sn", sn);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if(reader.HasRows)
                        { 
                            found_sn = true;
                        }
                    }
                    connection.Close();
                }
            }
            return found_sn;
        }
    }
}
