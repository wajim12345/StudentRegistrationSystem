using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationSystem.BackEnd.Entities
{
	internal class Admin
	{
		public string Name { get; set; }
		public string AdminID { get; set; }
		public string Password { get; set; }

		public Admin()
		{

		}
	}
}
