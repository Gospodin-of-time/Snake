using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Головоломка.Properties;

namespace Головоломка
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PictureBox[,] visibleField = new PictureBox[5, 5];
        string[,] actualField = new string[5, 5];
        Image[] assets = new Image[21];
        //assets: 0/1/2/3 - Head0/1/2/3
        //assets: 4/5/6/7 - Segment0/1/2/3
        //assets: 8/9/10/11/12/13/14/15 - Segment0.1/0.3/1.0/1.2/2.1/2.3/3.0/3.2
        //assets: 16/17/18/19 - Tail0/1/2/3
        //assets: 20 - Food
        int direction;
        //direction: 0 = right, 1 = down, 2 = left, 3 = up
        int headX;
        int headY;
        private void button26_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            CreateField(); 
        }
        private void CreateField()
        {
            assets[0] = Resources.Head0;
            assets[1] = Resources.Head1;
            assets[2] = Resources.Head2;
            assets[3] = Resources.Head3;
            assets[20] = Resources.Food;
            visibleField[0, 0] = pictureBox5;
            visibleField[0, 1] = pictureBox6;
            visibleField[0, 2] = pictureBox7;
            visibleField[0, 3] = pictureBox8;
            visibleField[0, 4] = pictureBox9;

            visibleField[1, 0] = pictureBox10;
            visibleField[1, 1] = pictureBox11;
            visibleField[1, 2] = pictureBox12;
            visibleField[1, 3] = pictureBox13;
            visibleField[1, 4] = pictureBox14;

            visibleField[2, 0] = pictureBox15;
            visibleField[2, 1] = pictureBox16;
            visibleField[2, 2] = pictureBox17;
            visibleField[2, 3] = pictureBox18;
            visibleField[2, 4] = pictureBox19;

            visibleField[3, 0] = pictureBox20;
            visibleField[3, 1] = pictureBox21;
            visibleField[3, 2] = pictureBox22;
            visibleField[3, 3] = pictureBox23;
            visibleField[3, 4] = pictureBox24;

            visibleField[4, 0] = pictureBox25;
            visibleField[4, 1] = pictureBox26;
            visibleField[4, 2] = pictureBox27;
            visibleField[4, 3] = pictureBox28;
            visibleField[4, 4] = pictureBox29;
            for (int i = 0; i < visibleField.GetLength(0); i++)
            {
                for (int j = 0; j < visibleField.GetLength(1); j++)
                {
                    visibleField[i, j].Visible = true;
                    visibleField[i, j].BackColor = Color.LightBlue;
                }
            }
            visibleField[2, 3].Image = Resources.Head0;
            direction = 0;
            headX = 3;
            headY = 2;
            FoodPlacer();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            HeadMovement(ref headX, ref headY);
            label3.Text = headX.ToString();
            label4.Text = headY.ToString();
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateField();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (direction != 2)
            {
                direction = 0;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (direction != 3)
            {
                direction = 1;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (direction != 0)
            {
                direction = 2;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (direction != 1)
            {
                direction = 3;
            }
        }
        private void HeadMovement(ref int headX, ref int headY)
        {
            switch (direction)
            {
                case 0:
                    if (headX + 1 >= actualField.GetLength(0) || actualField[headY, headX + 1] != null && actualField[headY, headX + 1] != "Food")
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        if (actualField[headY, headX + 1] == "Food")
                        {
                            FoodPlacer();
                        }
                        headX++;
                    }
                    actualField[headY, headX] = "Head0";
                    visibleField[headY, headX].Image = assets[0];
                    break;
                case 1:
                    if (headY + 1 >= actualField.GetLength(1) || actualField[headY + 1, headX] != null && actualField[headY + 1, headX] != "Food")
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        if (actualField[headY + 1, headX] == "Food")
                        {
                            FoodPlacer();
                        }
                        headY++;
                    }
                    actualField[headY, headX] = "Head1";
                    visibleField[headY, headX].Image = assets[1];
                    break;
                case 2:
                    if (headX - 1 < 0 || actualField[headY, headX - 1] != null && actualField[headY, headX - 1] != "Food")
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        if (actualField[headY, headX - 1] == "Food")
                        {
                            FoodPlacer();
                        }
                        headX--;
                    }
                    actualField[headY, headX] = "Head2";
                    visibleField[headY, headX].Image = assets[2];
                    break;
                case 3:
                    if (headY - 1 < 0 || actualField[headY - 1, headX] != null && actualField[headY - 1, headX] != "Food")
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        if (actualField[headY - 1, headX] == "Food")
                        {
                            FoodPlacer();
                        }
                        headY--;
                    }
                    actualField[headY, headX] = "Head3";
                    visibleField[headY, headX].Image = assets[3];
                    break;
            }
        }

        private void TailMovement()
        {

        }
        private void FoodPlacer()
        {
            Random r = new Random();
            while (true)
            {
                int foodX = r.Next(0, actualField.GetLength(0));
                int foodY = r.Next(0, actualField.GetLength(1));
                if (actualField[foodY, foodX] == null)
                {
                    actualField[foodY, foodX] = "Food";
                    visibleField[foodY, foodX].Image = assets[20];
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
