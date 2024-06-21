using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class CMASExercise
    {
        public int Id { get; set; }
        public Exercise? Exercise { get; set; }
        public int Score { get; set; }

        public CMASExercise(int id, Exercise? exercise, int score)
        {
            Id = id;
            Exercise = exercise;
            Score = score;
        }

        public Exercise ShowExercise()
        {
            return null; //Laat zien hoe de exercise uitgevoert wordt
        }

        public void DoCMASExercise()
        {

        }
    }
}
