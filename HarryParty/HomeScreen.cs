using System;
using System.Windows.Forms;

namespace WiiWandz
{
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void btnAraniaExumai_Click(object sender, EventArgs e)
        {
           new AraniaExumai().Show();
        }

        private void btnExpectoPatronum_Click(object sender, EventArgs e)
        {
            new ExpectoPatronum().Show();
        }

        private void btnSortingHat_Click(object sender, EventArgs e)
        {
            new SortingHat().Show();
        }

        private void btnHedwig_Click(object sender, EventArgs e)
        {
            new Hedwig().Show();
        }

        private void btnWerewolf_Click(object sender, EventArgs e)
        {
            new Werewolf().Show();
        }

        private void btnLumos_Click(object sender, EventArgs e)
        {
            new Lumos().Show();
        }
    }
}
