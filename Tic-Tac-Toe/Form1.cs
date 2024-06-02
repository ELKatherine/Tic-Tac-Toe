using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        private int playerXScore = 0;
        private int playerOScore = 0;
        private bool turn = true; 
        private int turnCount = 0;

        private Label lblPlayerX;
        private Label lblPlayerO;
        private Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Name = $"Button{i * 3 + j}";
                    btn.Size = new System.Drawing.Size(100, 100);
                    btn.Location = new System.Drawing.Point(100 * j, 100 * i);
                    btn.Click += new EventHandler(button_Click);
                    btn.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
                    this.Controls.Add(btn);
                }
            }

            lblPlayerX = new Label();
            lblPlayerX.Text = $"Player X: {playerXScore}";
            lblPlayerX.Location = new System.Drawing.Point(350, 20);
            lblPlayerX.Font = new System.Drawing.Font("Arial", 14);
            lblPlayerX.Name = "lblPlayerX";
            this.Controls.Add(lblPlayerX);

            lblPlayerO = new Label();
            lblPlayerO.Text = $"Player O: {playerOScore}";
            lblPlayerO.Location = new System.Drawing.Point(350, 50);
            lblPlayerO.Font = new System.Drawing.Font("Arial", 14);
            lblPlayerO.Name = "lblPlayerO";
            this.Controls.Add(lblPlayerO);

            Button btnNewGame = new Button();
            btnNewGame.Text = "New Game";
            btnNewGame.Location = new System.Drawing.Point(350, 100);
            btnNewGame.Click += new EventHandler(newGame_Click);
            this.Controls.Add(btnNewGame);

            Button btnReset = new Button();
            btnReset.Text = "Reset";
            btnReset.Location = new System.Drawing.Point(350, 150);
            btnReset.Click += new EventHandler(reset_Click);
            this.Controls.Add(btnReset);

            Button btnExit = new Button();
            btnExit.Text = "Exit";
            btnExit.Location = new System.Drawing.Point(350, 200);
            btnExit.Click += new EventHandler(exit_Click);
            this.Controls.Add(btnExit);
        }

        private void button_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Text == "")
            {
                btn.Text = "X";
                turn = false; 
                turnCount++;
                checkForWinner();
                ComputerMove(); 
            }
        }

        private void ComputerMove()
        {
            List<Button> emptyCells = new List<Button>();

            foreach (Control c in Controls)
            {
                if (c is Button btn && btn.Name.StartsWith("Button") && btn.Text == "")
                {
                    emptyCells.Add(btn);
                }
            }

            if (emptyCells.Count > 0)
            {
                int index = random.Next(emptyCells.Count);
                Button selectedCell = emptyCells[index];
                selectedCell.Text = "O"; 
                turn = true; 
                turnCount++;
                checkForWinner();
            }
        }

        private void checkForWinner()
        {
            bool thereIsAWinner = false;

            for (int i = 0; i < 3; i++)
            {
                if ((Controls[$"Button{i * 3}"] as Button).Text == (Controls[$"Button{i * 3 + 1}"] as Button).Text &&
                    (Controls[$"Button{i * 3 + 1}"] as Button).Text == (Controls[$"Button{i * 3 + 2}"] as Button).Text &&
                    (Controls[$"Button{i * 3}"] as Button).Text != "")
                {
                    thereIsAWinner = true;
                }

                if ((Controls[$"Button{i}"] as Button).Text == (Controls[$"Button{i + 3}"] as Button).Text &&
                    (Controls[$"Button{i + 3}"] as Button).Text == (Controls[$"Button{i + 6}"] as Button).Text &&
                    (Controls[$"Button{i}"] as Button).Text != "")
                {
                    thereIsAWinner = true;
                }
            }

            if ((Controls["Button0"] as Button).Text == (Controls["Button4"] as Button).Text &&
                (Controls["Button4"] as Button).Text == (Controls["Button8"] as Button).Text &&
                (Controls["Button0"] as Button).Text != "")
            {
                thereIsAWinner = true;
            }

            if ((Controls["Button2"] as Button).Text == (Controls["Button4"] as Button).Text &&
                (Controls["Button4"] as Button).Text == (Controls["Button6"] as Button).Text &&
                (Controls["Button2"] as Button).Text != "")
            {
                thereIsAWinner = true;
            }

            if (thereIsAWinner)
            {
                disableButtons();
                string winner = turn ? "O" : "X"; 
                MessageBox.Show($"{winner} Wins!", "Yay!");
                updateScore(winner);
            }
            else if (turnCount == 9)
            {
                MessageBox.Show("Draw!", "It's a draw!");
                newGame_Click(null, null); 
            }
        }

        private void disableButtons()
        {
            foreach (Control c in Controls)
            {
                if (c is Button btn && btn.Name.StartsWith("Button"))
                {
                    btn.Enabled = false;
                }
            }
        }

        private void updateScore(string winner)
        {
            if (winner == "X")
                playerXScore++;
            else if (winner == "O")
                playerOScore++;

            lblPlayerX.Text = $"Player X: {playerXScore}";
            lblPlayerO.Text = $"Player O: {playerOScore}";
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            turn = true;
            turnCount = 0;

            foreach (Control c in Controls)
            {
                if (c is Button btn && btn.Name.StartsWith("Button"))
                {
                    btn.Enabled = true;
                    btn.Text = "";
                }
            }
        }

        private void reset_Click(object sender, EventArgs e)
        {
            playerXScore = 0;
            playerOScore = 0;
            lblPlayerX.Text = "Player X: 0";
            lblPlayerO.Text = "Player O: 0";
            newGame_Click(sender, e);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
