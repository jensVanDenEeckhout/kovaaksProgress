using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kovaaksProgressGUI
{
    // TODO: panda
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\Users\Jens\Desktop";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = theDialog.FileName;

                string[] filelines = File.ReadAllLines(filename);

                List<line> lineList = new List<line>();

                //parse line by line into instance of employee class
 
                for (int a = 0; a < filelines.Length; a++)
                {
                    line line = new line();
                    
                    line.LineText = filelines[a];
                    try
                    {

                        //  TITLE || DAYTRAINING || WHITESPACE
                        // DAYTRAINING
                        if (char.IsDigit(line.LineText.Substring(0, 1), 0))
                        {
                            // invalid record
                            if (line.LineText.Substring(8, 2) == "XX")
                            {
                                // XXXX XX XX
                                // 0123 56 89
                            }
                            else
                            {
                                // method # panda
                                int dateLength = 10;
                                char scoreSeperator = '/';
                                char recordNotation = 'R';
                                char highestScoreOfTheDaySeperator = '-';
                                line.Date = line.LineText.Substring(0, dateLength);  
                                // get first XXXX XX XX ( 4y + 1 + 2m + 1 + 2d = 10 )  [INFO: substring(A,B)  A = startindex,  B = length]

                                string filteringLine = line.LineText.Substring(10, line.LineText.Length - dateLength);


                                // CONVERT THIS CODE TO A METHOD # panda
                                if (filteringLine.Contains(scoreSeperator)){
                                    filteringLine = filteringLine.Replace(scoreSeperator, ' ');
                                }

                                if(filteringLine.Contains(recordNotation))
                                {
                                    line.NewRecord = true;
                                    filteringLine = filteringLine.Replace(recordNotation, ' ');
                                }
                                if(filteringLine.Contains(highestScoreOfTheDaySeperator))
                                {
                                    line.HighestScoreOfTheDay  = Convert.ToInt32( filteringLine.Split(highestScoreOfTheDaySeperator)[0]);
                                    line.Scores = filteringLine.Split(highestScoreOfTheDaySeperator)[1];

                                    var splitted = line.Scores.Split(new[] { " " },
                                          StringSplitOptions.RemoveEmptyEntries);

                                    line.ScoresList = splitted
                                        .Select(x => Convert.ToInt32(x) )
                                        .ToList();



                                }

                                
                            }
                        }
                        else if (char.IsLetter(line.LineText.Substring(0, 1), 0)) {
                            // title
                            line.Title = line.LineText;
                        }
                        else {
                            // whitespace
                        }
                        

                    }
                    catch (Exception exc)
                    {

                    }
                    // add date to date property

                    // add record (IF) to record property
                    // add scores to score array

                    lineList.Add(line);

                }
                //Test to see if it works
                foreach (line line in lineList)
                {
                    if ( !String.IsNullOrEmpty(line.Title) )
                    {
                        textBox1.Text += "Title: " + line.Title + Environment.NewLine;
                    }else if ( !String.IsNullOrEmpty(line.Date) )
                    {
                        textBox1.Text += "Date: " + line.Date + Environment.NewLine;
                        textBox1.Text += "scores: " + line.Scores + Environment.NewLine;
                    }

                   

                }


            }
        }
    }
}
