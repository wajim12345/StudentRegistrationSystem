using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Class
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public string Instructor {  get; set; }
		public int Capacity { get; set; }
		public List<Student> WaitList { get; set; }
		public int Credits { get; set; }
		public List<string> Enrolled = new List<string>();
		public string Prerequisites { get; set; }
		public string Time {  get; set; }
		public string Location { get; set; }

		public Class()
		{

		}
	}
}
