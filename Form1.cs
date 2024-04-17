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
        //headDirection: 0 = right, 1 = down, 2 = left, 3 = up
        int headX;
        int headY;
        int tailX;
        int tailY;
        private void SpaceLeft()
        {
            bool spaceLeft = false;
            for(int i = 0; i < actualField.GetLength(0); i++)
            {
                for (int j = 0; j < actualField.GetLength(1); j++)
                {
                    if (actualField[i, j] != null)
                    {
                        spaceLeft = true;
                    }
                }
            }
            if (spaceLeft)
            {

            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
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
                    visibleField[i, j].BackColor = Color.LightBlue;
                    visibleField[i, j].Image = null;
                }
            }
            ChooseAssets();
            PlaceSnake();
            PlaceFood();
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
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
                    actualField[headY, headX] = "Head0";
                    visibleField[headY, headX].Image = assets[0];
                    break;
                case 1:
                        if (IsFood())
                        {
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
                    actualField[headY, headX] = "Head1";
                    visibleField[headY, headX].Image = assets[1];
                    break;
                case 2:
                        if (IsFood())
                        {
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
                    actualField[headY, headX] = "Head2";
                    visibleField[headY, headX].Image = assets[2];
                    break;
                case 3:
                        if (IsFood())
                        {
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
                    actualField[headY, headX] = "Head3";
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
                    if (headX < 0 || actualField[headY, headX - 1] != null && actualField[headY, headX - 1] != "Food")
                    {
                        return true;
                    }
                    break;
                case 3:
                    if (headY < 0 || actualField[headY - 1, headX] != null && actualField[headY - 1, headX] != "Food")
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void howToPlayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
