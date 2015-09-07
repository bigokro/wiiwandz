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
        public SortingHat()
        {
            InitializeComponent();

            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);
            this.btnSortingHat.KeyPress += new System.Windows.Forms.KeyPressEventHandler(HandleKeys);
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

        private void chooseRandom()
        {
            MessageBox.Show("ex.Message", "Wiimote not found error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void chooseGryffindorExtended()
        {
            throw new NotImplementedException();
        }

        private void chooseSlytherin()
        {
            throw new NotImplementedException();
        }

        private void chooseHufflepuff()
        {
            throw new NotImplementedException();
        }

        private void chooseRavenclaw()
        {
            throw new NotImplementedException();
        }

        private void chooseGryffindor()
        {
            throw new NotImplementedException();
        }
    }
}
