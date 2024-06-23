using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace TBD_Console.DAL
{
    public class DAL
    {
        private string connectionString = "Data Source=casus-tbd.database.windows.net;Initial Catalog=\"TBD Database\";Persist Security Info=True;User ID=TBDAdmin;Password:GoedGejat2024;Encrypt=True";

        // Classes nog niet aangemaakt, dus vele errors mbt het "Niet bestaan" van de classes.

        // Methods CMASExercise
        public List<CMASExercise> ReadCMASExercises()
        {
            List<CMASExercise> cmasExercises = new List<CMASExercise>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    // ChatGPT: Pas de query aan om alle records op te halen zonder parameters
                    command.CommandText = @"
                        SELECT 
                            CMASExercise.Id, 
                            Exercise.Exercise AS Exercise, 
                            CMASExercise.Score
                        FROM CMASExercise
                        JOIN Exercise ON CMASExercise.Exercise = Exercise.Id";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Lees het id (index 0)
                            int exerciseId = reader.GetInt32(1); // Lees het ExerciseId (index 1)
                            string exerciseDecription = reader.GetString(2); // Lees het ExerciseDecription (index 2)
                            int score = reader.GetInt32(3); // Lees het Score (index 2)

                            Exercise exercise = new Exercise(exerciseId, exerciseDecription);
                            cmasExercises.Add(new CMASExercise(id, exercise, score));
                        }
                    }

                    if (cmasExercises.Count > 0)
                    {
                        Console.WriteLine("Gegevens zijn gevonden in de database (SSMS).");
                    }
                    else
                    {
                        Console.WriteLine("Geen overeenkomende gegevens gevonden in de database (SSMS).");
                    }
                }
            }

            return cmasExercises;
        }

        // Methods CMAS
        public List<CMAS> ReadCMAS()
        {
            List<Patient> allPatients = ReadPatients();
            List<CMASExercise> CMASExercises = ReadCMASExercises();

            List<CMAS> cmasList = new List<CMAS>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            SELECT CMAS.Id, CMAS.PatientId
            FROM CMAS
            JOIN Patient ON CMAS.PatientId = Patient.Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int cmasId = reader.GetInt32(0);
                            int patientId = reader.GetInt32(1); // Lees het patientId (index 1)
                            Patient? patient = allPatients.FirstOrDefault(p => p.Id == patientId);

                            CMAS cmas = new CMAS(cmasId,CMASExercises, patient);
                            cmasList.Add(cmas);
                        }
                    }
                }
            }

            return cmasList;
        }

        // Methods Exercise
        public List<Exercise> ReadExercises()
        {
            List<Exercise> exercises = new List<Exercise>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    // ChatGPT: Pas de query aan om alle records op te halen zonder parameters
                    command.CommandText = @"
                        SELECT Id, Description
                        FROM Exercise";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Lees het id (index 0)
                            string decription = reader.GetString(1); // Lees het ExerciseDecription (index 2)

                            Exercise exercise = new Exercise(id, decription);
                            exercises.Add(exercise);
                        }
                    }

                    if (exercises.Count > 0)
                    {
                        Console.WriteLine("Gegevens zijn gevonden in de database (SSMS).");
                    }
                    else
                    {
                        Console.WriteLine("Geen overeenkomende gegevens gevonden in de database (SSMS).");
                    }
                }
            }

            return exercises;
        }


        // Methods Appointments 
        public List<Appointment> ReadAppointments()
        {
            List<Doctor> allDoctors = ReadDoctors();
            List<Patient> allPatients = ReadPatients();


            List<Appointment> appointments = new List<Appointment>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    // ChatGPT: Pas de query aan om alle records op te halen zonder parameters
                    command.CommandText = @"
                        SELECT 
                            Appointment.Id, 
                            Appointment.PatientId, 
                            Appointment.DoctorId, 
                            Appointment.Date, 
                            Appointment.CMASId, 
                            Appointment.Description
                        FROM Appointment";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Lees het id (index 0)
                            int patientId = reader.GetInt32(1); // Lees het patientId (index 1)
                            int doctorId = reader.GetInt32(2); // Lees het doctorId (index 2)
                            DateTime date = reader.GetDateTime(3); // Lees het date (index 3)
                            int cmasId = reader.GetInt32(4); // Lees het cmasId (index 4)
                            string description = reader.GetString(5); // Lees het description (index 5)

                            Doctor doctor = allDoctors.Find(d => d.Id == doctorId);
                            Patient patient = allPatients.Find(p => p.Id == patientId);
                            CMAS cmas = ReadCMAS().Find(c => c.Id == cmasId);

                            appointments.Add(new Appointment(id, patient, doctor, date, cmas, description));
                        }
                        return appointments;
                    }

                }
            }
        }
        public void CreateAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Insert Into Appointment (PatientId, DoctorId, Date, CMASId, Description) Values ({appointment.Patient.Id}, {appointment.Doctor.Id}, '{appointment.Date}', {appointment.CMAS.Id}, '{appointment.Description}')";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                // SQL query to update an appointment. Assumes all fields except ID can be updated.
                string query = @"
            UPDATE Appointment
            SET 
                PatientId = @PatientId, 
                DoctorId = @DoctorId, 
                Date = @Date, 
                CMASId = @CMASId, 
                Description = @Description
            WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to prevent SQL injection
                    command.Parameters.AddWithValue("@PatientId", appointment.Patient.Id);
                    command.Parameters.AddWithValue("@DoctorId", appointment.Doctor.Id);
                    command.Parameters.AddWithValue("@Date", appointment.Date);
                    command.Parameters.AddWithValue("@CMASId", appointment.CMAS.Id);
                    command.Parameters.AddWithValue("@Description", appointment.Description);
                    command.Parameters.AddWithValue("@Id", appointment.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Appointment successfully updated in the database.");
                    }
                    else
                    {
                        Console.WriteLine("No rows were affected. The appointment was not updated in the database.");
                    }
                }
            }
        }
        public void DeleteAppointment(Appointment appointment)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Delete From Appointment Where Id = {appointment.Id}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }           
        }

        // Methods User
        public List<User> ReadUsers()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    // ChatGPT: Pas de query aan om alle records op te halen zonder parameters
                    command.CommandText = @"
                        SELECT *
                        FROM User";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Lees het id (index 0)
                            string name = reader.GetString(1); // Lees het name (index 1)
                            string username = reader.GetString(2); // Lees het username (index 3)
                            string password = reader.GetString(3); // Lees het password (index 4)
                        }
                    }

                    if (users.Count > 0)
                    {
                        Console.WriteLine("Gegevens zijn gevonden in de database (SSMS).");
                    }
                    else
                    {
                        Console.WriteLine("Geen overeenkomende gegevens gevonden in de database (SSMS).");
                    }
                }
            }

            return users;
        }
        public void CreateUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
                    INSERT INTO User 
                        (Id, Name, Username, Password) 
                    VALUES 
                        (@UserId, @Name, @Username, @Password)";
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("User successfully added to the database.");
                    }
                    else
                    {
                        Console.WriteLine("No rows were affected. The user was not added to the database.");
                    }
                }
            }
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
                string query = "select * from Patient Inner Join User on Patient.UserId=User.UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new Patient(
                                Int32.Parse(reader["UserID"].ToString()),
                                reader["Name"].ToString(),
                                reader["Username"].ToString(),
                                reader["Password"].ToString(),
                                new List<CMAS>(),
                                null,
                                null
                                ));
                        }
                    }
                }
                connection.Close();
                return patients;
            }
        }
        public int CreatePatient(Patient patient)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Insert Into User (Name, Username, Password) Values ('{patient.Name}', '{patient.Username}', '{patient.Password}');Select SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(query, connection);
                int id = Convert.ToInt32(command.ExecuteScalar());
                query = $"Insert Into Patient (DoctorId, GuardianId, UserId) Values ({patient.Doctor.Id}, {patient.Guardian.Id}, {id})";
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return id;
            }
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
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "select * from Patient Inner Join User on Patient.UserId=User.UserId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor(
                                Int32.Parse(reader["UserId"].ToString()),
                                reader["Name"].ToString(),
                                reader["Username"].ToString(),
                                reader["Password"].ToString()
                                ));
                        }
                    }
                }
                connection.Close();
                return doctors;
            }
        }
        public int CreateDoctor(Doctor doctor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Insert Into User (Name, Username, Password) Values ('{doctor.Name}', '{doctor.Username}', '{doctor.Password}');Select SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(query, connection);
                int id = Convert.ToInt32(command.ExecuteScalar());
                query = $"Insert Into Doctor (DoctorId) Values ({id})";
                command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
                return id;
            }
        }
        public Doctor UpdateDoctor(Doctor doctor)
        {
            return null;
        }
        public void DeleteDoctor(Doctor doctor)
        {
            return;
        }

        //Sjablonen voor de Dal methods
        public int CreateUpdateDeleteClass()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Insert Into tablename (attributes) Values (values);Select SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(query, connection);
                int id = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return id;
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
