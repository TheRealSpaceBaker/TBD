﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Innovatie
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public Exercise(int id, string description)
        {
            Id = id;
            Description = description;
        }

        public static List<Exercise> ShowExercise()
        {
            return new DAL().ReadExercises();
        }

        public void DoExercise()
        {
            // ook womp womp (Er wordt aan gewerkt)
        }
    }
}
