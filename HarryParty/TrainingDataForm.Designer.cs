namespace WiiWandz
{
	partial class TrainingDataForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.textBoxPoints = new System.Windows.Forms.RichTextBox();
            this.tbLineNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.spellBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.btnLowestY = new System.Windows.Forms.Button();
            this.btnLowestX = new System.Windows.Forms.Button();
            this.btnHighestX = new System.Windows.Forms.Button();
            this.btnHighestY = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbStrokes)).BeginInit();
            this.SuspendLayout();
            // 
            // pbStrokes
            // 
            this.pbStrokes.Location = new System.Drawing.Point(430, 83);
            this.pbStrokes.Name = "pbStrokes";
            this.pbStrokes.Size = new System.Drawing.Size(256, 192);
            this.pbStrokes.TabIndex = 43;
            this.pbStrokes.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "trainingFile";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // textBoxPoints
            // 
            this.textBoxPoints.Location = new System.Drawing.Point(12, 83);
            this.textBoxPoints.Name = "textBoxPoints";
            this.textBoxPoints.Size = new System.Drawing.Size(365, 192);
            this.textBoxPoints.TabIndex = 44;
            this.textBoxPoints.Text = "";
            this.textBoxPoints.TextChanged += new System.EventHandler(this.textBoxPoints_TextChanged);
            // 
            // tbLineNumber
            // 
            this.tbLineNumber.Location = new System.Drawing.Point(46, 47);
            this.tbLineNumber.Name = "tbLineNumber";
            this.tbLineNumber.Size = new System.Drawing.Size(100, 20);
            this.tbLineNumber.TabIndex = 45;
            this.tbLineNumber.TextChanged += new System.EventHandler(this.tbLineNumber_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 46;
            this.label1.Text = "Line";
            // 
            // spellBox
            // 
            this.spellBox.FormattingEnabled = true;
            this.spellBox.Items.AddRange(new object[] {
            "Aguamenti",
            "Alohomora",
            "Arresto Momentum",
            "Ascendio",
            "Descendo",
            "Herbivicus",
            "Incendio",
            "Locomotor",
            "Metelojinx",
            "Mimblewimble",
            "Reparo",
            "Revelio",
            "Silencio",
            "Specialis Revelio",
            "Tarantallegra",
            "Wingardium Leviosa"});
            this.spellBox.Location = new System.Drawing.Point(254, 46);
            this.spellBox.Name = "spellBox";
            this.spellBox.Size = new System.Drawing.Size(121, 21);
            this.spellBox.TabIndex = 47;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Spell";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(430, 47);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 49;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(519, 47);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 50;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(611, 47);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 51;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(16, 13);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(130, 23);
            this.btnChooseFile.TabIndex = 52;
            this.btnChooseFile.Text = "Choose a file...";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(161, 18);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(92, 13);
            this.lblFileName.TabIndex = 54;
            this.lblFileName.Text = "<No file selected>";
            // 
            // btnLowestY
            // 
            this.btnLowestY.Location = new System.Drawing.Point(202, 291);
            this.btnLowestY.Name = "btnLowestY";
            this.btnLowestY.Size = new System.Drawing.Size(86, 23);
            this.btnLowestY.TabIndex = 55;
            this.btnLowestY.Text = "Crop Lowest Y";
            this.btnLowestY.UseVisualStyleBackColor = true;
            this.btnLowestY.Click += new System.EventHandler(this.btnLowestY_Click);
            // 
            // btnLowestX
            // 
            this.btnLowestX.Location = new System.Drawing.Point(12, 291);
            this.btnLowestX.Name = "btnLowestX";
            this.btnLowestX.Size = new System.Drawing.Size(86, 23);
            this.btnLowestX.TabIndex = 56;
            this.btnLowestX.Text = "Crop Lowest X";
            this.btnLowestX.UseVisualStyleBackColor = true;
            this.btnLowestX.Click += new System.EventHandler(this.btnLowestX_Click);
            // 
            // btnHighestX
            // 
            this.btnHighestX.Location = new System.Drawing.Point(102, 291);
            this.btnHighestX.Name = "btnHighestX";
            this.btnHighestX.Size = new System.Drawing.Size(86, 23);
            this.btnHighestX.TabIndex = 57;
            this.btnHighestX.Text = "Crop Highest X";
            this.btnHighestX.UseVisualStyleBackColor = true;
            this.btnHighestX.Click += new System.EventHandler(this.btnHighestX_Click);
            // 
            // btnHighestY
            // 
            this.btnHighestY.Location = new System.Drawing.Point(291, 291);
            this.btnHighestY.Name = "btnHighestY";
            this.btnHighestY.Size = new System.Drawing.Size(86, 23);
            this.btnHighestY.TabIndex = 58;
            this.btnHighestY.Text = "Crop Highest Y";
            this.btnHighestY.UseVisualStyleBackColor = true;
            this.btnHighestY.Click += new System.EventHandler(this.btnHighestY_Click);
            // 
            // TrainingDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 326);
            this.Controls.Add(this.btnHighestY);
            this.Controls.Add(this.btnHighestX);
            this.Controls.Add(this.btnLowestX);
            this.Controls.Add(this.btnLowestY);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnChooseFile);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spellBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbLineNumber);
            this.Controls.Add(this.textBoxPoints);
            this.Controls.Add(this.pbStrokes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TrainingDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WiiWandz";
            ((System.ComponentModel.ISupportInitialize)(this.pbStrokes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        public System.Windows.Forms.PictureBox pbStrokes;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.RichTextBox textBoxPoints;
        private System.Windows.Forms.TextBox tbLineNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox spellBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.Button btnLowestY;
        private System.Windows.Forms.Button btnLowestX;
        private System.Windows.Forms.Button btnHighestX;
        private System.Windows.Forms.Button btnHighestY;

    }
}