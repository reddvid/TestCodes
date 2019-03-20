namespace RPNWebsiteTools
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvChannels = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lvTwitter = new System.Windows.Forms.ListView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lvFacebook = new System.Windows.Forms.ListView();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.lblUpdate = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvChannels);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 286);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "YouTube";
            // 
            // lvChannels
            // 
            this.lvChannels.HideSelection = false;
            this.lvChannels.Location = new System.Drawing.Point(6, 19);
            this.lvChannels.Name = "lvChannels";
            this.lvChannels.Size = new System.Drawing.Size(265, 261);
            this.lvChannels.TabIndex = 2;
            this.lvChannels.UseCompatibleStateImageBehavior = false;
            this.lvChannels.DoubleClick += new System.EventHandler(this.lvChannels_DoubleClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(187, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Update Stats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lvTwitter);
            this.groupBox2.Location = new System.Drawing.Point(295, 41);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(277, 286);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Twitter";
            // 
            // lvTwitter
            // 
            this.lvTwitter.HideSelection = false;
            this.lvTwitter.Location = new System.Drawing.Point(6, 19);
            this.lvTwitter.Name = "lvTwitter";
            this.lvTwitter.Size = new System.Drawing.Size(265, 261);
            this.lvTwitter.TabIndex = 2;
            this.lvTwitter.UseCompatibleStateImageBehavior = false;
            this.lvTwitter.DoubleClick += new System.EventHandler(this.lvTwitter_DoubleClick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lvFacebook);
            this.groupBox3.Location = new System.Drawing.Point(578, 41);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(431, 286);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Facebook";
            // 
            // lvFacebook
            // 
            this.lvFacebook.HideSelection = false;
            this.lvFacebook.Location = new System.Drawing.Point(6, 19);
            this.lvFacebook.Name = "lvFacebook";
            this.lvFacebook.Size = new System.Drawing.Size(419, 261);
            this.lvFacebook.TabIndex = 2;
            this.lvFacebook.UseCompatibleStateImageBehavior = false;
            this.lvFacebook.DoubleClick += new System.EventHandler(this.lvFacebook_DoubleClick);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(RPNWebsiteTools.Form1);
            // 
            // lblUpdate
            // 
            this.lblUpdate.AutoSize = true;
            this.lblUpdate.Location = new System.Drawing.Point(205, 17);
            this.lblUpdate.Name = "lblUpdate";
            this.lblUpdate.Size = new System.Drawing.Size(10, 13);
            this.lblUpdate.TabIndex = 5;
            this.lblUpdate.Text = " ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 339);
            this.Controls.Add(this.lblUpdate);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "RPN Social Media";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView lvChannels;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvTwitter;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lvFacebook;
        private System.Windows.Forms.Label lblUpdate;
    }
}

