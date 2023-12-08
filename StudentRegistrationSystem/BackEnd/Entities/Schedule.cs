using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Schedule
	{
		public List<Class> EnrolledClasses { get; set; }
		public Schedule() 
		{
		
		}
	}
}
