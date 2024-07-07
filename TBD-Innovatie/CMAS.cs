using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Innovatie
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

        public void AddScore(int exerciseId, int score)
        {
            var exercise = CMASExercises.FirstOrDefault(e => e.Id == exerciseId);

            if (exercise != null)
            {
                exercise.Score = score;
            }
            else
            {
                //Console.WriteLine("Exercise not found.");
            }
        }

        public static List<CMAS> ShowResults(Patient patient)
        {
            return new DAL().ReadCMASses(patient);
        }

    }
}
