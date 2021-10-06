using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SQLite;
namespace abb_retrofill_powerbreak.data_handlers
{
    class database
    {
        public List<string> char_13_list { get; set; }
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
    }
}
