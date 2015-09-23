using System;
using System.Collections.Generic;
using System.Media;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WiiWandz
{
    public partial class SortingHat : Form
    {
        private List<SoundPlayer> starting;
        private List<SoundPlayer> choosingGryffindor;
        private List<SoundPlayer> choosingRavenclaw;
        private List<SoundPlayer> choosingSlytherin;
        private List<SoundPlayer> choosingHufflepuff;
        private List<SoundPlayer> pronouncement;
        private List<SoundPlayer> gryffindor;
        private List<SoundPlayer> slytherin;
        private List<SoundPlayer> hufflepuff;
        private List<SoundPlayer> ravenclaw;

        public SortingHat()
        {
            InitializeComponent();

            KeyPreview = true;
            KeyPress += new KeyPressEventHandler(HandleKeys);
            btnSortingHat.KeyPress += new KeyPressEventHandler(HandleKeys);

            getSound();
        }

        private void btnSortingHat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            Thread t = null;
            switch (e.KeyChar)
            {
                case '0':
                    t = new Thread(chooseRandomHouse);
                    break;
                case '1':
                    t = new Thread(chooseGryffindor);
                    break;
                case '2':
                    t = new Thread(chooseRavenclaw);
                    break;
                case '3':
                    t = new Thread(chooseHufflepuff);
                    break;
                case '4':
                    t = new Thread(chooseSlytherin);
                    break;
                case '9':
                    t = new Thread(chooseGryffindorExtended);
                    break;

            }
            if (t != null)
            {
                t.Start();
            }
            e.Handled = true;

        }
  
        private void playRandom(List<SoundPlayer> sounds)
        {
            var rnd = new Random();
            int toPlay = rnd.Next(0, starting.Count);
            starting.ElementAt(toPlay).PlaySync();

            toPlay = rnd.Next(0, sounds.Count);
            sounds.ElementAt(toPlay).PlaySync();

            toPlay = rnd.Next(0, pronouncement.Count);
            pronouncement.ElementAt(toPlay).PlaySync();
        }

        private void chooseRandomHouse()
        {
            var rnd = new Random();
            int toPlay = rnd.Next(0, 4);

            switch (toPlay)
            {
                case 0:
                    chooseGryffindor();
                    break;
                case 1:
                    chooseHufflepuff();
                    break;
                case 2:
                    chooseRavenclaw();
                    break;
                case 3:
                    chooseSlytherin();
                    break;

            }
        }

        private void chooseGryffindorExtended()
        {
            playRandom(choosingGryffindor);

            BackgroundImage = Properties.Resources.sorting_hat_bg_gryffindor;

            var rnd = new Random();
            int toPlay = rnd.Next(0, gryffindor.Count);
            gryffindor.ElementAt(toPlay).PlaySync();

            reset();
        }

        private void chooseSlytherin()
        {
            playRandom(choosingSlytherin);

            BackgroundImage = Properties.Resources.sorting_hat_bg_slytherin;

            var rnd = new Random();
            int toPlay = rnd.Next(0, slytherin.Count);
            slytherin.ElementAt(toPlay).PlaySync();

            reset();
        }

        private void chooseHufflepuff()
        {
            playRandom(choosingHufflepuff);

            BackgroundImage = Properties.Resources.sorting_hat_bg_hufflepuff;

            var rnd = new Random();
            int toPlay = rnd.Next(0, hufflepuff.Count);
            hufflepuff.ElementAt(toPlay).PlaySync();

            reset();
        }

        private void chooseRavenclaw()
        {
            playRandom(choosingRavenclaw);

            BackgroundImage = Properties.Resources.sorting_hat_bg_ravenclaw;

            var rnd = new Random();
            int toPlay = rnd.Next(0, ravenclaw.Count);
            ravenclaw.ElementAt(toPlay).PlaySync();

            reset();
        }

        private void chooseGryffindor()
        {
            playRandom(choosingGryffindor);

            BackgroundImage = Properties.Resources.sorting_hat_bg_gryffindor;

            var rnd = new Random();
            int toPlay = rnd.Next(0, gryffindor.Count);
            gryffindor.ElementAt(toPlay).PlaySync();

            reset();
        }

        private void reset()
        {
            BackgroundImage = Properties.Resources.hogwarts_logo_wood;
        }

        #region Helpers

        private void getSound()
        {
            const string PATH = @"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\";

            var starting = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_starting_kit_ah_certo.wav"),
                new SoundPlayer(PATH + "sorting_hat_starting_kit_dificil.wav"),
                new SoundPlayer(PATH + "sorting_hat_starting_kit_muito_dificil.wav"),
                new SoundPlayer(PATH + "sorting_hat_starting_kit_piolhos.wav"),
                new SoundPlayer(PATH + "sorting_hat_starting_kit_vamos_pensar.wav"),
                new SoundPlayer(PATH + "sorting_hat_starting_kit_vejamos.wav")
            };

            var choosingGryffindor = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_chule.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_coragem.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_fedendo.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_outra_para_esta_crianca.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_talento.wav")
            };

            var choosingRavenclaw = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_chule.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_esperta.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_fedendo.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_outra_para_esta_crianca.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_talento.wav")
            };

            var choosingHufflepuff = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_chule.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_fedendo.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_outra_para_esta_crianca.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_talento.wav")
            };

            var choosingSlytherin = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_chule.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_fedendo.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_pode_ser_grande.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_outra_para_esta_crianca.wav"),
                new SoundPlayer(PATH + "sorting_hat_choosing_kit_talento.wav")
            };

            var pronouncement = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_decidi.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_esta_ja_sei.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_esta_sera.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_este_aqui.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_hunh_ja_sei.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_ja_sei.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_sei_muito_bem.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_sera.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_vai_para.wav"),
                new SoundPlayer(PATH + "sorting_hat_pronouncement_kit_vou_escolher.wav")
            };

            var gryffindor = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_gryffindor_1.wav"),
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_gryffindor_2.wav"),
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_gryffindor_3.wav")
            };

            var slytherin = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_slytherin_1.wav"),
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_slytherin_2.wav")
            };

            var hufflepuff = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_hufflepuff_1.wav"),
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_hufflepuff_2.wav")
            };

            var ravenclaw = new List<SoundPlayer>()
            {
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_ravenclaw_1.wav"),
                new SoundPlayer(PATH + "sorting_hat_chosen_kit_ravenclaw_1.wav")
            };
        }

        #endregion Helpers
    }
}
