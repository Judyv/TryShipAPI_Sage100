namespace TryShipAPI
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
            this.btShipShipment = new System.Windows.Forms.Button();
            this.btCreate = new System.Windows.Forms.Button();
            this.btShipSourceDocument = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.edSSPassword = new System.Windows.Forms.TextBox();
            this.edSSUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.edStarShipServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbLocationCode = new System.Windows.Forms.Label();
            this.edDevKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cbSSLocation = new System.Windows.Forms.ComboBox();
            this.ShipSourceByID = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btShipShipment
            // 
            this.btShipShipment.Location = new System.Drawing.Point(97, 236);
            this.btShipShipment.Margin = new System.Windows.Forms.Padding(4);
            this.btShipShipment.Name = "btShipShipment";
            this.btShipShipment.Size = new System.Drawing.Size(301, 47);
            this.btShipShipment.TabIndex = 5;
            this.btShipShipment.Text = "ShipShipment";
            this.btShipShipment.UseVisualStyleBackColor = true;
            this.btShipShipment.Click += new System.EventHandler(this.btStart_Click);
            // 
            // btCreate
            // 
            this.btCreate.Location = new System.Drawing.Point(97, 290);
            this.btCreate.Margin = new System.Windows.Forms.Padding(4);
            this.btCreate.Name = "btCreate";
            this.btCreate.Size = new System.Drawing.Size(301, 47);
            this.btCreate.TabIndex = 6;
            this.btCreate.Text = "CreateShipment/ShipShipment";
            this.btCreate.UseVisualStyleBackColor = true;
            this.btCreate.Click += new System.EventHandler(this.btCreate_Click);
            // 
            // btShipSourceDocument
            // 
            this.btShipSourceDocument.Location = new System.Drawing.Point(97, 345);
            this.btShipSourceDocument.Margin = new System.Windows.Forms.Padding(4);
            this.btShipSourceDocument.Name = "btShipSourceDocument";
            this.btShipSourceDocument.Size = new System.Drawing.Size(301, 47);
            this.btShipSourceDocument.TabIndex = 7;
            this.btShipSourceDocument.Text = "ShipSourceDocument";
            this.btShipSourceDocument.UseVisualStyleBackColor = true;
            this.btShipSourceDocument.Click += new System.EventHandler(this.btShipSourceDocument_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 138);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "Password";
            // 
            // edSSPassword
            // 
            this.edSSPassword.Location = new System.Drawing.Point(156, 132);
            this.edSSPassword.Margin = new System.Windows.Forms.Padding(4);
            this.edSSPassword.Name = "edSSPassword";
            this.edSSPassword.PasswordChar = '*';
            this.edSSPassword.Size = new System.Drawing.Size(205, 22);
            this.edSSPassword.TabIndex = 3;
            this.edSSPassword.TextChanged += new System.EventHandler(this.edSSPassword_TextChanged);
            // 
            // edSSUser
            // 
            this.edSSUser.Location = new System.Drawing.Point(156, 97);
            this.edSSUser.Margin = new System.Windows.Forms.Padding(4);
            this.edSSUser.Name = "edSSUser";
            this.edSSUser.Size = new System.Drawing.Size(205, 22);
            this.edSSUser.TabIndex = 2;
            this.edSSUser.TextChanged += new System.EventHandler(this.edSSUser_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 103);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 16;
            this.label2.Text = "User";
            // 
            // edStarShipServer
            // 
            this.edStarShipServer.Location = new System.Drawing.Point(156, 27);
            this.edStarShipServer.Margin = new System.Windows.Forms.Padding(4);
            this.edStarShipServer.Name = "edStarShipServer";
            this.edStarShipServer.Size = new System.Drawing.Size(205, 22);
            this.edStarShipServer.TabIndex = 0;
            this.edStarShipServer.TextChanged += new System.EventHandler(this.edStarShipServer_TextChanged);
            this.edStarShipServer.Leave += new System.EventHandler(this.edStarShipServer_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Server";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbLocationCode);
            this.groupBox1.Controls.Add(this.edDevKey);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cbSSLocation);
            this.groupBox1.Controls.Add(this.edSSUser);
            this.groupBox1.Controls.Add(this.edStarShipServer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.edSSPassword);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(493, 214);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "StarShip Settings";
            // 
            // lbLocationCode
            // 
            this.lbLocationCode.AutoSize = true;
            this.lbLocationCode.Location = new System.Drawing.Point(371, 69);
            this.lbLocationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbLocationCode.Name = "lbLocationCode";
            this.lbLocationCode.Size = new System.Drawing.Size(24, 17);
            this.lbLocationCode.TabIndex = 27;
            this.lbLocationCode.Text = "99";
            // 
            // edDevKey
            // 
            this.edDevKey.Location = new System.Drawing.Point(156, 166);
            this.edDevKey.Margin = new System.Windows.Forms.Padding(4);
            this.edDevKey.Name = "edDevKey";
            this.edDevKey.Size = new System.Drawing.Size(205, 22);
            this.edDevKey.TabIndex = 4;
            this.edDevKey.TextChanged += new System.EventHandler(this.edDevKey_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 172);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 17);
            this.label4.TabIndex = 26;
            this.label4.Text = "Developer Key";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(31, 69);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 17);
            this.label12.TabIndex = 24;
            this.label12.Text = "Location";
            // 
            // cbSSLocation
            // 
            this.cbSSLocation.FormattingEnabled = true;
            this.cbSSLocation.Location = new System.Drawing.Point(156, 62);
            this.cbSSLocation.Margin = new System.Windows.Forms.Padding(4);
            this.cbSSLocation.Name = "cbSSLocation";
            this.cbSSLocation.Size = new System.Drawing.Size(205, 24);
            this.cbSSLocation.TabIndex = 1;
            this.cbSSLocation.SelectedIndexChanged += new System.EventHandler(this.cbSSLocation_SelectedIndexChanged);
            // 
            // ShipSourceByID
            // 
            this.ShipSourceByID.Location = new System.Drawing.Point(97, 400);
            this.ShipSourceByID.Margin = new System.Windows.Forms.Padding(4);
            this.ShipSourceByID.Name = "ShipSourceByID";
            this.ShipSourceByID.Size = new System.Drawing.Size(301, 47);
            this.ShipSourceByID.TabIndex = 24;
            this.ShipSourceByID.Text = "ShipSourceDocumentByID";
            this.ShipSourceByID.UseVisualStyleBackColor = true;
            this.ShipSourceByID.Click += new System.EventHandler(this.ShipSourceByID_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(94, 463);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(72, 17);
            this.linkLabel1.TabIndex = 25;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "linkLabel1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 498);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.ShipSourceByID);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btShipSourceDocument);
            this.Controls.Add(this.btCreate);
            this.Controls.Add(this.btShipShipment);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Try Ship API ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btShipShipment;
        private System.Windows.Forms.Button btCreate;
        private System.Windows.Forms.Button btShipSourceDocument;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox edSSPassword;
        private System.Windows.Forms.TextBox edSSUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edStarShipServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cbSSLocation;
        private System.Windows.Forms.TextBox edDevKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbLocationCode;
        private System.Windows.Forms.Button ShipSourceByID;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

