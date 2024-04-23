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
        int snakeColor = 0;
        //snakeColor: 0 = green, 1 = blue, 2 = gray
        int foodType = 0;
        //foodType: 0 = apple, 1 = orange, 2 = banana, 3 = pear
        int score;
        int bestScore = 0;
        private bool SpaceLeft()
        {
            bool spaceLeft = false;
            for(int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    if (field[i, j].Image == null || field[i, j].Image == (assets[20]))
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
            switch (snakeColor)
            {
                case 0:
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
                    break;
                case 1:
                    assets[0] = Resources.Head0Blue;
                    assets[1] = Resources.Head1Blue;
                    assets[2] = Resources.Head2Blue;
                    assets[3] = Resources.Head3Blue;
                    assets[4] = Resources.Segment0Blue;
                    assets[5] = Resources.Segment1Blue;
                    assets[6] = Resources.Segment2Blue;
                    assets[7] = Resources.Segment3Blue;
                    assets[8] = Resources.Segment0_1Blue;
                    assets[9] = Resources.Segment0_3Blue;
                    assets[10] = Resources.Segment1_0Blue;
                    assets[11] = Resources.Segment1_2Blue;
                    assets[12] = Resources.Segment2_1Blue;
                    assets[13] = Resources.Segment2_3Blue;
                    assets[14] = Resources.Segment3_0Blue;
                    assets[15] = Resources.Segment3_2Blue;
                    assets[16] = Resources.Tail0Blue;
                    assets[17] = Resources.Tail1Blue;
                    assets[18] = Resources.Tail2Blue;
                    assets[19] = Resources.Tail3Blue;
                    break;
                case 2:
                    assets[0] = Resources.Head0Gray;
                    assets[1] = Resources.Head1Gray;
                    assets[2] = Resources.Head2Gray;
                    assets[3] = Resources.Head3Gray;
                    assets[4] = Resources.Segment0Gray;
                    assets[5] = Resources.Segment1Gray;
                    assets[6] = Resources.Segment2Gray;
                    assets[7] = Resources.Segment3Gray;
                    assets[8] = Resources.Segment0_1Gray;
                    assets[9] = Resources.Segment0_3Gray;
                    assets[10] = Resources.Segment1_0Gray;
                    assets[11] = Resources.Segment1_2Gray;
                    assets[12] = Resources.Segment2_1Gray;
                    assets[13] = Resources.Segment2_3Gray;
                    assets[14] = Resources.Segment3_0Gray;
                    assets[15] = Resources.Segment3_2Gray;
                    assets[16] = Resources.Tail0Gray;
                    assets[17] = Resources.Tail1Gray;
                    assets[18] = Resources.Tail2Gray;
                    assets[19] = Resources.Tail3Gray;
                    break;
            }
            switch (foodType)
            {
                case 0:
                    assets[20] = Resources.Apple;
                    break;
                case 1:
                    assets[20] = Resources.Orange;
                    break;
                case 2:
                    assets[20] = Resources.Banana;
                    break;
                case 3:
                    assets[20] = Resources.Pear;
                    break;
            }
        }
        private void PlaceSnake()
        {
            field[2, 3].Image = assets[0];
            field[2, 2].Image = assets[4];
            field[2, 1].Image = assets[16];
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
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j].Image = null;
                    field[i, j].Visible = true;
                    switch (fieldColor)
                    {
                        case 0:
                            field[i, j].BackColor = Color.LightBlue;
                            break;
                        case 1:
                            field[i, j].BackColor = Color.SeaGreen;
                            break;
                        case 2:
                            field[i, j].BackColor = Color.White;
                            break;
                        case 3:
                            field[i, j].BackColor = Color.Khaki;
                            break;
                    }
                    field[i, j].Image = null;
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
                        field[headY, headX].Image = assets[8];
                    }
                    else if (previousHeadDirection == 3)
                    {
                        field[headY, headX].Image = assets[9];
                    }
                    else
                    {
                        field[headY, headX].Image = assets[4];
                    }
                    headX++;
                    field[headY, headX].Image = assets[0];
                    break;
                case 1:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 0)
                    {
                        field[headY, headX].Image = assets[10];
                    }
                    else if (previousHeadDirection == 2)
                    {
                        field[headY, headX].Image = assets[11];
                    }
                    else
                    {
                        field[headY, headX].Image = assets[5];
                    }
                    headY++;
                    field[headY, headX].Image = assets[1];
                    break;
                case 2:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 1)
                    {
                        field[headY, headX].Image = assets[12];
                    }
                    else if (previousHeadDirection == 3)
                    {
                        field[headY, headX].Image = assets[13];
                    }
                    else
                    {
                        field[headY, headX].Image = assets[6];
                    }
                    headX--;
                    field[headY, headX].Image = assets[2];
                    break;
                case 3:
                    if (IsFood())
                    {
                        score++;
                        PlaceFood();
                    }
                    if (previousHeadDirection == 0)
                    {
                        field[headY, headX].Image = assets[14];
                    }
                    else if (previousHeadDirection == 2)
                    {
                        field[headY, headX].Image = assets[15];
                    }
                    else
                    {
                        field[headY, headX].Image = assets[7];
                    }
                    headY--;
                    field[headY, headX].Image = assets[3];
                    break;
            }
        }

        private void MoveTail()
        {
            switch (tailDirection)
            {
                case 0:
                    if (field[tailY, tailX + 1].Image == assets[10])
                    {
                        tailDirection = 1;
                        field[tailY, tailX + 1].Image = assets[17];
                    }
                    else if (field[tailY, tailX + 1].Image == assets[14])
                    {
                        tailDirection = 3;
                        field[tailY, tailX + 1].Image = assets[19];
                    }
                    else
                    {
                        field[tailY, tailX + 1].Image = assets[16];
                    }
                    field[tailY, tailX].Image = null;
                    tailX++;
                    break;
                case 1:
                    if (field[tailY + 1, tailX].Image == assets[8])
                    {
                        tailDirection = 0;
                        field[tailY + 1, tailX].Image = assets[16];
                    }
                    else if (field[tailY + 1, tailX].Image == assets[12])
                    {
                        tailDirection = 2;
                        field[tailY + 1, tailX].Image = assets[18];
                    }
                    else
                    {
                        field[tailY + 1, tailX].Image = assets[17];
                    }
                    field[tailY, tailX].Image = null;
                    tailY++;
                    break;
                case 2:
                    if (field[tailY, tailX - 1].Image == assets[11])
                    {
                        tailDirection = 1;
                        field[tailY, tailX - 1].Image = assets[17];
                    }
                    else if (field[tailY, tailX - 1].Image == assets[15])
                    {
                        tailDirection = 3;
                        field[tailY, tailX - 1].Image = assets[19];
                    }
                    else
                    {
                        field[tailY, tailX - 1].Image = assets[18];
                    }
                    field[tailY, tailX].Image = null;
                    tailX--;
                    break;
                case 3:
                    if (field[tailY - 1, tailX].Image == assets[9])
                    {
                        tailDirection = 0;
                        field[tailY - 1, tailX].Image = assets[16];
                    }
                    else if (field[tailY - 1, tailX].Image == assets[13])
                    {
                        tailDirection = 2;
                        field[tailY - 1, tailX].Image = assets[18];
                    }
                    else
                    {
                        field[tailY - 1, tailX].Image = assets[19];
                    }
                    field[tailY, tailX].Image = null;
                    tailY--;
                    break;
            }
        }
        private void PlaceFood()
        {
            Random r = new Random();
            while (true)
            {
                int foodX = r.Next(0, field.GetLength(0));
                int foodY = r.Next(0, field.GetLength(1));
                if (field[foodY, foodX].Image == null)
                {
                    field[foodY, foodX].Image = assets[20];
                    break;
                }
            }
        }
        private bool IsFood()
        {
            switch (headDirection)
            {
                case 0:
                    if (field[headY, headX + 1].Image == assets[20])
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (field[headY + 1, headX].Image == assets[20])
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (field[headY, headX - 1].Image == assets[20])
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (field[headY - 1, headX].Image == assets[20])
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
                    if (headX + 1 >= field.GetLength(0) || field[headY, headX + 1].Image != null && field[headY, headX + 1].Image != assets[20])
                    {
                        return true;
                    }
                    break;
                case 1:
                    if (headY + 1 >= field.GetLength(1) || field[headY + 1, headX].Image != null && field[headY + 1, headX].Image != assets[20])
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (headX - 1 < 0 || field[headY, headX - 1].Image != null && field[headY, headX - 1].Image != assets[20])
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (headY - 1 < 0 || field[headY - 1, headX].Image != null && field[headY - 1, headX].Image != assets[20])
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Eat food to grow \r\nDon't bump into walls or your own body");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            snakeColor = comboBox1.SelectedIndex;
            fieldColor = comboBox2.SelectedIndex;
            foodType = comboBox3.SelectedIndex;
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
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j].Visible = false;
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
