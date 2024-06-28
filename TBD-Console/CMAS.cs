using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBD_Console.Data_Access;

namespace TBD_Console
{
    public class CMAS
    {
        public int Id { get; set; }
        public List<CMASExercise> CMASExercises { get; set; }
        public Patient Patient { get; set; }

        public CMAS(int id, List<CMASExercise> cmasExercises, Patient? patient)
        {
            Id = id;
            CMASExercises = cmasExercises;
            Patient = patient;
        }

        public void AddScore()
        {
            

        }

        public static List<CMAS> ShowResults(Patient patient)
        {
            // return null; Dal methode aanroepen
            return new DAL().ReadCMASses(patient);
        }

    }
}
