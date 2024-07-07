using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Innovatie
{
    public class Guardian
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int ParentalLockCode { get; set; }


        public Guardian(int id, Patient? patient, string name, string email, int phoneNumber, int parentalLockCode)
        {
            Id = id;
            Patient = patient;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            ParentalLockCode = parentalLockCode;
        }

        public void UnlockParentalLock(int code)
        {
            if (code == ParentalLockCode)
            {
                //Console.WriteLine("Parental lock has been unlocked.");
            }
            else
            {
               // Console.WriteLine("Invalid code.");
            }
        }
        public static List<Guardian> ReadGuardians()
        {
            return new DAL().ReadGuardians();
        }
    }
}
