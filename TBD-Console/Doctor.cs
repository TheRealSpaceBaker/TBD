using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBD_Console.Data_Access;

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

        public List<Appointment> ShowAppointments()
        {
            return new DAL().ReadAppointments(this);
        }
        public static List<Doctor> ReadDoctors()
        {
            return new DAL().ReadDoctors();
        }

    }

}
