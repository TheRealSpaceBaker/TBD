using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console.DAL
{
    public class DAL
    {
        private string connectionString = "Data Source=casus-tbd.database.windows.net;Initial Catalog=\"TBD Database\";Persist Security Info=True;User ID=TBDAdmin;Password:GoedGejat2024;Encrypt=True";

        // Classes nog niet aangemaakt, dus vele errors mbt het "Niet bestaan" van de classes.

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
            return null;
        }
        public void CreateAppointment(Appointment appointment)
        {
            return;
        }
        public void UpdateAppointment(Appointment appointment)
        {
            return;
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
            List<Patient> patients = new List<Patient>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Patient";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new Patient(reader["id"]));
                        }
                    }
                }
                connection.Close();
                return patients;
            }
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

        //Sjablonen voor de Dal methods
        public void CreateUpdateDeleteClass()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"sqlquery here";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<int> ReadClass()
        {
            List<int> ListFilledWithObjects = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from <TableName>";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Constructor Here
                        }
                    }
                }
                connection.Close();
                return ListFilledWithObjects;
            }
        }
    }
}
