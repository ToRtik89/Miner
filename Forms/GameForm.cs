using System.Collections.Generic;
using System.Drawing;
using System;
using System.Windows.Forms;
using System.IO;

namespace Miner
{
    public partial class GameForm : Form
    {
        int width = 0;
        int height = 0;
        int offset = 30;
        int bombPercent = 10;
        bool isFirstClick = true;
        FieldButton[,] field;
        int cellsOpened = 0;
        int bombs = 0;

        int score;

        string settings;


        public GameForm()
        {
            
            InitializeComponent();
            Timer timer1 = new Timer();
            timer1.Tick += Timer1_Tick;

            timer1.Interval = 1000;
            timer1.Enabled = true;

        }

        private void Timer1_Tick(object sender, EventArgs e) => score++;

        private void Form1_Load_1(object sender, EventArgs e)
        {
            LoadSettings();
            SetupSettings();

            field = new FieldButton[width, height];
            GenerateField();
        }

        private void LoadSettings()
        {
            try
            {
                using (var sr = new StreamReader("settings.txt"))
                {
                    settings = sr.ReadToEnd();
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private void SetupSettings()
        {
            if (settings == "")
            {
                settings = "easy\r\n";
            }

            if (settings == "easy\r\n")
            {
                width = height = 9;
                Size = new Size(290, 310);

            }

            if (settings == "normal\r\n")
            {
                width = height = 16;
                Size = new Size(500, 520);
            }

            if (settings == "hard\r\n")
            {
                width = 30;
                height = 16;
                Size = new Size(920, 520);
            }
        }


        public void GenerateField()
        {
            Random random = new Random();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    FieldButton newButton = new FieldButton();
                    newButton.Location = new Point(x * offset, y * offset);
                    newButton.Size = new Size(offset, offset);
                    newButton.isClickable = true;
                    if (random.Next(0, 100) <= bombPercent)
                    {
                        newButton.isBomb = true;
                        bombs++;
                    }
                    newButton.xCoord = x;
                    newButton.yCoord = y;
                    Controls.Add(newButton);
                    newButton.MouseUp += new MouseEventHandler(FieldButtonClick);   // поменяли на MouseUp
                    field[x, y] = newButton;
                }
            }
        }

        private void FieldButtonClick(object sender, MouseEventArgs e)
        {

            FieldButton clickedButton = (FieldButton)sender;
            if (e.Button == MouseButtons.Left && clickedButton.isClickable)  // новое
            {
                if (!clickedButton.isBomb) { EmptyFieldButtonClick(clickedButton); return; }

                if (!isFirstClick) { Explode(); return; }

                clickedButton.isBomb = false;
                isFirstClick = false;
                bombs--;
                OpenRegion(clickedButton.xCoord, clickedButton.yCoord, clickedButton);
                    

                isFirstClick = false;
            }


            if (e.Button == MouseButtons.Right)
            {
                clickedButton.isClickable = !clickedButton.isClickable;

                if (!clickedButton.isClickable)
                    clickedButton.Text = "B";

                else
                    clickedButton.Text = "";
            }
            CheckWin();
        }

        private void Explode()
        {
            // Можно дописать (в самом конце) раскрытие бомб начиная от нажатой по очереди. (а не всех сразу)
            foreach (FieldButton button in field)
            {
                if (button.isBomb)
                {
                    button.Text = "💣";
                }
            }

            MessageBox.Show("Вы проиграли!");

            GameOver();

        }
        private void EmptyFieldButtonClick(FieldButton clickedButton)
        {

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (field[x, y] == clickedButton)
                    {
                        //bombsAround = CountBombsAround(x, y);
                        OpenRegion(x, y, clickedButton);
                    }
                }
            }
        }

        private void OpenRegion(int xCoord, int yCoord, FieldButton clickedButton)
        {
            Queue<FieldButton> queue = new Queue<FieldButton>();
            queue.Enqueue(clickedButton);
            clickedButton.wasAdded = true;//
            while (queue.Count > 0)
            {
                FieldButton currentCell = queue.Dequeue();
                OpenCell(currentCell.xCoord, currentCell.yCoord, currentCell);
                cellsOpened++;
                if (CountBombsAround(currentCell.xCoord, currentCell.yCoord) == 0)
                {
                    for (int y = currentCell.yCoord - 1; y <= currentCell.yCoord + 1; y++)
                    {
                        for (int x = currentCell.xCoord - 1; x <= currentCell.xCoord + 1; x++)
                        {
                            if (x == currentCell.xCoord && y == currentCell.yCoord)//
                            {
                                continue;
                            }
                            if (x >= 0 && x < width && y < height && y >= 0)
                            {
                                if (!field[x, y].wasAdded)
                                {
                                    queue.Enqueue(field[x, y]);
                                    field[x, y].wasAdded = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OpenCell(int x, int y, FieldButton clickedButton)
        {
            int bombsAround = CountBombsAround(x, y);
            if (bombsAround == 0) { }

            else
                clickedButton.Text = "" + bombsAround;

            clickedButton.Enabled = false;
        }

        private int CountBombsAround(int xCoord, int yCoord)
        {
            int bombsAround = 0;
            for (int x = xCoord - 1; x <= xCoord + 1; x++)
            {
                for (int y = yCoord - 1; y <= yCoord + 1; y++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (field[x, y].isBomb == true)
                        {
                            bombsAround++;
                        }
                    }
                }
            }
            return bombsAround;
        }

        private void CheckWin()
        {
            int cells = width * height;
            int emptyCells = cells - bombs;
            if (cellsOpened >= emptyCells)
            {

                MessageBox.Show("Ты выйграл!");
                GameOver();
            }
        }

        private void GameOver()
        {
            Hide();
            FormClass.InputUserName = new InputUserNameForm();
            FormClass.InputUserName.Score = score;
            FormClass.InputUserName.Show();
        }

        private void Game_FormClosed(object sender, FormClosedEventArgs e) => GameOver();
    }



    public class FieldButton : Button
    {
        public bool isBomb;
        public bool isClickable;
        public bool wasAdded;
        public int xCoord;
        public int yCoord;
    }


}

