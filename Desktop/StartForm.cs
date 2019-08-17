using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rating
{
    public partial class StartForm : Form
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public StartForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.CenterToScreen();

            logger.Error("This is an error message");
        }
    }
}
