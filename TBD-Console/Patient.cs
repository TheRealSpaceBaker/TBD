using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Patient
    {
        public CMAS CMASsession { get; set; }
        public Doctor Doctors { get; set; }
        public Guardian Guardians { get; set; }

        public Patient(CMAS cmasSession, Doctor doctors, Guardian guardians)
        {
            CMASsession = cmasSession;
            Doctors = doctors;
            Guardians = guardians;
        }

        public void addCMAS(CMAS cmasSession)
        {
            CMASsession = cmasSession;
        }

        public static List<Patient> ShowCMASes()
        {
            return new List<Patient>(); //Dal methode hier
        }
    }
}
