namespace chatProgram
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
            btnStarta = new Button();
            clientsTbx = new RichTextBox();
            label1 = new Label();
            tbxInkorg = new RichTextBox();
            gdfggd = new Label();
            SuspendLayout();
            // 
            // btnStarta
            // 
            btnStarta.BackColor = SystemColors.ButtonFace;
            btnStarta.Location = new Point(320, 88);
            btnStarta.Name = "btnStarta";
            btnStarta.Size = new Size(333, 47);
            btnStarta.TabIndex = 0;
            btnStarta.Text = "Starta";
            btnStarta.UseVisualStyleBackColor = false;
            btnStarta.Click += btnStarta_Click;
            // 
            // clientsTbx
            // 
            clientsTbx.Location = new Point(12, 88);
            clientsTbx.Name = "clientsTbx";
            clientsTbx.Size = new Size(271, 341);
            clientsTbx.TabIndex = 2;
            clientsTbx.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 39);
            label1.Name = "label1";
            label1.Size = new Size(271, 46);
            label1.TabIndex = 3;
            label1.Text = "Anslutna klienter";
            // 
            // tbxInkorg
            // 
            tbxInkorg.Location = new Point(320, 187);
            tbxInkorg.Name = "tbxInkorg";
            tbxInkorg.Size = new Size(333, 242);
            tbxInkorg.TabIndex = 4;
            tbxInkorg.Text = "";
            // 
            // gdfggd
            // 
            gdfggd.AutoSize = true;
            gdfggd.Font = new Font("Segoe UI", 25F, FontStyle.Regular, GraphicsUnit.Point);
            gdfggd.Location = new Point(414, 138);
            gdfggd.Name = "gdfggd";
            gdfggd.Size = new Size(135, 46);
            gdfggd.TabIndex = 5;
            gdfggd.Text = "Historik";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Azure;
            ClientSize = new Size(800, 450);
            Controls.Add(gdfggd);
            Controls.Add(tbxInkorg);
            Controls.Add(label1);
            Controls.Add(clientsTbx);
            Controls.Add(btnStarta);
            Name = "Form1";
            Text = "Chatserver";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStarta;
        private RichTextBox clientsTbx;
        private Label label1;
        private RichTextBox tbxInkorg;
        private Label gdfggd;
    }
}