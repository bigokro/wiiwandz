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

        int count = 0;
        Boolean spellCast, shownEnd;
        WandHandler wandHandler;

        public AraniaExumai()
        {
            InitializeComponent();

            List<String> spellNames = new List<string>();
            //spellNames.Add("Aguamenti");
            spellNames.Add("Reparo");
            spellNames.Add("Metelojinx");
            spellNames.Add("Tarantallegra");
            spellNames.Add("Locomotor");
            spellNames.Add("Incendio");
            spellNames.Add("WingardiumLeviosa");
            wandHandler = new WandHandler(pbStrokes, spellNames, delegate { castSpell(); });

            this.spellCast = false;
            this.shownEnd = false;

            this.KeyPreview = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);


        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);

            playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("ExpectoPatronumVideos");
            preMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_pre.mp4");
            playList.appendItem(preMovie);
            loopMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_loop.mp4");
            //playList.appendItem(loopMovie);
            postMovie = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_post.mp4");

            axWindowsMediaPlayer1.currentPlaylist = playList;
        }

        private void HandleKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ' ':
                    castSpell();
                    break;
            }
            e.Handled = true;

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
                    if (!spellCast)
                    {
                        //playList.appendItem(loopMovie);
                        pbStrokes.Visible = true;
                        wandHandler.StartTracking();

                        axWindowsMediaPlayer1.Visible = false;

                    }
                    else if (spellCast && !shownEnd)
                    {
                        // TODO: moved this to the place where the spell is cast
                        axWindowsMediaPlayer1.Visible = true;
                        playList.appendItem(postMovie);
                        pbStrokes.Visible = false;
                        shownEnd = true;
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

        private void castSpell()
        {
            spellCast = true;

            wandHandler.StopTracking();

            IftttStartStopSpell spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_arania_exumai_on",
                "hue_spell_off",
                5);
            spell.castSpell();

            Incendio cloudBit = new Incendio("00e04c034e9a", "c83578c843ac46220849a1bd919662b340e537dd14f0b234a5a99634becc5339", 50, 1000, null, null);
            cloudBit.castSpell();

            axWindowsMediaPlayer1.Visible = true;
            //playList.appendItem(postMovie);
            pbStrokes.Visible = false;
            shownEnd = true;
            axWindowsMediaPlayer1.currentMedia = postMovie;
        }


    }

}

