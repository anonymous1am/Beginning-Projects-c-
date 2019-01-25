using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Hangman
{
    public partial class Hangman: Form
    {
        private Bitmap [] hangImages = {Properties.Resources.Hangman_Start,
            Properties.Resources.Hangman_1 , Properties.Resources.Hangman_2 ,
            Properties.Resources.Hangman_3, Properties.Resources.Hangman_4,
            Properties.Resources.Hangman_5, Properties.Resources.Hangman_6};

        private int wrongGuesses = 0;

        private string current = "";

        private string copyCurrent = "";

        private string[] words;

        public Hangman()
        {
            InitializeComponent();
            loadwords();
            setUpWordChoice();
        }

        private void loadwords()
        {
            char delimiterChars = ',';
            string[] readText = File.ReadAllLines("words.csv");
            words = new string[readText.Length];
            
            int index = 0;
           foreach (string s in readText)
            {
                
                string[] line = s.Split(delimiterChars);
                s.Split(delimiterChars);
                words[index++] = line[0];
            }
            
        }

        private void setUpWordChoice()
        {
            wrongGuesses = 0;
            hangImage.Image = hangImages[wrongGuesses];
            int guessIndex = new Random().Next(0, words.Length);
            current = words[guessIndex];

            copyCurrent = "";
            for (int i = 0; i < current.Length; i++)
            {
                copyCurrent += "_";
            }
            displayCopy();
        }

        private void displayCopy()
        {
            lblShowWord.Text = "";
            for (int i = 0; i < copyCurrent.Length; i++)
            {
                lblShowWord.Text += copyCurrent.Substring(i, 1);
                lblShowWord.Text += " ";
            }
        }

        private void updateCopy(char guess)
        {

        }

        private void guessClick(object sender, EventArgs e)
        {
            Button choice = sender as Button;
            if (choice != null)
            {
                choice.Enabled = false;
                if (current.Contains(choice.Text))
                {
                    char[] temp = copyCurrent.ToCharArray();
                    char[] find = current.ToCharArray();
                    char guessChar = choice.Text.ElementAt(0);

                    for (int i = 0; i < find.Length; i++)
                    {
                        if (find[i] == guessChar)
                        {
                            temp[i] = guessChar;
                        }

                    }
                    copyCurrent = new string(temp);
                    displayCopy();

                    if (copyCurrent.Equals(current))
                    {
                        lblShowWord.Text = "You win!";
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            this.Controls[i].Enabled = false;
                        }

                        button27.Enabled = true;
                    }

                } 
                else
                {
                    if(wrongGuesses < (hangImages.Length -  2))
                    {
                        wrongGuesses++;
                        hangImage.Image = hangImages[wrongGuesses];
                    }
                    else
                    {
                        wrongGuesses++;
                        hangImage.Image = hangImages[wrongGuesses];
                        lblShowWord.Text = "The word was : " + current;
                        for (int i = 0; i < this.Controls.Count; i++)
                        {
                            this.Controls[i].Enabled = false;
                        }

                        button27.Enabled = true;

                    }                    
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            setUpWordChoice();
            lblShowWord.Text = "";

            for (int i=0; i < this.Controls.Count; i++)
            {
                this.Controls[i].Enabled = true;
            }
            displayCopy();
        }
    }
}
