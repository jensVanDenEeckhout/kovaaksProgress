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
        private string _GameMode = "";
        private List<Line> lineList = new List<Line>();

        private const int dateLength = 10;
        private const char scoreSeperator = '/';
        private const char recordNotation = 'R';
        private const char highestScoreOfTheDaySeperator = '-';

        DataTable dt = new DataTable();
        DataTable dtHighest = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        public void SetGameModeName(Line line)
        {
            if (char.IsLetter(line.LineText.Substring(0, 1), 0))
            {
                // title
                _GameMode = line.LineText;
                //line.Title = _GameMode;
            }
        }
        private void CreateScoreObject(Line line)
        {
            //  TITLE || DAYTRAINING || WHITESPACE
            // DAYTRAINING
            if (char.IsDigit(line.LineText.Substring(0, 1), 0))
            {
                line.GameMode = _GameMode;

                if (line.LineText.Substring(8, 2) == "XX")
                {
                    // XXXX XX XX
                    // 0123 56 89
                }
                else
                {
                    line.Date = line.LineText.Substring(0, dateLength); // get first XXXX XX XX ( 4y + 1 + 2m + 1 + 2d = 10 )  [INFO: substring(A,B)  A = startindex,  B = length]
                    string filteringLine = line.LineText.Substring(10, line.LineText.Length - dateLength);

                    filterScoreRow(filteringLine, line);


                }
                lineList.Add(line);
            }
        }

        private void filterScoreRow(string filteringLine, Line line)
        {
            // CONVERT THIS CODE TO A METHOD # panda
            if (filteringLine.Contains(scoreSeperator))
            {
                filteringLine = filteringLine.Replace(scoreSeperator, ' ');
            }

            if (filteringLine.Contains(recordNotation))
            {
                line.NewRecord = true;
                filteringLine = filteringLine.Replace(recordNotation, ' ');
            }
            line.Scores = filteringLine;

            var splitted = line.Scores.Split(new[] { " " },
                      StringSplitOptions.RemoveEmptyEntries);

            line.ScoresList = splitted
                .Select(x => Convert.ToInt32(x))
                .ToList();
            /*
            if (filteringLine.Contains(highestScoreOfTheDaySeperator))
            {
                line.HighestScoreOfTheDay = Convert.ToInt32(filteringLine.Split(highestScoreOfTheDaySeperator)[0]);
                line.Scores = filteringLine.Split(highestScoreOfTheDaySeperator)[1];

                var splitted = line.Scores.Split(new[] { " " },
                      StringSplitOptions.RemoveEmptyEntries);

                line.ScoresList = splitted
                    .Select(x => Convert.ToInt32(x))
                    .ToList();
            }
            */

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

                

                //parse line by line into instance of employee class
 
                for (int a = 0; a < filelines.Length; a++)
                {
                    Line line = new Line();
                    
                    line.LineText = filelines[a];
                    try
                    {

                        SetGameModeName(line);


                        CreateScoreObject(line);
                    }
                    catch (Exception exc)
                    {

                    }



                }

                CreateDataTables();


                //Test to see if it works
                foreach (Line line in lineList)
                {
                    /*
                    if ( !String.IsNullOrEmpty(line.Title) )
                    {
                        textBox1.Text += "Title: " + line.Title + Environment.NewLine;


                    }else 
                    */
                    if ( !String.IsNullOrEmpty(line.Date) )
                    {
                        // CHECK IF DATE IS VALID OTHERWISE SKIP



                        if (line.GameMode == "jumbo tile frenzy (shortened)")
                        {
                            textBox1.Text += "Date: " + line.Date + Environment.NewLine;
                            textBox1.Text += "scores: " + line.Scores + Environment.NewLine;

                            line.calculateAverage();
                            textBox1.Text += "average: " + line.AverageScoreOfTheDay + Environment.NewLine;

                            line.calculateLowest();
                            textBox1.Text += "lowest: " + line.LowestScoreOfTheDay + Environment.NewLine;

                            line.calculateHighest();
                            textBox1.Text += "highest: " + line.HighestScoreOfTheDay + Environment.NewLine;

                            dt.Rows.Add(line.Date, line.LowestScoreOfTheDay);
                            dtHighest.Rows.Add(line.Date, line.HighestScoreOfTheDay);

                        }
                    }
                }
                dataGridView1.DataSource = dt;
                dataGridView2.DataSource = dtHighest;

            }
        }

        private void CreateDataTables()
        {


            dt.Columns.Add("date");
            dt.Columns.Add("lowest score");

            dtHighest.Columns.Add("date");
            dtHighest.Columns.Add("highest score");
        }
    }
}
