using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WiimoteLib;
using WiiWandz.Spells;

namespace WiiWandz
{
    public partial class Hedwig : Form
    {
        private delegate void UpdateWiimoteStateDelegate(WiimoteChangedEventArgs args);
        private delegate void UpdateExtensionChangedDelegate(WiimoteExtensionChangedEventArgs args);

        int count = 0;
        Boolean spellCast, shownEnd;
        WandHandler wandHandler;

        public Hedwig()
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

            this.spellCast = false;
            this.shownEnd = false;

            this.KeyPreview = true;
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);

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
            spellCast = true;
            // todo... stop tracking
            //Application.RemoveMessageFilter(gmh);

            IftttStartStopSpell spell = new IftttStartStopSpell(
                "bslEohHzR8x_HsJ3vWzxub",
                "hue_arania_exumai_on",
                "hue_spell_off",
                5);
            spell.castSpell();

            Incendio cloudBit = new Incendio("00e04c034e9a", "c83578c843ac46220849a1bd919662b340e537dd14f0b234a5a99634becc5339", 50, 1000, null, null);
            cloudBit.castSpell();

            pbStrokes.Visible = false;
            shownEnd = true;
        }


    }
}
