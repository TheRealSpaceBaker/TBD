using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TBD_Console
{
    public class Patient : User
    {
        public List<CMAS> CMASsession { get; set; }
        public Doctor Doctors { get; set; }
        public Guardian Guardians { get; set; }

        public Patient(int id, string name, string username, string password, Doctor doctors, Guardian guardians)
            :base(id, name, username, password)
        {
            Doctors = doctors;
            Guardians = guardians;
        }

        public static List<Patient> ShowCMASes()
        {
            return new List<Patient>(); //Dal methode hier
        }
    }
}
