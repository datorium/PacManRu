using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PacManRu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupGame();
        }

        private void SetupGame()
        {
            Hero.BackColor = Color.DarkSalmon;
            Food.BackColor = Color.Green;
            Enemy.BackColor = Color.Blue;
        }

    }
}
