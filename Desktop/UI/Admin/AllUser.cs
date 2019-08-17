using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using rating.Data;

namespace rating.UI.Admin
{
    public partial class AllUser : UserControl
    {
        public AllUser()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                var users = User.GetAllUsers();

                userBindingSource.DataSource = users;                 
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
