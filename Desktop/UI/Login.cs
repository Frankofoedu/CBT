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

namespace rating.UI
{
    public partial class Login : UserControl
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var password = txtNumber.Text.Trim();
            if (!string.IsNullOrWhiteSpace(password))
            {
                if (password == "ADMIN1234")
                {
                    var f = new Admin.AdminPanel(true);


                    this.Parent.Controls.Add(f);

                    f.Dock = DockStyle.Fill;
                    f.Show();
                    f.BringToFront();

                    return;
                }
                var user = User.GetUser(password);

                if (user == null)
                {
                    MessageBox.Show("User does not exist");
                }
                else
                {
                    Constants.User = user;
                    CheckUserAdmin(user);             

                }
            }
            else
            {
                MessageBox.Show("Please enter your ID");
            }
        }
        void CheckUserAdmin(User u)
        {
            if (u.isAdmin)
            {
                var f = new Admin.AdminPanel(false);


                this.Parent.Controls.Add(f);

                f.Dock = DockStyle.Fill;
                f.Show();
                f.BringToFront();
            }
            else
            {
                if (u.isDone)
                {
                    MessageBox.Show("You have taken this exam");
                    return;
                }

                MessageBox.Show("Welcome. Click Ok to start Your exam");
                var f = new ExamControl();


                this.Parent.Controls.Add(f);

                f.Dock = DockStyle.Fill;
                f.Show();
                f.BringToFront();
            }



        }
    }

}
