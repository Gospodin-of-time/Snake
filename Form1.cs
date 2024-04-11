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
        PictureBox[,] field = new PictureBox[5, 5];
        int direction;
        int snakeHeadX;
        int snakeHeadY;
        private void button26_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            CreateField(ref field); 
        }
        private void CreateField(ref PictureBox[,] field)
        {
            field[0, 0] = pictureBox5;
            field[0, 1] = pictureBox6;
            field[0, 2] = pictureBox7;
            field[0, 3] = pictureBox8;
            field[0, 4] = pictureBox9;

            field[1, 0] = pictureBox10;
            field[1, 1] = pictureBox11;
            field[1, 2] = pictureBox12;
            field[1, 3] = pictureBox13;
            field[1, 4] = pictureBox14;

            field[2, 0] = pictureBox15;
            field[2, 1] = pictureBox16;
            field[2, 2] = pictureBox17;
            field[2, 3] = pictureBox18;
            field[2, 4] = pictureBox19;

            field[3, 0] = pictureBox20;
            field[3, 1] = pictureBox21;
            field[3, 2] = pictureBox22;
            field[3, 3] = pictureBox23;
            field[3, 4] = pictureBox24;

            field[4, 0] = pictureBox25;
            field[4, 1] = pictureBox26;
            field[4, 2] = pictureBox27;
            field[4, 3] = pictureBox28;
            field[4, 4] = pictureBox29;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {

                }
            }
            //direction: 0 = right, 1 = down, 2 = left, 3 = up
            direction = 0;
            snakeHeadX = 2;
            snakeHeadY = 2;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (direction)
            {
                case 0:
                    if (snakeHeadX + 1 >= field.GetLength(0) || field[snakeHeadY, snakeHeadX + 1].Image == Resources.snakeHead)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        snakeHeadX++;
                    }
                    break;
                case 1:
                    if (snakeHeadY + 1 >= field.GetLength(1) || field[snakeHeadY + 1, snakeHeadX].Image == Resources.snakeHead)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        snakeHeadY++;
                    }
                    break;
                case 2:
                    if (snakeHeadX - 1 < 0 || field[snakeHeadY, snakeHeadX - 1].Image == Resources.snakeHead)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        snakeHeadX--;
                    }
                    break;
                case 3:
                    if (snakeHeadY - 1 < 0 || field[snakeHeadY - 1, snakeHeadX].Image == Resources.snakeHead)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                    }
                    else
                    {
                        snakeHeadY--;
                    }
                    break;
            }
            field[snakeHeadY, snakeHeadX].Image = Resources.snakeHead;
            label3.Text = snakeHeadX.ToString();
            label4.Text = snakeHeadY.ToString();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateField(ref field);
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
    }
}
