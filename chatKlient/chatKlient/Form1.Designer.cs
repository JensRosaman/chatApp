namespace chatKlient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSkicka = new Button();
            txtSendBox = new TextBox();
            chatWin = new RichTextBox();
            connectBtn = new Button();
            usernameTxtbox = new TextBox();
            label1 = new Label();
            FileBtn = new Button();
            portTxtbx = new TextBox();
            IPAdressTxtbx = new TextBox();
            label2 = new Label();
            label3 = new Label();
            SuspendLayout();
            // 
            // btnSkicka
            // 
            btnSkicka.Location = new Point(434, 374);
            btnSkicka.Name = "btnSkicka";
            btnSkicka.Size = new Size(73, 23);
            btnSkicka.TabIndex = 0;
            btnSkicka.Text = "Skicka";
            btnSkicka.UseVisualStyleBackColor = true;
            btnSkicka.Click += btnSkicka_Click;
            // 
            // txtSendBox
            // 
            txtSendBox.Location = new Point(225, 374);
            txtSendBox.Name = "txtSendBox";
            txtSendBox.Size = new Size(203, 23);
            txtSendBox.TabIndex = 1;
            // 
            // chatWin
            // 
            chatWin.DetectUrls = false;
            chatWin.Location = new Point(225, 127);
            chatWin.Name = "chatWin";
            chatWin.ReadOnly = true;
            chatWin.Size = new Size(316, 240);
            chatWin.TabIndex = 2;
            chatWin.Text = "";
            // 
            // connectBtn
            // 
            connectBtn.Location = new Point(466, 97);
            connectBtn.Name = "connectBtn";
            connectBtn.Size = new Size(75, 23);
            connectBtn.TabIndex = 3;
            connectBtn.Text = "Anslut";
            connectBtn.UseVisualStyleBackColor = true;
            connectBtn.Click += connectBtn_Click;
            // 
            // usernameTxtbox
            // 
            usernameTxtbox.Location = new Point(370, 98);
            usernameTxtbox.Name = "usernameTxtbox";
            usernameTxtbox.Size = new Size(89, 23);
            usernameTxtbox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(371, 80);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 5;
            label1.Text = "Användarnamn";
            // 
            // FileBtn
            // 
            FileBtn.Image = Properties.Resources.Skärmbild_2024_05_21_152925;
            FileBtn.Location = new Point(513, 374);
            FileBtn.Name = "FileBtn";
            FileBtn.Size = new Size(28, 24);
            FileBtn.TabIndex = 7;
            FileBtn.UseVisualStyleBackColor = true;
            FileBtn.Click += FileBtn_Click;
            // 
            // portTxtbx
            // 
            portTxtbx.Location = new Point(316, 98);
            portTxtbx.Name = "portTxtbx";
            portTxtbx.Size = new Size(48, 23);
            portTxtbx.TabIndex = 8;
            portTxtbx.Text = "12345";
            // 
            // IPAdressTxtbx
            // 
            IPAdressTxtbx.Location = new Point(224, 98);
            IPAdressTxtbx.Name = "IPAdressTxtbx";
            IPAdressTxtbx.Size = new Size(86, 23);
            IPAdressTxtbx.TabIndex = 9;
            IPAdressTxtbx.Text = "127.0.0.1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(224, 80);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 10;
            label2.Text = "IP-adress";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(316, 80);
            label3.Name = "label3";
            label3.Size = new Size(29, 15);
            label3.TabIndex = 11;
            label3.Text = "Port";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 70, 75);
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(IPAdressTxtbx);
            Controls.Add(portTxtbx);
            Controls.Add(FileBtn);
            Controls.Add(label1);
            Controls.Add(usernameTxtbox);
            Controls.Add(connectBtn);
            Controls.Add(chatWin);
            Controls.Add(txtSendBox);
            Controls.Add(btnSkicka);
            Name = "Form1";
            Text = "Discborb";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSkicka;
        private TextBox txtSendBox;
        private RichTextBox chatWin;
        private Button connectBtn;
        private TextBox usernameTxtbox;
        private Label label1;
        public Button FileBtn;
        private TextBox portTxtbx;
        private TextBox IPAdressTxtbx;
        private Label label2;
        private Label label3;
    }
}