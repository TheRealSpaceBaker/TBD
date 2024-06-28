using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(int id, string name, string username, string password)
        {
            Id = id;
            Name = name;
            Username = username;
            Password = password;
        }

        public void LogIn(string username, string password)
        {
            if (username == Username && password == Password)
            {
                Console.WriteLine("Logged in");
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
        }

        public void LogOut()
        {
            Console.WriteLine("Logged out succesfully");
        }
    }
}
