using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Admin
	{
		//Property
		public string Name { get; set; }
		public string AdminID { get; set; }
		public string Password { get; set; }

		//Construct
		public Admin()
		{

		}

		//Method

		public void ViewEmergencyInfo(Student student)
		{
			//Display information if it is complete, else return missing information.
			if (!string.IsNullOrEmpty(student.EmergencyName) && !string.IsNullOrWhiteSpace(student.EmergencyNumber))
			{
				Console.WriteLine($"Student Name: {student.Name}");
				Console.WriteLine($"Emergency Contact Name: {student.EmergencyName}");
				Console.WriteLine($"Emergency Phone Number: {student.EmergencyNumber}");
			}
			else
			{
				Console.WriteLine($"Student Name: {student.Name}");
				Console.WriteLine("Student Emergency information missing");
			}
		}

		public Student CreateStudent()
		{
			//Create new student
			Student student = new Student();
			//Add data into the object
			Console.WriteLine("Enter student name");
			student.Name = Console.ReadLine();
			Console.WriteLine("Enter student ID");
			student.studentID = Console.ReadLine();
			//etc.

			return student;
		}

		public void StartRegistration()
		{
			//Start class registration period
		}

		public void EndRegistration()
		{
			//End class registration period
		}

		public void ManageStudentProfile(Student student)
		{
			//View and edit information on student profile
		}
	}
}
