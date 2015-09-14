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
    public partial class Werewolf : Form
    {
        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        WandHandler wandHandler;
        SoundPlayer sound;

        public Werewolf()
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

            sound = new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\werewolf_howling_2.wav");

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

        void castSpell()
        {
            wandHandler.StopTracking();

            // TODO: probably need to run this in a separate thread
            IftttStartStopSpell spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_werewolf_on",
                "hue_spell_off",
                5);
            spell.castSpell();

            Incendio cloudBit = new Incendio("00e04c034e9a", "c83578c843ac46220849a1bd919662b340e537dd14f0b234a5a99634becc5339", 75, 10000, null, null);
            cloudBit.castSpell();

            sound.PlaySync();
            sound.PlaySync();
            Close();
        }


    }
}
