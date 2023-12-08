using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Instructor
	{
		//Property
		public string Name { get; set; }
		public string InstructorID { get; set; }
		public string Password { get; set; }
		public string Department {  get; set; }
		public Schedule Schedule { get; set; }

		//Construct
		public Instructor()
		{

		}
		//Method
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

		public void ViewClassList()
		{
			foreach (Class c in Schedule.EnrolledClasses)
			{
				Console.WriteLine($"Course: {c.Code}");
				c.ViewEnrolledList();
			}
				
		}

		public void ViewStudentInfo()
		{
			//view basic student information
		}
	}
}
