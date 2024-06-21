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
        public List<CMAS> CMASSessions { get; set; }
        public Doctor? Doctor { get; set; }
        public Guardian? Guardian { get; set; }



        public Patient(int id, string name, string username, string password, List<CMAS> cmasSessions, Doctor? doctor, Guardian? guardian)
            :base(id, name, username, password)
        {
            CMASSessions = cmasSessions;
            Doctor = doctor;
            Guardian = guardian;
        }

        public static List<Patient> ShowCMASes()
        {
            return new List<Patient>(); //Dal methode hier
        }
    }
}
