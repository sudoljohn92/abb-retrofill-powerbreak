using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abb_retrofill_powerbreak.data_handlers
{
    class data_handler
    {
        public string generate_new_date_code()
        {
            string year_a = DateTime.Now.ToString("yyyy").Substring(3, 1);
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            string week_of_year = Convert.ToString(cal.GetWeekOfYear(DateTime.Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday));
            string year_b = calculate_decade_code(DateTime.Now.ToString("yyyy").Substring(2, 1));
            return year_a + week_of_year + year_b;
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
        public string ul_viewable(string char_16)
        {
            switch (char_16)
            {
                case "4":
                    return "";
                case "5":
                    return "";
                case "C":
                    return "";
                case "D":
                    return "";
                case "L":
                    return "";
                case "M":
                    return "";
                case "V":
                    return "";
                case "W":
                    return "";
                default:
                    return @"C:\Projects\abb-retrofill-powerbreak\images\CuL.jpg";
            }
        }
    }


    class retrofill_data_handler
    {
        public string find_fuzed_frame_sensor(string char_5)
        {
            switch (char_5)
            {
                case "0":
                    return "5000A";
                case "1":
                    return "3200A";
                case "2":
                    return "3200A";
                case "3":
                    return "3200A";
                case "4":
                    return "3200A";
                case "5":
                    return "4000A";
                case "6":
                    return "4000A";
                case "7":
                    return "4000A";
                case "8":
                    return "4000A";
                case "9":
                    return "5000A";
                case "A":
                    return "225A";
                case "B":
                    return "225A";
                case "C":
                    return "600A";
                case "D":
                    return "600A";
                case "E":
                    return "600A";
                case "F":
                    return "600A";
                case "G":
                    return "800A";
                case "H":
                    return "800A";
                case "I":
                    return "800A";
                case "J":
                    return "800A";
                case "K":
                    return "800A";
                case "L":
                    return "800A";
                case "M":
                    return "1600A";
                case "N":
                    return "1600A";
                case "O":
                    return "1600A";
                case "P":
                    return "1600A";
                case "Q":
                    return "1600A";
                case "R":
                    return "1600A";
                case "S":
                    return "1600A";
                case "T":
                    return "2000A";
                case "U":
                    return "2000A";
                case "V":
                    return "2000A";
                case "W":
                    return "2000A";
                case "X":
                    return "3000A";
                case "Y":
                    return "3000A";
                case "Z":
                    return "3000A";
            }
            return "error";
        }
    }
}
