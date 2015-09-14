namespace WiiWandz
{
    partial class Hedwig
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
            this.pbStrokes.Location = new System.Drawing.Point(14, 35);
            this.pbStrokes.Name = "pbStrokes";
            this.pbStrokes.Size = new System.Drawing.Size(256, 192);
            this.pbStrokes.TabIndex = 44;
            this.pbStrokes.TabStop = false;
            this.pbStrokes.Visible = false;
            // 
            // Hedwig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::WiiWandz.Properties.Resources.bg_hedwig;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.pbStrokes);
            this.Name = "Hedwig";
            this.Text = "Hedwig";
            ((System.ComponentModel.ISupportInitialize)(this.pbStrokes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox pbStrokes;
    }
}