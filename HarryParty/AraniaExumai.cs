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


namespace WiiWandz
{
    public partial class AraniaExumai : Form
    {

        WMPLib.IWMPMedia preMovie;
        WMPLib.IWMPMedia loopMovie;
        WMPLib.IWMPMedia postMovie;
        IWMPPlaylist playList;

        int count = 0;

        public AraniaExumai()
        {
            InitializeComponent();
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
                    if (count < 3)
                    {
                        playList.appendItem(loopMovie);
                    }
                    else if (count < 4)
                    {
                        playList.appendItem(postMovie);
                    }
                    count++;
                    break;
                case 9:
                    val = "Transitioning";
                    
                //axWindowsMediaPlayer1.URL = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_loop.mp4";
                //WMPLib.IWMPMedia loopFile = axWindowsMediaPlayer1.newMedia(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\videos\arania_exumai_loop.mp4");
                //WMPLib.IWMPMedia3 loopFile = (WMPLib.IWMPMedia3)axWindowsMediaPlayer1.mediaCollection.getAll().get_Item(1);
                //axWindowsMediaPlayer1.currentMedia = loopFile;
                //axWindowsMediaPlayer1.currentPlaylist.appendItem(loopFile);
                //axWindowsMediaPlayer1.Ctlcontrols.play();
                    axWindowsMediaPlayer1.Ctlcontrols.pause();
                    break;
                case 12:
                    val = "last";
                    break;
            }
        }
    }


}
