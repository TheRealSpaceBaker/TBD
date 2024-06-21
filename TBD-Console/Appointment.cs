using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public DateTime Date { get; set; }
        public CMAS? CMAS { get; set; }
        public string Description { get; set; }

        public Appointment(int id, Patient? patient, Doctor? doctor, DateTime date, CMAS? cmas, string description)
        {
            Id = id;
            Patient = patient;
            Doctor = doctor;
            Date = date;
            CMAS = cmas;
            Description = description;
        }

        public void FillInData()
        {

        }
        public void CancelAppointment()
        {

        }
        public void MakeAppointment()
        {

        }

    }
}
