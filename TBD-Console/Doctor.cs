using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Doctor : User
    {
        public List<Appointment> Appointments { get; set; }
        public List<Patient> Patients { get; set; }

        public Doctor(int id, string name, string username, string password)
            : base(id, name, username, password)
        {

        }

        public static List<Appointment> ShowAppointments()
        {
            return null; //Dal methode hier
        }


    }

}
