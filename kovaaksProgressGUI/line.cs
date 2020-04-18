using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kovaaksProgressGUI
{
    public class line
    {
        public line(string date, string scores, char record = ' ')
        {

        }
        public line(string lineText)
        {
            LineText = lineText;
        }
        public line()
        {
           
        }

        public string LineText { get; set; }

        public string Date { get; set; }

        public string Title { get; set; }

        public string Scores { get; set; }
 
        public Boolean NewRecord { get; set; }

        public int HighestScoreOfTheDay { get; set; }

        public int LowestScoreOfTheDay { get; set; }

        public int AverageScoreOfTheDay { get; set; }

        public List<int> ScoresList { get; set; }

    }
}
