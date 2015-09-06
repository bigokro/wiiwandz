using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WMPLib;
using AxWMPLib;
using WiimoteLib;
using System.Drawing.Imaging;
using WiiWandz.Spells;
using WiiWandz.Strokes;
using WiiWandz.Positions;

namespace WiiWandz
{
    public partial class AraniaExumai : Form
    {

        WMPLib.IWMPMedia preMovie;
        WMPLib.IWMPMedia loopMovie;
        WMPLib.IWMPMedia postMovie;
        IWMPPlaylist playList;

        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        private Bitmap strokesBitmap = new Bitmap(256, 192, PixelFormat.Format24bppRgb);
        private Graphics strokesGraphics;
        private Wiimote mWiimote;
        private WandTracker wandTracker;
        private Spell trigger;

        private double maxConfidence;
        private double minConfidence;

        int count = 0;
        Boolean spellCast;
        Dictionary<Guid, WiimoteInfo> mWiimoteMap = new Dictionary<Guid, WiimoteInfo>();
        WiimoteCollection mWC;

        public AraniaExumai()
        {
            InitializeComponent();

            strokesGraphics = Graphics.FromImage(strokesBitmap);
            wandTracker = new WandTracker();

            this.maxConfidence = 0.0;
            this.minConfidence = 1.0;
            this.spellCast = false;
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);
            //axWindowsMediaPlayer1.URL = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_pre.mp4";
            //axWindowsMediaPlayer1.settings.autoStart = false;
            //axWindowsMediaPlayer1.Ctlcontrols.play();
            playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("AraniaExumaiVideos");
            preMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_pre.mp4");
            playList.appendItem(preMovie);
            loopMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_loop.mp4");
            //playList.appendItem(loopMovie);
            postMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_post.mp4");

            axWindowsMediaPlayer1.currentPlaylist = playList;
            //WMPLib.IWMPMedia3 preFile = (WMPLib.IWMPMedia3)axWindowsMediaPlayer1.mediaCollection.getAll().get_Item(0);
            //axWindowsMediaPlayer1.currentMedia = preFile;

        }

        void wplayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent NewState)
        {
            string val = "";
            switch (NewState.newState)
            {
                case 1:
                    val = "Stopped";
                    break;
                case 2:
                    val = "Paused";
                    break;
                case 8:
                    val = "Media Ended";
                    //playList.removeItem(preMovie);
                    if (!spellCast && count < 5)
                    {
                        playList.appendItem(loopMovie);
                        pbStrokes.Visible = true;
                        if (mWC == null || mWC.Count == 0)
                        {
                            initWiiMotes();
                        }
                    }
                    else if (spellCast || count < 6)
                    {
                        playList.appendItem(postMovie);
                        pbStrokes.Visible = false;
                        count = 10;
                    }
                    else
                    {
                        Close();
                    }
                    count++;
                    break;
                case 9:
                    val = "Transitioning";
                    break;
                case 12:
                    val = "last";
                    break;
            }
        }

        private void initWiiMotes()
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
                MessageBox.Show(ex.Message, "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            strokesGraphics.Clear(Color.Black);

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
                // TODO: HERE IS WHERE EVERYTHING SHOULD HAPPEN!!!!!!!!!!!!
                spellCast = true;

                IftttStartStopSpell spell = new IftttStartStopSpell(
                    "bslEohHzR8x_HsJ3vWzxub",
                    "hue_arania_exumai_on",
                    "hue_spell_off",
                    5);
                spell.castSpell();

                if (trigger.getConfidence() > maxConfidence)
                {
                    maxConfidence = trigger.getConfidence();
                }
                if (trigger.getConfidence() < minConfidence)
                {
                    minConfidence = trigger.getConfidence();
                }
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

}

