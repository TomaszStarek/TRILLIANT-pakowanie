
namespace WindowsFormsApp5
{
    partial class QrForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelNapisZekanujDoEps = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.labelQrPartNumber = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(193, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(500, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelNapisZekanujDoEps
            // 
            this.labelNapisZekanujDoEps.AutoSize = true;
            this.labelNapisZekanujDoEps.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNapisZekanujDoEps.Location = new System.Drawing.Point(26, 7);
            this.labelNapisZekanujDoEps.Name = "labelNapisZekanujDoEps";
            this.labelNapisZekanujDoEps.Size = new System.Drawing.Size(848, 42);
            this.labelNapisZekanujDoEps.TabIndex = 1;
            this.labelNapisZekanujDoEps.Text = "Zeskanuj do EPS aby nadać krok PAKOWANIA";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button1.Location = new System.Drawing.Point(754, 529);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 72);
            this.button1.TabIndex = 2;
            this.button1.Text = "Potwierdzam zeskanowanie";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelQrPartNumber
            // 
            this.labelQrPartNumber.AutoSize = true;
            this.labelQrPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelQrPartNumber.Location = new System.Drawing.Point(28, 101);
            this.labelQrPartNumber.Name = "labelQrPartNumber";
            this.labelQrPartNumber.Size = new System.Drawing.Size(118, 29);
            this.labelQrPartNumber.TabIndex = 3;
            this.labelQrPartNumber.Text = "Część 0/2";
            // 
            // QrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 662);
            this.Controls.Add(this.labelQrPartNumber);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelNapisZekanujDoEps);
            this.Controls.Add(this.pictureBox1);
            this.Name = "QrForm";
            this.Text = "QrForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelNapisZekanujDoEps;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelQrPartNumber;
    }
}