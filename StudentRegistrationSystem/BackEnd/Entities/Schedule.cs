using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Schedule
	{
		//Property
		public List<Class> EnrolledClasses { get; set; }

		//Construct
		public Schedule() 
		{
		
		}

		//Method
		public void DisplaySchedule()
		{
			//display a formatted class schedule
		}
	}
}
