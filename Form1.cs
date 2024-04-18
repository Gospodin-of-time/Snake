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
        int headDirection;
        int previousHeadDirection;
        int tailDirection;
        //*Direction: 0 = right, 1 = down, 2 = left, 3 = up
        int headX;
        int headY;
        int tailX;
        int tailY;
        int fieldColor = 0;
        //fieldColor: 0 = blue, 1 = green, 2 = white, 3 = yellow 
        int difficulty = 1;
        //difficulty: 0 = easy, 1 = medium; 2 = hard
        int score;
        int bestScore = 0;
        private bool SpaceLeft()
        {
            bool spaceLeft = false;
            for(int i = 0; i < actualField.GetLength(0); i++)
            {
                for (int j = 0; j < actualField.GetLength(1); j++)
                {
                    if (actualField[i, j] == null || actualField[i, j] == "Food")
                    {
                        spaceLeft = true;
                    }
                }
            }
            if (spaceLeft)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            CreateField(); 
        }
        private void ChooseAssets()
        {
            assets[0] = Resources.Head0Green;
            assets[1] = Resources.Head1Green;
            assets[2] = Resources.Head2Green;
            assets[3] = Resources.Head3Green;
            assets[4] = Resources.Segment0Green;
            assets[5] = Resources.Segment1Green;
            assets[6] = Resources.Segment2Green;
            assets[7] = Resources.Segment3Green;
            assets[8] = Resources.Segment0_1Green;
            assets[9] = Resources.Segment0_3Green;
            assets[10] = Resources.Segment1_0Green;
            assets[11] = Resources.Segment1_2Green;
            assets[12] = Resources.Segment2_1Green;
            assets[13] = Resources.Segment2_3Green;
            assets[14] = Resources.Segment3_0Green;
            assets[15] = Resources.Segment3_2Green;
            assets[16] = Resources.Tail0Green;
            assets[17] = Resources.Tail1Green;
            assets[18] = Resources.Tail2Green;
            assets[19] = Resources.Tail3Green;
            assets[20] = Resources.Apple;
        }
        private void PlaceSnake()
        {
            actualField[2, 3] = "Head0";
            visibleField[2, 3].Image = Resources.Head0Green;
            actualField[2, 2] = "Segment0";
            visibleField[2, 2].Image = Resources.Segment0Green;
            actualField[2, 1] = "Tail0";
            visibleField[2, 1].Image = Resources.Tail0Green;
            headDirection = 0;
            previousHeadDirection = 0;
            tailDirection = 0;
            headX = 3;
            headY = 2;
            tailX = 1;
            tailY = 2;
        }
        private void CreateField()
        {
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
                    actualField[i, j] = null;
                    visibleField[i, j].Visible = true;
                    switch (fieldColor)
                    {
                        case 0:
                            visibleField[i, j].BackColor = Color.LightBlue;
                            break;
                        case 1:
                            visibleField[i, j].BackColor = Color.SeaGreen;
                            break;
                        case 2:
                            visibleField[i, j].BackColor = Color.White;
                            break;
                        case 3:
                            visibleField[i, j].BackColor = Color.Khaki;
                            break;
                    }
                    visibleField[i, j].Image = null;
                }
            }
            ChooseAssets();
            PlaceSnake();
            PlaceFood();
            switch (difficulty)
            {
                case 0:
                    timer1.Interval = 1000;
                    break;
                case 1:
                    timer1.Interval = 700;
                    break;
                case 2:
                    timer1.Interval = 500;
                    break;
            }
            timer1.Enabled = true;
            settingsToolStripMenuItem.Enabled = true;
            button1.Visible = false;
            score = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!SpaceLeft())
            {
                timer1.Stop();
                MessageBox.Show("You win");
                return;
            }
            if (IsSolid())
            {
                timer1.Stop();
                MessageBox.Show("Game over");
                return;
            }
            if (!IsFood())
            {
                MoveTail();
            }
            MoveHead();
            previousHeadDirection = headDirection;
            label1.Text = $"Score: {score}";
            if (score > bestScore)
            {
                bestScore = score;
            }
            label2.Text = $"Best score: {bestScore}";
            //MessageBox.Show($"{actualField[0, 0]}|{actualField[0, 1]}|{actualField[0, 2]}|{actualField[0, 3]}|{actualField[0, 4]}|{actualField[1, 0]}|{actualField[1, 1]}|{actualField[1, 2]}|{actualField[1, 3]}|{actualField[1, 4]}|{actualField[2, 0]}|{actualField[2, 1]}|{actualField[2, 2]}|{actualField[2, 3]}|{actualField[2, 4]}|{actualField[3, 0]}|{actualField[3, 1]}|{actualField[3, 2]}|{actualField[3, 3]}|{actualField[3, 4]}|{actualField[4, 0]}|{actualField[4, 1]}|{actualField[4, 2]}|{actualField[4, 3]}|{actualField[4, 4]}");
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateField();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (headDirection != 2)
            {
                headDirection = 0;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (headDirection != 3)
            {
                headDirection = 1;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (headDirection != 0)
            {
                headDirection = 2;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (headDirection != 1)
            {
                headDirection = 3;
            }
        }
        private void MoveHead()
        {
            switch (headDirection)
            {
                case 0:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 1)
                    {
                        actualField[headY, headX] = "Segment0,1";
                        visibleField[headY, headX].Image = assets[8];
                    }
                    else if (previousHeadDirection == 3)
                    {
                        actualField[headY, headX] = "Segment0,3";
                        visibleField[headY, headX].Image = assets[9];
                    }
                    else
                    {
                        actualField[headY, headX] = "Segment0";
                        visibleField[headY, headX].Image = assets[4];
                    }
                    headX++;
                    actualField[headY, headX] = "Head";
                    visibleField[headY, headX].Image = assets[0];
                    break;
                case 1:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 0)
                    {
                        actualField[headY, headX] = "Segment1,0";
                        visibleField[headY, headX].Image = assets[10];
                    }
                    else if (previousHeadDirection == 2)
                    {
                        actualField[headY, headX] = "Segment1,2";
                        visibleField[headY, headX].Image = assets[11];
                    }
                    else
                    {
                        actualField[headY, headX] = "Segment1";
                        visibleField[headY, headX].Image = assets[5];
                    }
                    headY++;
                    actualField[headY, headX] = "Head";
                    visibleField[headY, headX].Image = assets[1];
                    break;
                case 2:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 1)
                    {
                        actualField[headY, headX] = "Segment2,1";
                        visibleField[headY, headX].Image = assets[12];
                    }
                    else if (previousHeadDirection == 3)
                    {
                        actualField[headY, headX] = "Segment2,3";
                        visibleField[headY, headX].Image = assets[13];
                    }
                    else
                    {
                        actualField[headY, headX] = "Segment2";
                        visibleField[headY, headX].Image = assets[6];
                    }
                    headX--;
                    actualField[headY, headX] = "Head";
                    visibleField[headY, headX].Image = assets[2];
                    break;
                case 3:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 0)
                    {
                        actualField[headY, headX] = "Segment3,0";
                        visibleField[headY, headX].Image = assets[14];
                    }
                    else if (previousHeadDirection == 2)
                    {
                        actualField[headY, headX] = "Segment3,2";
                        visibleField[headY, headX].Image = assets[15];
                    }
                    else
                    {
                        actualField[headY, headX] = "Segment3";
                        visibleField[headY, headX].Image = assets[7];
                    }
                    headY--;
                    actualField[headY, headX] = "Head";
                    visibleField[headY, headX].Image = assets[3];
                    break;
            }
        }

        private void MoveTail()
        {
            switch (tailDirection)
            {
                case 0:
                    if (actualField[tailY, tailX + 1] == "Segment1,0")
                    {
                        tailDirection = 1;
                        visibleField[tailY, tailX + 1].Image = Resources.Tail1Green;
                    }
                    else if (actualField[tailY, tailX + 1] == "Segment3,0")
                    {
                        tailDirection = 3;
                        visibleField[tailY, tailX + 1].Image = Resources.Tail3Green;
                    }
                    else
                    {
                        visibleField[tailY, tailX + 1].Image = Resources.Tail0Green;
                    }
                    actualField[tailY, tailX] = null;
                    visibleField[tailY, tailX].Image = null;
                    tailX++;
                    actualField[tailY, tailX] = "Tail";
                    break;
                case 1:
                    if (actualField[tailY + 1, tailX] == "Segment0,1")
                    {
                        tailDirection = 0;
                        visibleField[tailY + 1, tailX].Image = Resources.Tail0Green;
                    }
                    else if (actualField[tailY + 1, tailX] == "Segment2,1")
                    {
                        tailDirection = 2;
                        visibleField[tailY + 1, tailX].Image = Resources.Tail2Green;
                    }
                    else
                    {
                        visibleField[tailY + 1, tailX].Image = Resources.Tail1Green;
                    }
                    actualField[tailY, tailX] = null;
                    visibleField[tailY, tailX].Image = null;
                    tailY++;
                    actualField[tailY, tailX] = "Tail";
                    break;
                case 2:
                    if (actualField[tailY, tailX - 1] == "Segment1,2")
                    {
                        tailDirection = 1;
                        visibleField[tailY, tailX - 1].Image = Resources.Tail1Green;
                    }
                    else if (actualField[tailY, tailX - 1] == "Segment3,2")
                    {
                        tailDirection = 3;
                        visibleField[tailY, tailX - 1].Image = Resources.Tail3Green;
                    }
                    else
                    {
                        visibleField[tailY, tailX - 1].Image = Resources.Tail2Green;
                    }
                    actualField[tailY, tailX] = null;
                    visibleField[tailY, tailX].Image = null;
                    tailX--;
                    actualField[tailY, tailX] = "Tail";
                    break;
                case 3:
                    if (actualField[tailY - 1, tailX] == "Segment0,3")
                    {
                        tailDirection = 0;
                        visibleField[tailY - 1, tailX].Image = Resources.Tail0Green;
                    }
                    else if (actualField[tailY - 1, tailX] == "Segment2,3")
                    {
                        tailDirection = 2;
                        visibleField[tailY - 1, tailX].Image = Resources.Tail2Green;
                    }
                    else
                    {
                        visibleField[tailY - 1, tailX].Image = Resources.Tail3Green;
                    }
                    actualField[tailY, tailX] = null;
                    visibleField[tailY, tailX].Image = null;
                    tailY--;
                    actualField[tailY, tailX] = "Tail";
                    break;
            }
        }
        private void PlaceFood()
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
        private bool IsFood()
        {
            switch (headDirection)
            {
                case 0:
                    if (actualField[headY, headX + 1] == "Food")
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (actualField[headY + 1, headX] == "Food")
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (actualField[headY, headX - 1] == "Food")
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (actualField[headY - 1, headX] == "Food")
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }
        private bool IsSolid()
        {
            switch (headDirection)
            {
                case 0:
                    if (headX + 1 >= actualField.GetLength(0) || actualField[headY, headX + 1] != null && actualField[headY, headX + 1] != "Food")
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (headY + 1 >= actualField.GetLength(1) || actualField[headY + 1, headX] != null && actualField[headY + 1, headX] != "Food")
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (headX - 1 < 0 || actualField[headY, headX - 1] != null && actualField[headY, headX - 1] != "Food")
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (headY - 1 < 0 || actualField[headY - 1, headX] != null && actualField[headY - 1, headX] != "Food")
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            fieldColor = comboBox2.SelectedIndex;
            difficulty = comboBox4.SelectedIndex;
            groupBox1.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            newGameToolStripMenuItem.Enabled = true;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            button2.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            CreateField();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            groupBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            newGameToolStripMenuItem.Enabled = false;
            for (int i = 0; i < visibleField.GetLength(0); i++)
            {
                for (int j = 0; j < visibleField.GetLength(1); j++)
                {
                    visibleField[i, j].Visible = false;
                }
            }
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            label6.Visible = true;
            label7.Visible = true;
            button2.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            comboBox3.Visible = true;
            comboBox4.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
