using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Головоломка
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Button[,] field = new Button[5, 5];
        short direction;
        short snakeHeadX;
        short snakeHeadY;
        private void button26_Click(object sender, EventArgs e)
        {
            button26.Visible = false;
            CreateField(field); 
        }
        private void CreateField(Button[,] field)
        {
            field[0, 0] = button1;
            field[1, 0] = button2;
            field[2, 0] = button3;
            field[3, 0] = button4;
            field[4, 0] = button5;

            field[0, 1] = button6;
            field[1, 1] = button7;
            field[2, 1] = button8;
            field[3, 1] = button9;
            field[4, 1] = button10;

            field[0, 2] = button11;
            field[1, 2] = button12;
            field[2, 2] = button13;
            field[3, 2] = button14;
            field[4, 2] = button15;

            field[0, 3] = button16;
            field[1, 3] = button17;
            field[2, 3] = button18;
            field[3, 3] = button19;
            field[4, 3] = button20;

            field[0, 4] = button21;
            field[1, 4] = button22;
            field[2, 4] = button23;
            field[3, 4] = button24;
            field[4, 4] = button25;
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(0); j++)
                {
                    field[i, j].Visible = true;
                    field[i, j].Enabled = true;
                }
            }
            //direction: 0 = right, 1 = down, 2 = left, 3 = up
            direction = 0;
            snakeHeadX = 2;
            snakeHeadY = 2;
            timer1.Enabled = true;
        }

        private void ElementClick(object sender, EventArgs e)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j] == sender)
                    {
                        if (direction == 0 || direction == 2)
                        {
                            if (j == snakeHeadY + 1)
                            {
                                direction = 1;
                            }
                            else if (j == snakeHeadY - 1)
                            {
                                direction = 3;
                            }
                        }
                        else if (direction == 1 || direction == 3)
                        {
                            if (i == snakeHeadX + 1)
                            {
                                direction = 0;
                            }
                            else if (i == snakeHeadX - 1)
                            {
                                direction = 2;
                            }
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (direction == 0)
            {
                if (snakeHeadX + 1 >= field.GetLength(0))
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Game over");
                }
                else
                {
                    snakeHeadX++;
                }
            }
            else if (direction == 1)
            {
                if (snakeHeadY + 1 >= field.GetLength(1) || field[snakeHeadX, snakeHeadY++].Text != "")
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Game over");
                }
                else
                {
                    snakeHeadY++;
                }
            }
            else if (direction == 2)
            {
                if (snakeHeadX - 1 < 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Game over");
                }
                else
                {
                    snakeHeadX--;
                }
            }
            else if (direction == 3)
            {
                if (snakeHeadY - 1 < 0)
                {
                    timer1.Enabled = false;
                    MessageBox.Show("Game over");
                }
                else
                {
                    snakeHeadY--;
                }
            }
            field[snakeHeadX, snakeHeadY].Text = "snakeHead";
            label3.Text = snakeHeadX.ToString();
            label4.Text = snakeHeadY.ToString();
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateField(field);
        }
    }
}
