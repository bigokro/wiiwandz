namespace WiiWandz
{
    partial class SortingHat
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
            this.btnSortingHat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSortingHat
            // 
            this.btnSortingHat.BackgroundImage = global::WiiWandz.Properties.Resources.btn_sorting_hat;
            this.btnSortingHat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSortingHat.Location = new System.Drawing.Point(24, 513);
            this.btnSortingHat.Name = "btnSortingHat";
            this.btnSortingHat.Size = new System.Drawing.Size(75, 75);
            this.btnSortingHat.TabIndex = 4;
            this.btnSortingHat.UseVisualStyleBackColor = true;
            this.btnSortingHat.Click += new System.EventHandler(this.btnSortingHat_Click);
            // 
            // SortingHat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::WiiWandz.Properties.Resources.hogwarts_logo_wood;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnSortingHat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SortingHat";
            this.Text = "Festa Harry Potter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSortingHat;
    }
}