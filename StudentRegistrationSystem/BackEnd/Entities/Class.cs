using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Class
	{
		//Property
		public string Name { get; set; }
		public string Code { get; set; }
		public string Instructor { get; set; }
		public int Capacity { get; set; }
		public List<Student> WaitList { get; set; }
		public int Credits { get; set; }
		public List<string> Enrolled = new List<string>();
		public string Prerequisites { get; set; }
		public string Time { get; set; }
		public string Location { get; set; }

		//Construct
		public Class()
		{

		}

		//Method

		public bool CheckAvailability()
		{
			//Check if there is an available spot for enrollment
			if (Capacity - (Enrolled.Count) > 0)
			{
				return true;
			}
			return false;
		}
		public void AutoEnroll()
		{
			//if there is a spot for enrollment
			if (CheckAvailability())
			{
				//Put the first student in waitlist into enrolled list
				Enrolled.Add(WaitList[0].studentID);
				//remove the first student in waitlist.
				WaitList.Remove(WaitList[0]);
			}
		}

		public Class FindClass(string code)
		{
			//Find the class through class code
			if (code == Code)
			{
				//return the class if matches
				return this;
			}
			return null;
		}

		public void ViewEnrolledList()
		{
			//Display the list of enrolled student (id)
			Console.WriteLine($"Class List: {string.Join(',', Enrolled)}");
		}

		public void ViewWaitList()
		{
			//Convert Student object list into a list of student id
			List<string> list = new List<string>();
			foreach (Student student in WaitList) 
			{
				list.Add(student.studentID);
			}
			//Display the waitlist student id
			Console.WriteLine($"Wait List: {string.Join(',', list)}");
		}

	}
}
