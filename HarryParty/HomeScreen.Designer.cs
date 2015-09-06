namespace WiiWandz
{
    partial class HomeScreen
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
            this.btnExpectoPatronum = new System.Windows.Forms.Button();
            this.btnAraniaExumai = new System.Windows.Forms.Button();
            this.btnLumos = new System.Windows.Forms.Button();
            this.btnHedwig = new System.Windows.Forms.Button();
            this.btnSortingHat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnExpectoPatronum
            // 
            this.btnExpectoPatronum.BackgroundImage = global::WiiWandz.Properties.Resources.btn_expecto_patronum;
            this.btnExpectoPatronum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExpectoPatronum.Location = new System.Drawing.Point(24, 243);
            this.btnExpectoPatronum.Name = "btnExpectoPatronum";
            this.btnExpectoPatronum.Size = new System.Drawing.Size(75, 75);
            this.btnExpectoPatronum.TabIndex = 0;
            this.btnExpectoPatronum.UseVisualStyleBackColor = true;
            // 
            // btnAraniaExumai
            // 
            this.btnAraniaExumai.BackgroundImage = global::WiiWandz.Properties.Resources.btn_arania_exumai;
            this.btnAraniaExumai.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAraniaExumai.Location = new System.Drawing.Point(24, 356);
            this.btnAraniaExumai.Name = "btnAraniaExumai";
            this.btnAraniaExumai.Size = new System.Drawing.Size(75, 75);
            this.btnAraniaExumai.TabIndex = 1;
            this.btnAraniaExumai.UseVisualStyleBackColor = true;
            this.btnAraniaExumai.Click += new System.EventHandler(this.btnAraniaExumai_Click);
            // 
            // btnLumos
            // 
            this.btnLumos.BackgroundImage = global::WiiWandz.Properties.Resources.btn_lumos;
            this.btnLumos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLumos.Location = new System.Drawing.Point(24, 25);
            this.btnLumos.Name = "btnLumos";
            this.btnLumos.Size = new System.Drawing.Size(75, 75);
            this.btnLumos.TabIndex = 2;
            this.btnLumos.UseVisualStyleBackColor = true;
            // 
            // btnHedwig
            // 
            this.btnHedwig.BackgroundImage = global::WiiWandz.Properties.Resources.btn_hedwig;
            this.btnHedwig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHedwig.Location = new System.Drawing.Point(24, 133);
            this.btnHedwig.Name = "btnHedwig";
            this.btnHedwig.Size = new System.Drawing.Size(75, 75);
            this.btnHedwig.TabIndex = 3;
            this.btnHedwig.UseVisualStyleBackColor = true;
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
            // 
            // HomeScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::WiiWandz.Properties.Resources.hogwarts_logo_wood;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.btnSortingHat);
            this.Controls.Add(this.btnHedwig);
            this.Controls.Add(this.btnLumos);
            this.Controls.Add(this.btnAraniaExumai);
            this.Controls.Add(this.btnExpectoPatronum);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HomeScreen";
            this.Text = "Festa Harry Potter";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExpectoPatronum;
        private System.Windows.Forms.Button btnAraniaExumai;
        private System.Windows.Forms.Button btnLumos;
        private System.Windows.Forms.Button btnHedwig;
        private System.Windows.Forms.Button btnSortingHat;
    }
}