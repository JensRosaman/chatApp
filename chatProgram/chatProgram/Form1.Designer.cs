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
            tbxInkorg = new TextBox();
            klienterRichtxtbx = new RichTextBox();
            SuspendLayout();
            // 
            // btnStarta
            // 
            btnStarta.BackColor = SystemColors.ButtonFace;
            btnStarta.Location = new Point(327, 183);
            btnStarta.Name = "btnStarta";
            btnStarta.Size = new Size(156, 60);
            btnStarta.TabIndex = 0;
            btnStarta.Text = "starta";
            btnStarta.UseVisualStyleBackColor = false;
            btnStarta.Click += btnStarta_Click;
            // 
            // tbxInkorg
            // 
            tbxInkorg.Location = new Point(327, 249);
            tbxInkorg.Name = "tbxInkorg";
            tbxInkorg.Size = new Size(156, 23);
            tbxInkorg.TabIndex = 1;
            // 
            // klienterRichtxtbx
            // 
            klienterRichtxtbx.Location = new Point(12, 12);
            klienterRichtxtbx.Name = "klienterRichtxtbx";
            klienterRichtxtbx.Size = new Size(236, 426);
            klienterRichtxtbx.TabIndex = 2;
            klienterRichtxtbx.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.DarkOliveGreen;
            ClientSize = new Size(800, 450);
            Controls.Add(klienterRichtxtbx);
            Controls.Add(tbxInkorg);
            Controls.Add(btnStarta);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnStarta;
        private TextBox tbxInkorg;
        private RichTextBox klienterRichtxtbx;
    }
}