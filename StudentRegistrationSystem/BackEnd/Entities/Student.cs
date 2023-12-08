using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Student
	{
		//Property
		public string Name { get; set; }
		public string studentID { get; set; }
		public string Password { get; set; }
		public string Program { get; set; }
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

		//Construct

		public Student()
		{

		}

		//Method
		public void ViewProfile()
		{
			//View student profile
		}

		public void EditProfile()
		{
			//Edit student profile
		}

		public void ContactStudentService()
		{
			//send request for other changes to account
		}

		public void ViewSchedule()
		{
			//Display scheduled class information
			foreach (Class c in Schedule.EnrolledClasses)
			{
				Console.WriteLine($"Course: {c.Code}");
				Console.WriteLine($"Instructor: {c.Instructor}");
				Console.WriteLine($"Time: {c.Time}");
			}
		}

		public void ViewClass(List<Class> allClass)
		{
			//display all classes offered
			foreach (Class c in allClass)
			{
				Console.WriteLine($"Course: {c.Code}");
				Console.WriteLine($"Instructor: {c.Instructor}");
				Console.WriteLine($"Time: {c.Time}");
			}

		}

		public void RegisterClass(Class c)
		{
			//Check availability
			bool available = c.CheckAvailability();
			//Add student into class enrolled list
			if (available) 
			{
				c.Enrolled.Add(studentID);
			}
			else
			{
				JoinWaitlist(c);
			}

		}
		public void JoinWaitlist(Class c)
		{
			//prompt message for join waitlist
			Console.WriteLine("Class is full,\n1-Join Waitlist\n2-Abort");
			int i = int.Parse(Console.ReadLine());
			if (i == 1)
			{
				c.WaitList.Add(this);
			}
			else if (i == 2)
			{
				return;
			}
		}


	}
}
