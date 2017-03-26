using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Spells;

namespace WiiWandz
{
    public partial class Lumos : Form
    {
        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        private const string SOUND = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\lumos_maxima.wav";

        WandHandler wandHandler;
        SoundPlayer sound;

        public Lumos()
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
                case (char)13:
                    endLumos();
                    break;
            }
            e.Handled = true;

        }

        void castSpell()
        {
            // TODO: probably need to run this in a separate thread
            var spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_random_color",
                "hue_bright",
                1
            );

            spell.castSpell();

            sound.PlaySync();
        }

        void endLumos()
        {
            wandHandler.StopTracking();

            var spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_dim",
                "hue_spell_off",
                1
            );

            spell.castSpell();

            Close();
        }
    }
}
