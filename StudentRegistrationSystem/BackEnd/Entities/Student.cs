using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Student
	{
		public string Name { get; set; }
		public string studentID { get; set; }
		public string Password { get; set; }
		public string Program {  get; set; }
		public int CreditEarned { get; set; }
		public string PrimaryEmail { get; set; }
		public string SecondaryEmail { get; set; }
		public string PhoneNumber { get; set; }
		public string MailingAddress { get; set; }
		public DateTime EntryDate { get; set; }
		public string Scholarships { get; set; }
		public string PostEdu { get; set; }
		public string HealthNumber { get; set; }
		public string EmergencyName { get; set; }
		public string EmergencyNumber { get; set; }
		public Schedule Schedule { get; set; }
		public List<string> CompletedCourse = new List<string>();
		
		public Student()
		{

		}
	}
}
