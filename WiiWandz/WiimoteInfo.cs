using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Spells;
using WiiWandz.Strokes;
using System.Collections.Generic;
using System.Text;
using WiiWandz.Positions;

namespace WiiWandz
{
	public partial class WiimoteInfo : UserControl
	{
		private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
		private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

		private Bitmap irBitmap = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Bitmap strokesBitmap = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Graphics irGraphics;
        private Graphics strokesGraphics;
		private Wiimote mWiimote;
        private WandTracker wandTracker;
        private Spell trigger;

        private double maxConfidence;
        private double minConfidence;

        public WiimoteInfo()
		{
			InitializeComponent();
            irGraphics = Graphics.FromImage(irBitmap);
            strokesGraphics = Graphics.FromImage(strokesBitmap);
            wandTracker = new WandTracker();

            this.maxConfidence = 0.0;
            this.minConfidence = 1.0;

        }

		public WiimoteInfo(Wiimote wm) : this()
		{
			mWiimote = wm;
		}

		public void UpdateState(WiimoteChangedEventArgs args)
		{
			BeginInvoke(new UpdateWiimoteStateDelegate(UpdateWiimoteChanged), args);
		}

		public void UpdateExtension(WiimoteExtensionChangedEventArgs args)
		{
			BeginInvoke(new UpdateExtensionChangedDelegate(UpdateExtensionChanged), args);
		}

		private void chkLED_CheckedChanged(object sender, EventArgs e)
		{
			//mWiimote.SetLEDs(chkLED1.Checked, chkLED2.Checked, chkLED3.Checked, chkLED4.Checked);
		}

		private void chkRumble_CheckedChanged(object sender, EventArgs e)
		{
			//mWiimote.SetRumble(chkRumble.Checked);
		}

		private void UpdateWiimoteChanged(WiimoteChangedEventArgs args)
		{
			WiimoteState ws = args.WiimoteState;

			clbButtons.SetItemChecked(0, ws.ButtonState.A);
			clbButtons.SetItemChecked(1, ws.ButtonState.B);
			clbButtons.SetItemChecked(2, ws.ButtonState.Minus);
			clbButtons.SetItemChecked(3, ws.ButtonState.Home);
			clbButtons.SetItemChecked(4, ws.ButtonState.Plus);
			clbButtons.SetItemChecked(5, ws.ButtonState.One);
			clbButtons.SetItemChecked(6, ws.ButtonState.Two);
			clbButtons.SetItemChecked(7, ws.ButtonState.Up);
			clbButtons.SetItemChecked(8, ws.ButtonState.Down);
			clbButtons.SetItemChecked(9, ws.ButtonState.Left);
			clbButtons.SetItemChecked(10, ws.ButtonState.Right);

			lblAccel.Text = ws.AccelState.Values.ToString();

            /*
			chkLED1.Checked = ws.LEDState.LED1;
			chkLED2.Checked = ws.LEDState.LED2;
			chkLED3.Checked = ws.LEDState.LED3;
			chkLED4.Checked = ws.LEDState.LED4;
            */

			switch(ws.ExtensionType)
			{
				case ExtensionType.Nunchuk:
                    /*
					lblChuk.Text = ws.NunchukState.AccelState.Values.ToString();
					lblChukJoy.Text = ws.NunchukState.Joystick.ToString();
					chkC.Checked = ws.NunchukState.C;
					chkZ.Checked = ws.NunchukState.Z;
                     */ 
					break;

				case ExtensionType.ClassicController:
                    /*
					clbCCButtons.SetItemChecked(0, ws.ClassicControllerState.ButtonState.A);
					clbCCButtons.SetItemChecked(1, ws.ClassicControllerState.ButtonState.B);
					clbCCButtons.SetItemChecked(2, ws.ClassicControllerState.ButtonState.X);
					clbCCButtons.SetItemChecked(3, ws.ClassicControllerState.ButtonState.Y);
					clbCCButtons.SetItemChecked(4, ws.ClassicControllerState.ButtonState.Minus);
					clbCCButtons.SetItemChecked(5, ws.ClassicControllerState.ButtonState.Home);
					clbCCButtons.SetItemChecked(6, ws.ClassicControllerState.ButtonState.Plus);
					clbCCButtons.SetItemChecked(7, ws.ClassicControllerState.ButtonState.Up);
					clbCCButtons.SetItemChecked(8, ws.ClassicControllerState.ButtonState.Down);
					clbCCButtons.SetItemChecked(9, ws.ClassicControllerState.ButtonState.Left);
					clbCCButtons.SetItemChecked(10, ws.ClassicControllerState.ButtonState.Right);
					clbCCButtons.SetItemChecked(11, ws.ClassicControllerState.ButtonState.ZL);
					clbCCButtons.SetItemChecked(12, ws.ClassicControllerState.ButtonState.ZR);
					clbCCButtons.SetItemChecked(13, ws.ClassicControllerState.ButtonState.TriggerL);
					clbCCButtons.SetItemChecked(14, ws.ClassicControllerState.ButtonState.TriggerR);

					lblCCJoy1.Text = ws.ClassicControllerState.JoystickL.ToString();
					lblCCJoy2.Text = ws.ClassicControllerState.JoystickR.ToString();

					lblTriggerL.Text = ws.ClassicControllerState.TriggerL.ToString();
					lblTriggerR.Text = ws.ClassicControllerState.TriggerR.ToString();
                     */
					break;

				case ExtensionType.Guitar:
                    /*
				    clbGuitarButtons.SetItemChecked(0, ws.GuitarState.FretButtonState.Green);
				    clbGuitarButtons.SetItemChecked(1, ws.GuitarState.FretButtonState.Red);
				    clbGuitarButtons.SetItemChecked(2, ws.GuitarState.FretButtonState.Yellow);
				    clbGuitarButtons.SetItemChecked(3, ws.GuitarState.FretButtonState.Blue);
				    clbGuitarButtons.SetItemChecked(4, ws.GuitarState.FretButtonState.Orange);
				    clbGuitarButtons.SetItemChecked(5, ws.GuitarState.ButtonState.Minus);
				    clbGuitarButtons.SetItemChecked(6, ws.GuitarState.ButtonState.Plus);
				    clbGuitarButtons.SetItemChecked(7, ws.GuitarState.ButtonState.StrumUp);
				    clbGuitarButtons.SetItemChecked(8, ws.GuitarState.ButtonState.StrumDown);

					clbTouchbar.SetItemChecked(0, ws.GuitarState.TouchbarState.Green);
					clbTouchbar.SetItemChecked(1, ws.GuitarState.TouchbarState.Red);
					clbTouchbar.SetItemChecked(2, ws.GuitarState.TouchbarState.Yellow);
					clbTouchbar.SetItemChecked(3, ws.GuitarState.TouchbarState.Blue);
					clbTouchbar.SetItemChecked(4, ws.GuitarState.TouchbarState.Orange);

					lblGuitarJoy.Text = ws.GuitarState.Joystick.ToString();
					lblGuitarWhammy.Text = ws.GuitarState.WhammyBar.ToString();
					lblGuitarType.Text = ws.GuitarState.GuitarType.ToString();
                     */
				    break;

				case ExtensionType.Drums:
                    /*
					clbDrums.SetItemChecked(0, ws.DrumsState.Red);
					clbDrums.SetItemChecked(1, ws.DrumsState.Blue);
					clbDrums.SetItemChecked(2, ws.DrumsState.Green);
					clbDrums.SetItemChecked(3, ws.DrumsState.Yellow);
					clbDrums.SetItemChecked(4, ws.DrumsState.Orange);
					clbDrums.SetItemChecked(5, ws.DrumsState.Pedal);
					clbDrums.SetItemChecked(6, ws.DrumsState.Minus);
					clbDrums.SetItemChecked(7, ws.DrumsState.Plus);

					lbDrumVelocity.Items.Clear();
					lbDrumVelocity.Items.Add(ws.DrumsState.RedVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.BlueVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.GreenVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.YellowVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.OrangeVelocity);
					lbDrumVelocity.Items.Add(ws.DrumsState.PedalVelocity);

					lblDrumJoy.Text = ws.DrumsState.Joystick.ToString();
                     */
					break;

				case ExtensionType.BalanceBoard:
                    /*
					if(chkLbs.Checked)
					{
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesLb.TopLeft.ToString();
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesLb.TopRight.ToString();
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesLb.BottomLeft.ToString();
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesLb.BottomRight.ToString();
						lblBBTotal.Text = ws.BalanceBoardState.WeightLb.ToString();
					}
					else
					{
						lblBBTL.Text = ws.BalanceBoardState.SensorValuesKg.TopLeft.ToString();
						lblBBTR.Text = ws.BalanceBoardState.SensorValuesKg.TopRight.ToString();
						lblBBBL.Text = ws.BalanceBoardState.SensorValuesKg.BottomLeft.ToString();
						lblBBBR.Text = ws.BalanceBoardState.SensorValuesKg.BottomRight.ToString();
						lblBBTotal.Text = ws.BalanceBoardState.WeightKg.ToString();
					}
					lblCOG.Text = ws.BalanceBoardState.CenterOfGravity.ToString();
                     */
					break;
			}

			irGraphics.Clear(Color.Black);
            strokesGraphics.Clear(Color.Black);

			UpdateIR(ws.IRState.IRSensors[0], lblIR1, lblIR1Raw, chkFound1, Color.Red);
			UpdateIR(ws.IRState.IRSensors[1], lblIR2, lblIR2Raw, chkFound2, Color.Blue);
			UpdateIR(ws.IRState.IRSensors[2], lblIR3, lblIR3Raw, chkFound3, Color.Yellow);
			UpdateIR(ws.IRState.IRSensors[3], lblIR4, lblIR4Raw, chkFound4, Color.Orange);

            if (ws.IRState.IRSensors[0].Found && ws.IRState.IRSensors[1].Found)
            {
                irGraphics.DrawEllipse(new Pen(Color.Green), (int)(ws.IRState.RawMidpoint.X / 4), (int)(ws.IRState.RawMidpoint.Y / 4), 2, 2);
            }


            if (ws.IRState.IRSensors[0].Found)
            {
                // Check for spell action
                trigger = wandTracker.addPosition(ws.IRState.IRSensors[0].RawPosition, DateTime.Now);
            }
            else if (ws.IRState.IRSensors[1].Found)
            {
                // Check for spell action
                trigger = wandTracker.addPosition(ws.IRState.IRSensors[1].RawPosition, DateTime.Now);
            }

            if (trigger != null) // && trigger.casting())
            {
                if (trigger.getConfidence() > maxConfidence)
                {
                    maxConfidence = trigger.getConfidence();
                }
                if (trigger.getConfidence() < minConfidence)
                {
                    minConfidence = trigger.getConfidence();
                }
                lblSpellName.Text = trigger.GetType().Name + " - " + DateTime.Now.Subtract(wandTracker.startSpell).Seconds + " seconds";
                lblMaxConfidence.Text = maxConfidence.ToString();
                lblMinConfidence.Text = minConfidence.ToString();
                lblCurrentConfidence.Text = trigger.getConfidence().ToString();
            }
            else
            {
                lblSpellName.Text = "No spell";
            }

            Position previous = null;
            foreach (Position p in wandTracker.positions)
            {
                if (previous == null)
                {
                    previous = p;
                    continue;
                }
                System.Drawing.Point pointA = new System.Drawing.Point();
                System.Drawing.Point pointB = new System.Drawing.Point();
                pointA.X = (PositionStatistics.MAX_X - previous.point.X)/4;
                pointA.Y = (PositionStatistics.MAX_Y - previous.point.Y)/4;
                pointB.X = (PositionStatistics.MAX_X - p.point.X) / 4;
                pointB.Y = (PositionStatistics.MAX_Y - p.point.Y) / 4;
                strokesGraphics.DrawLine(new Pen(Color.Yellow), pointA, pointB);

                previous = p;
            }

            if (wandTracker.strokes != null)
            {
                foreach (Stroke stroke in wandTracker.strokes)
                {
                    /*
                    switch (stroke.direction)
                    {
                        case StrokeDirection.Bumbled:
                            strokesGraphics.DrawEllipse(
                                new Pen(Color.Purple), 
                                (stroke.start.SystemPoint().X + stroke.end.SystemPoint().X) / 2,
                                (stroke.start.SystemPoint().Y + stroke.end.SystemPoint().Y) / 2,
                                10, 5);
                            strokesGraphics.DrawLine(
                                new Pen(Color.Red), 
                                stroke.start.SystemPoint(), 
                                stroke.end.SystemPoint());
                            break;
                        default:
                            strokesGraphics.DrawLine(
                                new Pen(Color.Green), 
                                stroke.start.SystemPoint(), 
                                stroke.end.SystemPoint());
                            break;
                    }
                     */ 
                    /*
                    strokesGraphics.DrawString(
                        stroke.direction.ToString(),
                        new Font(FontFamily.GenericMonospace, 12.0f, FontStyle.Bold),
                        new SolidBrush(Color.Orange),
                        (stroke.start.SystemPoint().X + stroke.end.SystemPoint().X) / 2,
                        (stroke.start.SystemPoint().Y + stroke.end.SystemPoint().Y) / 2);
                    */
                }

            }

			pbIR.Image = irBitmap;
            pbStrokes.Image = strokesBitmap;

			pbBattery.Value = (ws.Battery > 0xc8 ? 0xc8 : (int)ws.Battery);
			lblBattery.Text = ws.Battery.ToString();
			lblDevicePath.Text = "Device Path: " + mWiimote.HIDDevicePath;

        }

		private void UpdateIR(IRSensor irSensor, Label lblNorm, Label lblRaw, CheckBox chkFound, Color color)
		{
			chkFound.Checked = irSensor.Found;

			if(irSensor.Found)
			{
				lblNorm.Text = irSensor.Position.ToString() + ", " + irSensor.Size;
				lblRaw.Text = irSensor.RawPosition.ToString();
				irGraphics.DrawEllipse(new Pen(color), (int)(irSensor.RawPosition.X / 4), (int)(irSensor.RawPosition.Y / 4),
							 irSensor.Size+1, irSensor.Size+1);
			}
		}

		private void UpdateExtensionChanged(WiimoteExtensionChangedEventArgs args)
		{
			/*
            chkExtension.Text = args.ExtensionType.ToString();
			chkExtension.Checked = args.Inserted;
             */
		}

		public Wiimote Wiimote
		{
			set { mWiimote = value; }
		}

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void cloudBitID_TextChanged(object sender, EventArgs e)
        {
            wandTracker.setDeviceInfo(cloudBitID.Text, cloudBitAuthentication.Text);
        }

        private void cloudBitAuthentication_TextChanged(object sender, EventArgs e)
        {
            wandTracker.setDeviceInfo(cloudBitID.Text, cloudBitAuthentication.Text);
        }

        private void spellBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSpells();
        }

        private void duration1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(duration1.Text))
            {
                duration1.Text = "10";
            }
            setSpells();
        }

        private void spellBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSpells();
        }


        private void setSpells()
        {
            List<String> spells = new List<string>();
            List<int> durations = new List<int>();
            List<int> voltages = new List<int>();

            if (!String.IsNullOrEmpty(spellBox1.Text))
            {
                spells.Add(spellBox1.Text.Replace(" ", ""));
                durations.Add(int.Parse(duration1.Text));
                voltages.Add(int.Parse(voltage1.Text));
            }

            if (!String.IsNullOrEmpty(spellBox2.Text))
            {
                spells.Add(spellBox2.Text.Replace(" ", ""));
                durations.Add(int.Parse(duration2.Text));
                voltages.Add(int.Parse(voltage3.Text));
            }

            if (!String.IsNullOrEmpty(spellBox3.Text))
            {
                spells.Add(spellBox3.Text.Replace(" ", ""));
                durations.Add(int.Parse(duration3.Text));
                voltages.Add(int.Parse(voltage3.Text));
            }

            wandTracker.setSpells(spells, durations, voltages);
        }

        private void duration2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(duration2.Text))
            {
                duration2.Text = "10";
            }
            setSpells();
        }

        private void spellBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            setSpells();
        }

        private void duration3_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(duration3.Text))
            {
                duration3.Text = "10";
            }
            setSpells();
        }

        private void recordButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(spellBox1.Text.Replace(" ", ""));
            sb.Append(";");
            foreach (Position p in wandTracker.positions)
            {
                sb.Append(p.point.X);
                sb.Append(",");
                sb.Append(p.point.Y);
                sb.Append(";");
            }
            sb.Append("\n");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\wiiwands-test-data.txt", true))
            {
                file.WriteLine(sb.ToString());
            }

        }

        private void btnOpenEditor_Click(object sender, EventArgs e)
        {
            TrainingDataForm f2 = new TrainingDataForm();
            f2.ShowDialog(); 
        }

        private void voltage1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(voltage1.Text))
            {
                voltage1.Text = "25";
            }
            setSpells();
        }

        private void voltage2_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(voltage2.Text))
            {
                voltage2.Text = "50";
            }
            setSpells();
        }

        private void voltage3_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(voltage3.Text))
            {
                voltage3.Text = "75";
            }
            setSpells();
        }

	}
}
