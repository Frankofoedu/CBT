namespace rating
{
    partial class StartForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.newUser1 = new rating.UI.NewUser();
            this.login1 = new rating.UI.Login();
            this.SuspendLayout();
            // 
            // newUser1
            // 
            this.newUser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newUser1.Location = new System.Drawing.Point(0, 0);
            this.newUser1.Name = "newUser1";
            this.newUser1.Size = new System.Drawing.Size(713, 450);
            this.newUser1.TabIndex = 0;
            // 
            // login1
            // 
            this.login1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.login1.Location = new System.Drawing.Point(0, 0);
            this.login1.Name = "login1";
            this.login1.Size = new System.Drawing.Size(771, 495);
            this.login1.TabIndex = 0;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 495);
            this.Controls.Add(this.login1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StartForm";
            this.Text = "Exam App";
            this.ResumeLayout(false);

        }

        #endregion

        private UI.NewUser newUser1;
        private UI.Login login1;
    }
}