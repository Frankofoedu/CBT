﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using rating.Data;
using System.IO;
using NLog;

namespace rating.UI.Admin
{
    public partial class AdminPanel : UserControl
    {
        private readonly HttpClient client = new HttpClient();

        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        public AdminPanel(bool superAdmin)
        {
            InitializeComponent();

            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
            {
                if (superAdmin)
                {
                    btnAddAdmin.Visible = true;

                }

                cmbZone.DataSource = Constants.ListCentres.Keys.ToList();
                cmbZone.SelectedItem = null;
                cmbZone.SelectedText = "--select--";

            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            var frm = new NewUser();

            this.Parent.Controls.Add(frm);

            frm.BringToFront();
            frm.Dock = DockStyle.Fill;

            //this.Dispose();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            // client.Dispose();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            var frm = new NewAdmin();

            this.Parent.Controls.Add(frm);

            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var frm = new AllUser();

            this.Parent.Controls.Add(frm);

            frm.BringToFront();
            frm.Dock = DockStyle.Fill;
        }

        private async void Button3_ClickAsync(object sender, EventArgs e)
        {
            var centre = cmbCentre.SelectedItem;

            if (centre == null)
            {
                MessageBox.Show("Please select a center");
                return;
            }
            try
            {
                
                Cursor = Cursors.WaitCursor;
                var response = await client.GetAsync("http://cbt.insyt.com.ng/api/UsersByCentre/" + centre.ToString());//, new StringContent(str, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var jsonObject = await response.Content.ReadAsStringAsync();

                    var users = JsonConvert.DeserializeObject<List<User>>(jsonObject);
                    if (users.Count < 1)
                    {
                        MessageBox.Show("No users registered");
                        return;
                    }

                    File.WriteAllText(Constants.userPath, jsonObject);

                   
                    MessageBox.Show("User data has been collected");

                }
                else
                {
                    MessageBox.Show("No User has been registered online");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
                MessageBox.Show("An error occured with the network. Please try again");            
            }
            Cursor = Cursors.Default;
        }

        private void CmbZone_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var x = cmbZone.SelectedItem;

            if (x != null)
            {
                var centres = Constants.ListCentres[x.ToString()];

                cmbCentre.DataSource = centres;
            }

        }

        private async void BtnSyncResults_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
           var results = Directory.EnumerateFiles(Constants.ResultFolderPath);

            if (results != null)
            {
                int i = 0;
                int j = 0;
                foreach (var resultFile in results)
                {
                    var result = File.ReadAllText(resultFile);

                    using (var client = new HttpClient())
                    {
                        var response = await client.PostAsync("http://cbt.insyt.com.ng/api/UserAnswers", new StringContent(result, Encoding.UTF8, "application/json"));

                        if (response.IsSuccessStatusCode)
                        {
                            i++;
                        }
                        else
                        {
                            j++;
                        }
                    }

                   
                }
                MessageBox.Show($"{i.ToString()} Results sent. {j.ToString()} failed");
            }
            else
            {
                MessageBox.Show("No results in this system");
            }

            Cursor = Cursors.Default;
        }
    }
}
