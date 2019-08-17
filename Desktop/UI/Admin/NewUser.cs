using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using rating.Data;
using System.IO;
using NLog;

namespace rating.UI
{
    public partial class NewUser : UserControl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public NewUser()
        {
            InitializeComponent();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            try
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

                var pwd = GeneratePassword();
                var usr = new User()
                {
                    Address = txtAddress.Text,
                    Email = txtEmail.Text,
                    FullName = txtName.Text,
                    PhoneNumber = txtNumber.Text,
                    Password = pwd,
                    isAdmin = false,
                    isDone = false,
                    TimeLeft = 3600

                };

                label6.Text = "Password: " + Environment.NewLine + pwd;


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

                MessageBox.Show("User Added");
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString()); 
            }          

        }

        string GeneratePassword()
        {
            var pwdgen = new RNDPassword();
            return "ANAMBRA" + pwdgen.RandomPassword(10);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var w = panel2.Controls.OfType<Bunifu.Framework.UI.BunifuMaterialTextbox>();

            foreach (var item in w)
            {
                
                    item.Text = "";
                
            }
            label6.Text = "";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
