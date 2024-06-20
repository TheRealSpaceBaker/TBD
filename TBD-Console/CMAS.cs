using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class CMAS
    {
        public int Id { get; set; }
        public List<CMASExercise> CMASExercises { get; set; }

        public Patient Patient { get; set; }

        public CMAS(int id, Patient patient)
        {
            Id = id;
            Patient = patient;
        }

        public void AddScore()
        {
            
        }

        public static List<CMAS> ShowResults()
        {
            return null; //Dal methode aanroepen
        }

    }
}
