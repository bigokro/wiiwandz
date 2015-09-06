using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WiimoteLib;
using System.IO;
using WiiWandz.Positions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace WiiWandz 
{
	public partial class TrainingDataForm : Form
	{
		// map a wiimote to a specific state user control dealie
		Dictionary<Guid,WiimoteInfo> mWiimoteMap = new Dictionary<Guid,WiimoteInfo>();
		WiimoteCollection mWC;

        protected string fileName;
        protected string currentLine;

        private Bitmap strokesBitmap = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Graphics strokesGraphics;

        public TrainingDataForm()
		{
			InitializeComponent();

            strokesGraphics = Graphics.FromImage(strokesBitmap);
		}

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                fileName = openFileDialog.FileName;
                lblFileName.Text = fileName;
                tbLineNumber.Text = "1";
                readLine(1);
            }
        }

        private void readLine(int lineNum)
        {
            FileStream fs = File.OpenRead(fileName);
            StreamReader sr = new StreamReader(fs);

            // TODO: either implement seek, or store ahead in memory
            currentLine = null;
            for (int i = 1; i <= lineNum; i++)
            {
                try
                {
                    currentLine = sr.ReadLine();
                }
                catch (IOException e)
                {
                    MessageBox.Show(
                        "Error reading file (have you reached the end?): " + e.Message,
                        "File Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            sr.Close();
            fs.Close();

            if (currentLine != null)
            {
                displayLine(currentLine);
            }
        }

        private void writeLine(int lineNum)
        {
            btnChooseFile.Enabled = false;
            btnReload.Enabled = false;
            btnSave.Enabled = false;
            btnNext.Enabled = false;

            string tempFile = @"C:\Users\Public\wiiwands-temp-file.txt";

            // Read from the target file and write to a new file.
            int line_number = 1;
            string line = null;
            using (StreamReader reader = new StreamReader(fileName))
            using (StreamWriter writer = new StreamWriter(tempFile))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line_number == lineNum)
                    {
                        writer.WriteLine(currentLine);
                    }
                    else
                    {
                        writer.WriteLine(line);
                    }
                    line_number++;
                }

                reader.Close();
                writer.Close();
            }

            // Overwrite original file
            line_number = 1;
            line = null;
            using (StreamReader reader = new StreamReader(tempFile))
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    writer.WriteLine(line);
                    line_number++;
                }
                reader.Close();
                writer.Close();
            }

            btnChooseFile.Enabled = true;
            btnReload.Enabled = true;
            btnSave.Enabled = true;
            btnNext.Enabled = true;

        }

        private void displayLine(string line)
        {
            textBoxPoints.Text = line;

            string spellName = line.Split(';')[0];
            spellBox.Text = spellName;

            drawPoints(line);
        }

        private void drawPoints(string line)
        {
            string[] data = line.Split(';');

            strokesGraphics.Clear(Color.Black);

            string[] previousXY = null;
            for (int i = 1; i < data.Length - 1; i++)
            {
                string[] xy = data[i].Split(',');

                if (xy.Length != 2)
                {
                    continue;
                }

                if (previousXY == null)
                {
                    previousXY = xy;
                    continue;
                }
                System.Drawing.Point pointA = new System.Drawing.Point();
                System.Drawing.Point pointB = new System.Drawing.Point();
                pointA.X = (PositionStatistics.MAX_X - int.Parse(previousXY[0])) / 4;
                pointA.Y = (PositionStatistics.MAX_Y - int.Parse(previousXY[1])) / 4;
                pointB.X = (PositionStatistics.MAX_X - int.Parse(xy[0])) / 4;
                pointB.Y = (PositionStatistics.MAX_Y - int.Parse(xy[1])) / 4;
                strokesGraphics.DrawLine(new Pen(Color.Yellow), pointA, pointB);

                previousXY = xy;
            }

            pbStrokes.Image = strokesBitmap;
        }

        private void textBoxPoints_TextChanged(object sender, EventArgs e)
        {
            currentLine = textBoxPoints.Text;
            displayLine(currentLine);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            int lineNum = int.Parse(tbLineNumber.Text);
            readLine(lineNum);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int lineNum = int.Parse(tbLineNumber.Text);
            lineNum++;
            tbLineNumber.Text = lineNum.ToString();
            readLine(lineNum);
        }

        private void tbLineNumber_TextChanged(object sender, EventArgs e)
        {
            int lineNum = int.Parse(tbLineNumber.Text);
            readLine(lineNum);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int lineNum = int.Parse(tbLineNumber.Text);
            writeLine(lineNum);
        }

        private void btnLowestY_Click(object sender, EventArgs e)
        {
            crop(CropType.LOWEST_Y);
        }

        private void btnLowestX_Click(object sender, EventArgs e)
        {
            crop(CropType.LOWEST_X);
        }

        enum CropType
        {
            LOWEST_X,
            HIGHEST_X,
            LOWEST_Y,
            HIGHEST_Y
        }

        private void crop(CropType type)
        {
            int valToBeat = 0;
            int xyIdx = 0;
            bool lowest = false;
            switch (type)
            {
                case CropType.LOWEST_X:
                    valToBeat = PositionStatistics.MAX_X;
                    xyIdx = 0;
                    lowest = true;
                    break;
                case CropType.HIGHEST_X:
                    valToBeat = 0;
                    xyIdx = 0;
                    lowest = false;
                    break;
                case CropType.LOWEST_Y:
                    valToBeat = PositionStatistics.MAX_Y;
                    xyIdx = 1;
                    lowest = true;
                    break;
                case CropType.HIGHEST_Y:
                    valToBeat = 0;
                    xyIdx = 1;
                    lowest = false;
                    break;
            }

            string[] points = currentLine.Split(';');
            int idx = points.Length - 1;
            for (; idx > 0; idx--)
            {
                string xy = points[idx];
                if (xy == null || xy.Equals("")) continue;
                string[] point = xy.Split(',');
                if (point.Length == 2)
                {
                    int val = int.Parse(point[xyIdx]);
                    if ((lowest && val <= valToBeat)
                        || (!lowest && val >= valToBeat))
                    {
                        valToBeat = val;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= idx; i++)
            {
                sb.Append(points[i]);
                sb.Append(";");
            }
            currentLine = sb.ToString();
            displayLine(currentLine);
        }

        private void btnHighestX_Click(object sender, EventArgs e)
        {
            crop(CropType.HIGHEST_X);
        }

        private void btnHighestY_Click(object sender, EventArgs e)
        {
            crop(CropType.HIGHEST_Y);
        }
	}
}
