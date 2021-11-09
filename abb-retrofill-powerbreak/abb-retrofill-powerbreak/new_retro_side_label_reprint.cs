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
    public partial class new_retro_side_label_reprint : Form
    {
        private label_files label_paths { get; set; }
        private Thread form_initialization_thread;
        public new_retro_side_label_reprint()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
