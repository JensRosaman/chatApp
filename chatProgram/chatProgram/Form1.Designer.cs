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
            btnStarta.Location = new Point(467, 305);
            btnStarta.Margin = new Padding(4, 5, 4, 5);
            btnStarta.Name = "btnStarta";
            btnStarta.Size = new Size(223, 100);
            btnStarta.TabIndex = 0;
            btnStarta.Text = "starta";
            btnStarta.UseVisualStyleBackColor = false;
            btnStarta.Click += btnStarta_Click;
            // 
            // tbxInkorg
            // 
            tbxInkorg.Location = new Point(467, 415);
            tbxInkorg.Margin = new Padding(4, 5, 4, 5);
            tbxInkorg.Name = "tbxInkorg";
            tbxInkorg.Size = new Size(221, 31);
            tbxInkorg.TabIndex = 1;
            // 
            // klienterRichtxtbx
            // 
            klienterRichtxtbx.Location = new Point(17, 20);
            klienterRichtxtbx.Margin = new Padding(4, 5, 4, 5);
            klienterRichtxtbx.Name = "klienterRichtxtbx";
            klienterRichtxtbx.Size = new Size(335, 707);
            klienterRichtxtbx.TabIndex = 2;
            klienterRichtxtbx.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Ivory;
            ClientSize = new Size(1143, 750);
            Controls.Add(klienterRichtxtbx);
            Controls.Add(tbxInkorg);
            Controls.Add(btnStarta);
            Margin = new Padding(4, 5, 4, 5);
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