using TBD_Console.DataAccessLayer;

namespace TBD_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DAL dal = new DAL();
            List<Appointment> appointments = dal.ReadAppointments();
            foreach(Appointment appointment in appointments)
            {
                Console.WriteLine(appointment.Id + " " + appointment.Date + " " + appointment.Description + " " + appointment.PatientId + " " + appointment.DoctorId);
            }
        }
    }
}
