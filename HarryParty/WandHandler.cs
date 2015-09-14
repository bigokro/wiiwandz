using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Positions;
using WiiWandz.Spells;
using WiiWandz.Strokes;

namespace WiiWandz
{
    public class WandHandler
    {
        private Bitmap strokesBitmap = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Graphics strokesGraphics;
        private Wiimote mWiimote;
        private WandTracker wandTracker;
        private Spell trigger;

        Dictionary<Guid, WiimoteInfo> mWiimoteMap = new Dictionary<Guid, WiimoteInfo>();
        WiimoteCollection mWC;
        GlobalMouseHandler gmh;
        PictureBox pbStrokes;
        Action spellAction;

        public WandHandler(PictureBox pbStrokes, List<String> spellNames, Action spellFunction)
        {
            this.pbStrokes = pbStrokes;
            this.spellAction = spellFunction;
            strokesGraphics = Graphics.FromImage(strokesBitmap);
            wandTracker = new WandTracker();
            wandTracker.setSpells(spellNames, null, null, null);
        }

        void gmh_TheMouseMoved()
        {
            System.Drawing.Point cur_pos = System.Windows.Forms.Cursor.Position;

            // Check for spell action
            trigger = wandTracker.addMousePosition(cur_pos, DateTime.Now);

            if (trigger != null) // && trigger.casting())
            {
                spellAction();
            }

            drawWandMovement();
        }

        public void StartTracking()
        {
            // find all wiimotes connected to the system
            mWC = new WiimoteCollection();
            int index = 1;

            try
            {
                mWC.FindAllWiimotes();
            }
            catch (WiimoteNotFoundException ex)
            {
                //MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (WiimoteException ex)
            {
                MessageBox.Show(ex.Message, "Wiimote error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unknown error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            foreach (Wiimote wm in mWC)
            {
                wm.WiimoteChanged += wm_WiimoteChanged;
                wm.WiimoteExtensionChanged += wm_WiimoteExtensionChanged;

                wm.Connect();
                if (wm.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
                    wm.SetReportType(InputReport.IRExtensionAccel, IRSensitivity.Maximum, true);

                wm.SetLEDs(index++);
            }

            // Init Mouse tracker, too
            gmh = new GlobalMouseHandler();
            gmh.TheMouseMoved += new MouseMovedEvent(gmh_TheMouseMoved);
            Application.AddMessageFilter(gmh);
        }

        public void StopTracking()
        {
            Application.RemoveMessageFilter(gmh);
        }

        void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            WiimoteInfo wi = mWiimoteMap[((Wiimote)sender).ID];
            wi.UpdateState(e);
        }

        void wm_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            // find the control for this Wiimote
            WiimoteInfo wi = mWiimoteMap[((Wiimote)sender).ID];
            wi.UpdateExtension(e);

            if (e.Inserted)
                ((Wiimote)sender).SetReportType(InputReport.IRExtensionAccel, true);
            else
                ((Wiimote)sender).SetReportType(InputReport.IRAccel, true);
        }

        private void MultipleWiimoteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Wiimote wm in mWC)
                wm.Disconnect();
        }

        public void UpdateState(WiimoteChangedEventArgs args)
        {
            //BeginInvoke(new UpdateWiimoteStateDelegate(UpdateWiimoteChanged), args);
        }

        public void UpdateExtension(WiimoteExtensionChangedEventArgs args)
        {
            //BeginInvoke(new UpdateExtensionChangedDelegate(UpdateExtensionChanged), args);
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

            for (int i = 0; i < 4; i++)
            {
                if (ws.IRState.IRSensors[i].Found)
                {
                    // Check for spell action
                    trigger = wandTracker.addPosition(ws.IRState.IRSensors[i].RawPosition, DateTime.Now);
                    break;
                }
            }

            if (trigger != null) // && trigger.casting())
            {
                spellAction();
            }

            drawWandMovement();
        }


        private void drawWandMovement()
        {
            strokesGraphics.Clear(Color.Black);

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
                // TODO: different measures for different inputs... do this in the position class

                pointA.X = (PositionStatistics.MAX_X - previous.point.X) / 4;
                pointA.Y = (PositionStatistics.MAX_Y - previous.point.Y) / 4;
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

            pbStrokes.Image = strokesBitmap;
        }
        private void UpdateIR(IRSensor irSensor, Label lblNorm, Label lblRaw, CheckBox chkFound, Color color)
        {
            //chkFound.Checked = irSensor.Found;
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


    }

    public delegate void MouseMovedEvent();

    public class GlobalMouseHandler : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public event MouseMovedEvent TheMouseMoved;

        #region IMessageFilter Members

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == WM_MOUSEMOVE)
            {
                if (TheMouseMoved != null)
                {
                    TheMouseMoved();
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }

        #endregion
    }
}