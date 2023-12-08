using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class DepartmentLeader
	{
		public string Name { get; set; }
		public string DepartmentLeaderId { get; set; }
		public string Password { get; set; }

		public DepartmentLeader() 
		{
		
		}
	}
}
