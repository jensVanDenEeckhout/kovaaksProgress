using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace kovaaksProgressGUI
{
    public class Line
    {
        public Line(string date, string scores, char record = ' ')
        {

        }
        public Line(string lineText)
        {
            LineText = lineText;
        }
        public Line()
        {
           
        }

        public string GameMode { get; set; }

        public string LineText { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        public string Scores { get; set; }
 
        public Boolean NewRecord { get; set; }

        public int HighestScoreOfTheDay { get; set; }

        public int LowestScoreOfTheDay { get; set; }

        public int AverageScoreOfTheDay { get; set; }

        public List<int> ScoresList { get; set; }

        public int AmountOfGames { 
            get{
                return ScoresList.Count();
            } 
        }



        public void calculateAverage()
        {
            try
            {
                if (ScoresList.Any())
                {
                    int summationOfScores = ScoresList.Sum(); ;
                    AverageScoreOfTheDay = summationOfScores / ScoresList.Count;

                }
            }catch(Exception exc)
            {

            }
        }

        public void calculateLowest()
        {
            try
            {
                if (ScoresList.Any())
                {
                    LowestScoreOfTheDay = ScoresList.Min();
                }
            }
            catch (Exception exc)
            {

            }
            

        }
        public void calculateHighest()
        {
            try
            {
                if (ScoresList.Any())
                {
                    HighestScoreOfTheDay = ScoresList.Max();
                }
            }
            catch (Exception exc)
            {

            }
        }

    }
}
