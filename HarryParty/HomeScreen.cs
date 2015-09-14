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
    public partial class HomeScreen : Form
    {
        public HomeScreen()
        {
            InitializeComponent();
        }

        private void btnAraniaExumai_Click(object sender, EventArgs e)
        {
            AraniaExumai form = new AraniaExumai();
            form.Show();
        }

        private void btnExpectoPatronum_Click(object sender, EventArgs e)
        {
            ExpectoPatronum form = new ExpectoPatronum();
            form.Show();
        }

        private void btnSortingHat_Click(object sender, EventArgs e)
        {
            SortingHat form = new SortingHat();
            form.Show();
        }

        private void btnHedwig_Click(object sender, EventArgs e)
        {
            Hedwig form = new Hedwig();
            form.Show();
        }

        private void btnWerewolf_Click(object sender, EventArgs e)
        {
            Werewolf form = new Werewolf();
            form.Show();
        }

        private void btnLumos_Click(object sender, EventArgs e)
        {
            Lumos form = new Lumos();
            form.Show();
        }
    }
}
