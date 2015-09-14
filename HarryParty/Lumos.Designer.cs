namespace WiiWandz
{
    partial class Lumos
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
            this.pbStrokes = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbStrokes)).BeginInit();
            this.SuspendLayout();
            // 
            // pbStrokes
            // 
            this.pbStrokes.BackColor = System.Drawing.Color.Black;
            this.pbStrokes.Location = new System.Drawing.Point(12, 21);
            this.pbStrokes.Name = "pbStrokes";
            this.pbStrokes.Size = new System.Drawing.Size(256, 192);
            this.pbStrokes.TabIndex = 44;
            this.pbStrokes.TabStop = false;
            // 
            // Lumos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::WiiWandz.Properties.Resources.bg_lumos_2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.pbStrokes);
            this.Name = "Lumos";
            this.Text = "Lumos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pbStrokes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbStrokes;
    }
}