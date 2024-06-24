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
                            int accesLevel = reader.GetInt32(2); // Lees het accesLevel (index 2)
                            string username = reader.GetString(3); // Lees het username (index 3)
                            string password = reader.GetString(4); // Lees het password (index 4)


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
                        (Id, Name, Acceslevel, Username, Password) 
                    VALUES 
                        (@UserId, @Name, @Acceslevel, @Username, @Password)";
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
