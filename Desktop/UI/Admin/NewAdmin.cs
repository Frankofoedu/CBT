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
using System.IO;
using Newtonsoft.Json;

namespace rating.UI.Admin
{
    public partial class NewAdmin : UserControl
    {
        public NewAdmin()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var w = panel2.Controls.OfType<Bunifu.Framework.UI.BunifuMaterialTextbox>();

            foreach (var item in w)
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                {
                    item.Focus();
                    MessageBox.Show("Please fill all fields");
                    return;
                }
            }

            var usr = new User()
            {
                Email = txtEmail.Text,
                FullName = txtName.Text,
                PhoneNumber = txtNumber.Text,
                Password = txtPassword.Text,
                isAdmin = true
            };

            // var userString = JsonConvert.SerializeObject(usr);

            if (File.Exists(Constants.userFolderPath + "/users.json"))
            {

                var users = User.GetAllUsers();

                users.Add(usr);
                File.WriteAllText(Constants.userFolderPath + "/users.json", JsonConvert.SerializeObject(users));
            }
            else
            {
                File.Create(Constants.userFolderPath + "/users.json").Dispose();

                var users = User.GetAllUsers();

                if (users == null)
                {
                    File.WriteAllText(Constants.userFolderPath + "/users.json", JsonConvert.SerializeObject(new List<User> { usr }));
                }
                else
                {
                    users.Add(usr);

                    File.WriteAllText(Constants.userFolderPath + "/users.json", JsonConvert.SerializeObject(users));
                }

            }

            MessageBox.Show("Admin Added");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var w = panel2.Controls.OfType<Bunifu.Framework.UI.BunifuMaterialTextbox>();

            foreach (var item in w)
            {
                item.Text = "";
            }
        }
    }
}
