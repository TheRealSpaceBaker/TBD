using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console.DataAccessLayer
{
    public class DAL
    {
        private string connectionString = "Data Source=casus-tbd.database.windows.net;Initial Catalog=\"TBD Database\";Persist Security Info=True;User ID=TBDAdmin;Password=GoedGejat2024;Encrypt=True";


        // Methods CMASExercise
        public List<CMASExercise> ReadCMASExercises()
        {
            return null;
        }

        // Methods CMAS

        // Methods Exercise
        public List<Exercise> ReadExercises()
        {
            return null;
        }


        // Methods Appointments K 
        public List<Appointment> ReadAppointments()
        {
            /*
            List<Appointment> appointments = new List<Appointment>();
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Appointments";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                           appointments.Add(new Appointment(reader.GetInt32(0), reader.GetDateTime(1), reader.GetString(2), new Patient(reader.GetInt32(3)), reader.GetInt32(4)));
                        }
                    }
                }
            }
            return appointments;
            */
            return null;
        }

        public void CreateAppointment(Appointment appointment)
        {           
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Appointments (Date, Description, PatientID, DoctorID) VALUES (@Date, @Description, @PatientID, @DoctorID)";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", appointment.Date);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@PatientID", appointment.PatientId.Id);
                    command.Parameters.AddWithValue("@DoctorID", appointment.DoctorId.Id);
                    command.ExecuteNonQuery();
                }
            } 
            
        }
        public void UpdateAppointment(Appointment appointment)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Appointments SET Date = @Date, Description = @Description, PatientID = @PatientID, DoctorID = @DoctorID WHERE ID = @AppointmentId";
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", appointment.Date);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@PatientID", appointment.PatientId.Id);
                    command.Parameters.AddWithValue("@DoctorID", appointment.DoctorId.Id);
                    command.Parameters.AddWithValue("@ID", appointment.Id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeleteAppointment(Appointment appointment)
        {
            return;
        }

        // Methods User
        public List<User> ReadUsers()
        {
            return null;
        }
        public void CreateUser(User user)
        {
            return;
        }
        public void UpdateUser(User user)
        {
            return;
        }
        public void DeleteUser(User user)
        {
            return;
        }


        // Methods Guardian K
        public List<Guardian> ReadGuardians()
        {
            return null;
        }
        public void CreateGuardian(Guardian guardian)
        {
            return;
        }
        public void UpdateGuardian(Guardian guardian)
        {
            return;
        }
        public void DeleteGuardian(Guardian guardian)
        {
            return;
        }


        // Methods Patient
        public List<Patient> ReadPatients()
        {
            return null;
        }
        public void CreatePatient(Patient patient)
        {
            return;
        }
        public void UpdatePatient(Patient patient)
        {
            return;
        }
        public void DeletePatient(Patient patient)
        {
            return;
        }

        // Methods Doctor
        public List<Doctor> ReadDoctors()
        {
            return null;
        }
        public void CreateDoctor(Doctor doctor)
        {
            return;
        }
        public void UpdateDoctor(Doctor doctor)
        {
            return;
        }
        public void DeleteDoctor(Doctor doctor)
        {
            return;
        }

    }
}
