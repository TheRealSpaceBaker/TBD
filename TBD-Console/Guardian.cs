using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Guardian
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int ParentalLockCode { get; set; }


        public Guardian(int id, string name, string username, string password)
        {

        }

        public void UnlockParentalLock(int code)
        {
            if (code == ParentalLockCode)
            {
                Console.WriteLine("Parental lock has been unlocked.");
            }
            else
            {
                Console.WriteLine("Invalid code.");
            }
        }
    }
}
