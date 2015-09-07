using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WiiWandz
{
    public partial class SortingHat : Form
    {
        private List<System.Media.SoundPlayer> players;
        private List<System.Media.SoundPlayer> houses;

        public SortingHat()
        {
            InitializeComponent();

            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);
            this.btnSortingHat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);

            players = new List<System.Media.SoundPlayer>();
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_ah_certo.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_ah_muito_bem.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_coragem.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_dificil.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_ja_sei.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_melhor_que_seja.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_mente.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_mmm.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_mmm_certo.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_muito_bem.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_muito_dificil.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_nao.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_onde_colocar.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_sei_muito_bem.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_ser_grande.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_talento.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_vejamos.wav"));
            players.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_choosing_vontade.wav"));

            houses = new List<System.Media.SoundPlayer>();
            houses.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_chosen_gryffindor.wav"));
            houses.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_chosen_gryffindor.wav"));
            houses.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_chosen_hufflepuff.wav"));
            houses.Add(new System.Media.SoundPlayer(@"C:\Users\CLARISSA RAMOS\Documents\GitHub\wiiwandz\HarryParty\media\sounds\sorting_hat_chosen_slytherin.wav"));
        }

        private void btnSortingHat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HandleKeys(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)13:
                    chooseRandom();
                    break;
                case '1':
                    chooseGryffindor();
                    break;
                case '2':
                    chooseRavenclaw();
                    break;
                case '3':
                    chooseHufflepuff();
                    break;
                case '4':
                    chooseSlytherin();
                    break;
                case '9':
                    chooseGryffindorExtended();
                    break;
                case ' ':
                    chooseRandom();
                    break;

            }
            e.Handled = true;

        }
  
        private void playChoosing()
        {
            Random rnd = new Random();
            int toPlay = rnd.Next(1, players.Count) - 1;
            players.ElementAt(toPlay).Play();
        }
        

        private void chooseRandom()
        {
            playChoosing();
            Random rnd = new Random();
            int toPlay = rnd.Next(0, 3);
            houses.ElementAt(toPlay).Play();
        }

        private void chooseGryffindorExtended()
        {
            playChoosing();
            //houses.ElementAt(0).Play();
        }

        private void chooseSlytherin()
        {
            playChoosing();
            //houses.ElementAt(3).Play();
        }

        private void chooseHufflepuff()
        {
            playChoosing();
            //houses.ElementAt(2).Play();
        }

        private void chooseRavenclaw()
        {
            playChoosing();
            //houses.ElementAt(1).Play();
        }

        private void chooseGryffindor()
        {
            playChoosing();
            //houses.ElementAt(0).Play();
        }
    }
}
