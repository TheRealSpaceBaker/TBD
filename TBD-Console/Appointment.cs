using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBD_Console
{
    public class Appointment : CMAS
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public Patient PatientId { get; set; }
        public Doctor DoctorId { get; set; }

        public Appointment(int id, DateTime date, string description, Patient patientId, Doctor doctorId)
            : base(id, patientId)
        {
            Id = id;
            Date = date;
            Description = description;
            PatientId = patientId;
            DoctorId = doctorId;

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
