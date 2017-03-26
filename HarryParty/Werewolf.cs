using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Spells;

namespace WiiWandz
{
    public partial class Werewolf : Form
    {
        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        private const string SOUND = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\werewolf_howling_2.wav";

        private string DEVICE = "00e04c034e9a";
        private string AUTHORIZATION = "c83578c843ac46220849a1bd919662b340e537dd14f0b234a5a99634becc5339";
        private int VOLTAGE = 75;
        private int DURATION = 10000;

        WandHandler wandHandler;
        SoundPlayer sound;

        public Werewolf()
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
            //spellNames.Add("Aguamenti");
            wandHandler = new WandHandler(pbStrokes, spellNames, delegate { castSpell(); });
            wandHandler.StartTracking();

            KeyPreview = true;
            KeyPress += new KeyPressEventHandler(HandleKeys);

            sound = new SoundPlayer(SOUND);
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

        void castSpell()
        {
            wandHandler.StopTracking();

            // TODO: probably need to run this in a separate thread
            var spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_werewolf_on",
                "hue_spell_off",
                5
            );

            spell.castSpell();

            var cloudBit = new Incendio(DEVICE, AUTHORIZATION, VOLTAGE, DURATION, null, null);
            cloudBit.castSpell();

            sound.PlaySync();
            sound.PlaySync();
            Close();
        }
    }
}
