using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WMPLib;
using AxWMPLib;
using WiimoteLib;
using WiiWandz.Spells;

namespace WiiWandz
{
    public partial class ExpectoPatronum : Form
    {
        IWMPMedia preMovie;
        IWMPMedia loopMovie;
        IWMPMedia postMovie;
        IWMPPlaylist playList;

        private const string PRE_MOVIE = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\expecto_patronum_pre.mp4";
        private const string LOOP_MOVIE = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\expecto_patronum_loop.mp4";
        private const string POST_MOVIE = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\expecto_patronum_post.mp4";

        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        int count = 0;
        bool spellCast, shownEnd;
        WandHandler wandHandler;

        public ExpectoPatronum()
        {
            InitializeComponent();

            var spellNames = new List<string>()
            {
                "Reparo",
                "Metelojinx",
                "Tarantallegra",
                "Locomotor",
                "Incendio",
                "WingardiumLeviosa"
            };

            wandHandler = new WandHandler(pbStrokes, spellNames, delegate { castSpell(); });

            spellCast = false;
            shownEnd = false;

            KeyPreview = true;
            KeyPress += new KeyPressEventHandler(HandleKeys);
        }

        void wplayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent NewState)
        {
            string val = string.Empty;
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

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(wplayer_PlayStateChange);
            //axWindowsMediaPlayer1.URL = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_pre.mp4";
            //axWindowsMediaPlayer1.settings.autoStart = false;
            //axWindowsMediaPlayer1.Ctlcontrols.play();
            playList = axWindowsMediaPlayer1.playlistCollection.newPlaylist("ExpectoPatronumVideos");
            preMovie = axWindowsMediaPlayer1.newMedia(PRE_MOVIE);
            playList.appendItem(preMovie);
            loopMovie = axWindowsMediaPlayer1.newMedia(LOOP_MOVIE);
            //playList.appendItem(loopMovie);
            postMovie = axWindowsMediaPlayer1.newMedia(POST_MOVIE);

            axWindowsMediaPlayer1.currentPlaylist = playList;
            //WMPLib.IWMPMedia3 preFile = (WMPLib.IWMPMedia3)axWindowsMediaPlayer1.mediaCollection.getAll().get_Item(0);
            //axWindowsMediaPlayer1.currentMedia = preFile;

        }

        private void HandleKeys(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ' ':
                    castSpell();
                    break;
            }
            e.Handled = true;

        }

        private void castSpell()
        {
            spellCast = true;
            wandHandler.StopTracking();

            var spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_expecto_patronum_on",
                "hue_spell_off",
                30
            );

            spell.castSpell();

            axWindowsMediaPlayer1.Visible = true;
            //playList.appendItem(postMovie);
            pbStrokes.Visible = false;
            shownEnd = true;
            axWindowsMediaPlayer1.currentMedia = postMovie;
        }
    }
}

