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
        int heroStep = 5;
        int enemyStep = 3;
        int verVelocity = 0;
        int horVelocity = 0;
        int verEnemyVelocity = 0;
        int horEnemyVelocity = 0;
        int heroImage = 1;
        int enemyImage = 1;
        int score = 0;

        string heroDirection = "right";
        string enemyDirection = "down";
        Random Rand = new Random();

        public Game()
        {
            InitializeComponent();
            SetupGame();
        }

        private void SetRandomEnemyDirection()
        {
            int directionCode = Rand.Next(1, 5);
            if(directionCode == 1)
            {
                enemyDirection = "right";
                verEnemyVelocity = 0;
                horEnemyVelocity = enemyStep;
            }
            else if(directionCode == 2)
            {
                enemyDirection = "down";
                verEnemyVelocity = enemyStep;
                horEnemyVelocity = 0;
            }
            else if (directionCode == 3)
            {
                enemyDirection = "left";
                verEnemyVelocity = 0;
                horEnemyVelocity = -enemyStep;
            }
            else if (directionCode == 4)
            {
                enemyDirection = "up";
                verEnemyVelocity = -enemyStep;
                horEnemyVelocity = 0;
            }
        }

        private void SetupGame()
        {
            this.BackColor = Color.Blue;
            Hero.BackColor = Color.Transparent;
            Hero.SizeMode = PictureBoxSizeMode.StretchImage;
            Hero.Width = 50;
            Hero.Height = 50;

            Food.BackColor = Color.Green;
            Food.BackColor = Color.Transparent;
            Food.Width = 30;
            Food.Height = 30;
            Food.Image = Properties.Resources.food_3;

            Enemy.BackColor = Color.Transparent;
            Enemy.Width = 40;
            Enemy.Height = 40;
            Enemy.SizeMode = PictureBoxSizeMode.StretchImage;

            SetRandomEnemyDirection();

            //initialize interface
            UpdateScoreLabel();
            //initializing timers
            TimerHeroMove.Start();
            TimerHeroAnimate.Start();
            TimerEnemyAnimate.Start();
            TimerEnemyMove.Start();
        }

        private void UpdateScoreLabel()
        {
            ScoreLabel.Text = "Score: " + score;
        }

        private void HeroFoodCollision() 
        {
            if (Hero.Bounds.IntersectsWith(Food.Bounds))
            {
                score += 100;
                UpdateScoreLabel();
                RandomizeFood();
                enemyStep += 1;
            }
        }

        private void RandomizeFood()
        {
            Food.Left = Rand.Next(0, ClientRectangle.Width - Food.Width);
            Food.Top = Rand.Next(0, ClientRectangle.Height - Food.Height);
            Food.Image = (Image)Properties.Resources.ResourceManager.GetObject("food_" + Rand.Next(1, 5));
        }

        private void HeroBorderCollision()
        {
            if(Hero.Top + Hero.Height < 0)
            {
                Hero.Top = ClientRectangle.Height;
            }
            if(Hero.Top > ClientRectangle.Height)
            {
                Hero.Top = 0 - Hero.Height;
            }
            if (Hero.Left + Hero.Width < 0)
            {
                Hero.Left = ClientRectangle.Width;
            }
            if (Hero.Left > ClientRectangle.Width)
            {
                Hero.Left = 0 - Hero.Width;
            }
        }

        private void HeroEnemyCollision()
        {
            if (Hero.Bounds.IntersectsWith(Enemy.Bounds))
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            TimerHeroMove.Stop();
            TimerHeroAnimate.Stop();
            TimerEnemyAnimate.Stop();
            TimerEnemyMove.Stop();
            heroImage = 0;
            TimerHeroMelt.Start();
            Food.Visible = false;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                verVelocity = -heroStep;
                horVelocity = 0;
                heroDirection = "up";
            }
            else if(e.KeyCode == Keys.Down)
            {
                verVelocity = heroStep;
                horVelocity = 0;
                heroDirection = "down";
            }
            else if (e.KeyCode == Keys.Left)
            {
                verVelocity = 0;
                horVelocity = -heroStep;
                heroDirection = "left";

            }
            else if (e.KeyCode == Keys.Right)
            {
                verVelocity = 0;
                horVelocity = heroStep;
                heroDirection = "right";
            }
            SetRandomEnemyDirection();
        }

        private void TimerHeroMove_Tick(object sender, EventArgs e)
        {
            Hero.Top += verVelocity;
            Hero.Left += horVelocity;
            HeroBorderCollision();
            HeroFoodCollision();
            HeroEnemyCollision();
        }

        private void TimerHeroAnimate_Tick(object sender, EventArgs e)
        {
            string heroImageName;
            heroImageName = "pacman_" + heroDirection + "_" + heroImage;
            Hero.Image = (Image)Properties.Resources.ResourceManager.GetObject(heroImageName);
            heroImage += 1; //heroImage++
            if(heroImage > 4)
            {
                heroImage = 1;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TimerEnemyAnimate_Tick(object sender, EventArgs e)
        {
            string enemyImageName;
            enemyImageName = "enemy_" + enemyDirection + "_" + enemyImage;
            Enemy.Image = (Image)Properties.Resources.ResourceManager.GetObject(enemyImageName);
            enemyImage += 1;
            if (enemyImage > 2)
            {
                enemyImage = 1;
            }
        }

        private void TimerHeroMelt_Tick(object sender, EventArgs e)
        {
            string heroImageName;
            heroImageName = "pacman_melt_" + heroImage;
            Hero.Image = (Image)Properties.Resources.ResourceManager.GetObject(heroImageName);
            heroImage += 1; //heroImage++
            if (heroImage > 14)
            {
                TimerHeroMelt.Stop();
                LabelGameOver.Visible = true;
            }
        }

        private void TimerEnemyMove_Tick(object sender, EventArgs e)
        {
            Enemy.Top += verEnemyVelocity;
            Enemy.Left += horEnemyVelocity;
            EnemyBorderCollisin();
        }

        private void EnemyBorderCollisin()
        {
            if (Enemy.Top < 0)
            {
                enemyDirection = "down";
                verEnemyVelocity = enemyStep;
                horEnemyVelocity = 0;
            }
            else if (Enemy.Top + Enemy.Height > ClientRectangle.Height)
            {
                enemyDirection = "up";
                verEnemyVelocity = -enemyStep;
                horEnemyVelocity = 0;
            }
            else if (Enemy.Left < 0)
            {
                enemyDirection = "right";
                verEnemyVelocity = 0;
                horEnemyVelocity = enemyStep;
            }
            else if (Enemy.Left + Enemy.Width > ClientRectangle.Width)
            {
                enemyDirection = "left";
                verEnemyVelocity = 0;
                horEnemyVelocity = -enemyStep;
            }
        }
    }
}
