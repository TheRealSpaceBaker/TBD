using TBD_Console.Data_Access;

namespace TBD_Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // Create a new instance of the DAL class
            DAL dal = new DAL();

            // Read all appointments from the database
            List<Appointment> appointments = dal.ReadAppointments();
            Console.WriteLine(
                "Appointments: " + 
                string.Join(", ", appointments.Select(a => a.Description))
            );


        }
    }
}
