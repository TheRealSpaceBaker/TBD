using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace TBD_Innovatie
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
        if (Exercise == null)
        {
            throw new InvalidOperationException("Exercise is not set.");
        }
        return Exercise;

        //Laat zien hoe de exercise uitgevoert wordt
        // ??
    }

    public void DoCMASExercise()
    {
        if (Exercise == null)
        {
            //Console.WriteLine("No exercise to perform.");
            return;
        }
        //Console.WriteLine($"Performing exercise: {Exercise.Description}. Please follow the instructions.");
        // Insert instructions here 
    }
}
}
