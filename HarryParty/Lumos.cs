using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Spells;

namespace WiiWandz
{
    public partial class Lumos : Form
    {
        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        WandHandler wandHandler;
        SoundPlayer sound;

        public Lumos()
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
            wandHandler.StartTracking();

            this.KeyPreview = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);

            sound = new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\lumos_maxima.wav");

        }

        private void HandleKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case ' ':
                    castSpell();
                    break;
                case (char)13:
                    endLumos();
                    break;
            }
            e.Handled = true;

        }

        void castSpell()
        {
            // TODO: probably need to run this in a separate thread
            IftttStartStopSpell spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_random_color",
                "hue_bright",
                1);
            spell.castSpell();

            sound.PlaySync();
        }

        void endLumos()
        {
            wandHandler.StopTracking();

            IftttStartStopSpell spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_dim",
                "hue_spell_off",
                1);
            spell.castSpell();

            Close();
        }
    }
}
