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
    }
}
