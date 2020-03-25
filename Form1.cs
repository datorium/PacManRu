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
    public partial class Game : Form
    {
        public Game()
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

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                Hero.Top -= 5;
            }
            else if(e.KeyCode == Keys.Down)
            {
                Hero.Top += 5;
            }
            else if (e.KeyCode == Keys.Left)
            {
                Hero.Left -= 5;
            }
            else if (e.KeyCode == Keys.Right)
            {
                Hero.Left += 5;
            }
        }
    }
}
