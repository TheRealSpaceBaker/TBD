using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Guardian : User
    {
        public Patient Patient { get; set; }
        public int ParentalLockCode { get; set; }

        public Guardian(int id, string name, string username, string password)
            : base(id, name, username, password)
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
