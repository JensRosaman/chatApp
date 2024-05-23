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
            button1 = new Button();
            usernameTxtbox = new TextBox();
            label1 = new Label();
            button2 = new Button();
            FileBtn = new Button();
            SuspendLayout();
            // 
            // btnSkicka
            // 
            btnSkicka.Location = new Point(442, 323);
            btnSkicka.Name = "btnSkicka";
            btnSkicka.Size = new Size(75, 23);
            btnSkicka.TabIndex = 0;
            btnSkicka.Text = "Skicka";
            btnSkicka.UseVisualStyleBackColor = true;
            btnSkicka.Click += btnSkicka_Click;
            // 
            // txtSendBox
            // 
            txtSendBox.Location = new Point(267, 323);
            txtSendBox.Name = "txtSendBox";
            txtSendBox.Size = new Size(169, 23);
            txtSendBox.TabIndex = 1;
            // 
            // chatWin
            // 
            chatWin.DetectUrls = false;
            chatWin.Location = new Point(265, 141);
            chatWin.Name = "chatWin";
            chatWin.ReadOnly = true;
            chatWin.Size = new Size(276, 170);
            chatWin.TabIndex = 2;
            chatWin.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(466, 112);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 3;
            button1.Text = "Anslut";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // usernameTxtbox
            // 
            usernameTxtbox.Location = new Point(360, 112);
            usernameTxtbox.Name = "usernameTxtbox";
            usernameTxtbox.Size = new Size(100, 23);
            usernameTxtbox.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(265, 116);
            label1.Name = "label1";
            label1.Size = new Size(89, 15);
            label1.TabIndex = 5;
            label1.Text = "Användarnamn";
            // 
            // button2
            // 
            button2.Location = new Point(71, 141);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 6;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FileBtn
            // 
            FileBtn.Image = Properties.Resources.Skärmbild_2024_05_21_152925;
            FileBtn.Location = new Point(523, 317);
            FileBtn.Name = "FileBtn";
            FileBtn.Size = new Size(28, 34);
            FileBtn.TabIndex = 7;
            FileBtn.UseVisualStyleBackColor = true;
            FileBtn.Click += FileBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 70, 75);
            ClientSize = new Size(800, 450);
            Controls.Add(FileBtn);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(usernameTxtbox);
            Controls.Add(button1);
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
        private Button button1;
        private TextBox usernameTxtbox;
        private Label label1;
        private Button button2;
        public Button FileBtn;
    }
}