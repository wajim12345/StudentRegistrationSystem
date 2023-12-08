using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class DepartmentLeader
	{
		//Property
		public string Name { get; set; }
		public string DepartmentLeaderId { get; set; }
		public string Password { get; set; }

		//Construct
		public DepartmentLeader() 
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
	}
}
