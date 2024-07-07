using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Innovatie
{
    public class DAL
    {
        private string connectionString = "Data Source=casus-tbd.database.windows.net;Initial Catalog=\"TBD Database\";Persist Security Info=True;User ID=TBDAdmin;Password:GoedGejat2024;Encrypt=True";

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
                            CMASExercise.CMASExerciseId, 
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
                        Console.WriteLine("Data was found in the database (SSMS)");
                    }
                    else
                    {
                        Console.WriteLine("No matching data found in the database (SSMS).");
                    }
                }
            }

            return cmasExercises;
        }

        // Methods CMAS
        public List<CMAS> ReadCMASses(Patient patient)
        {
            List<CMAS> CMASses = new List<CMAS>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"select * from CMAS Where PatientId = {patient.Id}";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CMASses.Add(new CMAS(Int32.Parse(reader["Id"].ToString()), new List<CMASExercise>(), patient));
                        }
                    }
                }
                connection.Close();
                return CMASses;
            }
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
                        Console.WriteLine("Data was found in the database (SSMS).");
                    }
                    else
                    {
                        Console.WriteLine("No matching data found in the database (SSMS).");
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
                            CMAS cmas = ReadCMASses(patient).Find(c => c.Id == cmasId);

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
                        Console.WriteLine("Data was found in the database (SSMS).");
                    }
                    else
                    {
                        Console.WriteLine("No matching data found in the database (SSMS).");
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Update de gegevens
                string query = @"
                    UPDATE User 
                        SET Name = @Name 
                        AND Username = @Username  
                        AND Password = @Password 
                        WHERE Id = @UserId"; ;
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new Exception("No user was found.");
                    }
                    else
                    {
                        Console.WriteLine("User has been succesfully updated.");
                    }

                }
            }
        }

        public void DeleteUser(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //Verwijdert de gegevens
                string query = "DELETE FROM User WHERE Id = @UserId ";
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Methods Guardian 
        public List<Guardian> ReadGuardians()
        {
            List<Guardian> guardians = new List<Guardian>();
            List<Patient> allPatients = ReadPatients();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Guardian.Id, Guardian.PatientId, Guardian.Name, Guardian.Email, Guardian.PhoneNumber, Guardian.ParentalLockCode FROM Guardian";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int? patientId = reader.IsDBNull(1) ? null : reader.GetInt32(1);
                            string name = reader.GetString(2);
                            string email = reader.GetString(3);
                            int phoneNumber = reader.GetInt32(4);
                            int parentalLockCode = reader.GetInt32(5);

                            Patient? patient = patientId.HasValue ? allPatients.FirstOrDefault(p => p.Id == patientId.Value) : null;

                            guardians.Add(new Guardian(id, patient, name, email, phoneNumber, parentalLockCode));
                        }
                    }
                }
            }
            return guardians;
        }

        public void CreateGuardian(Guardian guardian)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Insert Into Guardian (PatientId, Name, Email, PhoneNumber, ParentalLockCode) Values ({guardian.Patient.Id}, '{guardian.Name}', '{guardian.Email}', {guardian.PhoneNumber}, {guardian.ParentalLockCode})";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public void UpdateGuardian(Guardian guardian)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
            UPDATE Guardian
            SET 
                PatientId = @PatientId, 
                Name = @Name, 
                Email = @Email, 
                PhoneNumber = @PhoneNumber, 
                ParentalLockCode = @ParentalLockCode
            WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PatientId", guardian.Patient?.Id ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Name", guardian.Name);
                    command.Parameters.AddWithValue("@Email", guardian.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", guardian.PhoneNumber);
                    command.Parameters.AddWithValue("@ParentalLockCode", guardian.ParentalLockCode);
                    command.Parameters.AddWithValue("@Id", guardian.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Guardian successfully updated in the database.");
                    }
                    else
                    {
                        Console.WriteLine("No rows were affected. The guardian was not updated in the database.");
                    }
                }
            }
        }

        public void DeleteGuardian(Guardian guardian)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"Delete From Guardian Where Id = {guardian.Id}";
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
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
