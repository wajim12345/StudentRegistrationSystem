using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using StudentRegistrationSystem.BackEnd.Entities;

namespace StudentRegistrationSystem.BackEnd
{
	internal class SystemController
	{
		const string ADMIN_FILE = "adminlist.csv";
		const string LOGINDATA_FILE = "logindata.csv";
		const string STUDENT_FILE = "student_record.csv";
		const string STUDENT_RECORD_FILE = "record_list.csv";
		const string INSTRUCTOR_FILE = "instructorlist.csv";
		const string ASSIGNED_COURSE_FILE = "assignedcourses.csv";
		const string COMPLETED_COURSE_FILE = "completedcourses.csv";
		public Dictionary<string, string> ValidLoginData = new Dictionary<string, string>();
		public List<Student> StudentList = new List<Student>();
		public List<Instructor> InstructorList = new List<Instructor>();
		public List<Admin> AdminList = new List<Admin>();
		public List<Class> CourseList = new List<Class>();

		public SystemController()
		{
			LoadLoginData();
			LoadStudentData();
			LoadInstructorData();
			LoadAdminData();
			LoadClassData();
			SystemHome();
		}

		//Get login information from Database
		public void LoadLoginData()
		{
			string baseDir = Directory.GetCurrentDirectory();
			string loginFile = Path.Combine(baseDir, "Data", LOGINDATA_FILE);
			string[] lines = File.ReadAllLines(loginFile);
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string password = column[1];
				ValidLoginData.Add(id, password);
			}
		}
		public void LoadStudentData()
		{
			string baseDir = Directory.GetCurrentDirectory();
			string studentFile = Path.Combine(baseDir, "Data", STUDENT_FILE);
			string[] lines = File.ReadAllLines(studentFile);
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string password = column[1];
				string name = column[2] + ' ' + column[3];
				string email = column[4];
				string phone = column[5];
				string postEdu = column[6];
				Student student = new Student();
				student.studentID = id;
				student.Password = password;
				student.Name = name;
				student.PrimaryEmail = email;
				student.PhoneNumber = phone;
				student.PostEdu = postEdu;
				StudentList.Add(student);
			}

			string recordFile = Path.Combine(baseDir, "Data", STUDENT_RECORD_FILE);
			string[] lines2 = File.ReadAllLines(recordFile);
			foreach (string line in lines2)
			{
				string[] column = line.Split(',');
				string id = column[2];
				foreach (Student student in StudentList)
				{
					if (id == student.studentID)
					{
						for (int i = 6; i <= 10; i++)
						{
							string course = column[i];

							if (!string.IsNullOrWhiteSpace(course))
							{
								student.CompletedCourse.Add(course);
							}
						}

					}
				}
			}
		}

		public void LoadInstructorData()
		{
			string baseDir = Directory.GetCurrentDirectory();
			string instructorFile = Path.Combine(baseDir, "Data", INSTRUCTOR_FILE);
			string[] lines = File.ReadAllLines(instructorFile);
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string name = column[1] + ' ' + column[2];
				Instructor instructor = new Instructor();
				instructor.InstructorID = id;
				instructor.Name = name;
				InstructorList.Add(instructor);
			}
		}
		public void LoadAdminData()
		{
			string baseDir = Directory.GetCurrentDirectory();
			string adminFile = Path.Combine(baseDir, "Data", ADMIN_FILE);
			string[] lines = File.ReadAllLines(adminFile);
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string password = column[1];
				string name = column[2] + ' ' + column[3];
				Admin admin = new Admin();
				admin.AdminID = id;
				admin.Password = password;
				admin.Name = name;
				AdminList.Add(admin);
			}
		}
		public void LoadClassData()
		{
			string baseDir = Directory.GetCurrentDirectory();
			string assignedFile = Path.Combine(baseDir, "Data", ASSIGNED_COURSE_FILE);
			string[] lines2 = File.ReadAllLines(assignedFile);
			foreach (string line in lines2)
			{
				string[] column = line.Split(',');
				string name = column[0];
				string code = column[1];
				string time = column[2] + '-' + column[3];
				Class _class = new Class();
				_class.Instructor = name;
				_class.Code = code;
				_class.Time = time;
				for (int i = 4; i <= 9; i++)
				{
					string student = column[i];

					if (!string.IsNullOrWhiteSpace(student))
					{
						_class.Enrolled.Add(student);
					}
				}
				CourseList.Add(_class);
			}
		}

		public void SystemHome()
		{
			Console.WriteLine("Enter your option:");
			Console.WriteLine("1 - Login");
			Console.WriteLine("2 - Exit");
			int selection = int.Parse(Console.ReadLine());
			switch (selection)
			{
				case 1:
					Login();
					break;
				case 2:
					Environment.Exit(0);
					break;
				default:
					Console.WriteLine("Invalid Selection");
					SystemHome();
					break;
			}
		}

		public void Login()
		{
			Console.WriteLine("Enter your ID:");
			string userID = Console.ReadLine();
			Console.WriteLine("Enter your password:");
			string password = Console.ReadLine();
			if (ValidLoginData.ContainsKey(userID))
			{
				if (ValidLoginData[userID] == password)
				{
					Console.WriteLine("Login Successfully");
					char firstDigit = userID[0];
					switch (firstDigit)
					{
						case '4':
							StudentHome(userID);
							break;
						case '5':
							InstructorHome(userID);
							break;
						case '6':
							AdminHome(userID);
							break;
					}
				}
				else
				{
					Console.WriteLine("Invalid ID or Password");
					Login();
				}
			}
			else
			{
				Console.WriteLine("ID doesn't exist, returning back to home selection");
				SystemHome();
			}
		}
		public void StudentHome(string id)
		{
			foreach (Student student in StudentList)
			{
				if (id == student.studentID)
				{
					Console.WriteLine("Welcome");
					Console.WriteLine($"Name: {student.Name}");
					Console.WriteLine($"Email: {student.PrimaryEmail}");
					Console.WriteLine($"Contact Number: {student.PhoneNumber}");
					Console.WriteLine($"Education Level: {student.PostEdu}");
					Console.WriteLine($"Completed Courses:{string.Join(',', student.CompletedCourse)}");
				}
			}
			Console.WriteLine("\nEnter your option:");
			Console.WriteLine("1 - View all Classes Offered");
			Console.WriteLine("2 - View Schedule");
			Console.WriteLine("3 - Contact Student Service");
			Console.WriteLine("4 - Log out");
			int selection = int.Parse(Console.ReadLine());
			switch (selection)
			{
				case 1:
					foreach (Class c in CourseList)
					{
						Console.WriteLine($"Course: {c.Code}");
						Console.WriteLine($"Instructor: {c.Instructor}");
						Console.WriteLine($"Time: {c.Time}\n");
					}
					Console.WriteLine("Enter your option:");
					Console.WriteLine("1 - Register Class");
					Console.WriteLine("2 - Withdraw Class");
					int s = int.Parse(Console.ReadLine());
					if (s == 1)
					{
						Console.WriteLine("Registration not opened yet, returning to homepage");
					}
					else if (s == 2)
					{
						Console.WriteLine("Withdraw unavailable at the moment, returning to homepage");
					}
					else
					{
						Console.WriteLine("Invalid Selection, returning to homepage");
					}
					StudentHome(id);
					break;
				case 2:
					Console.WriteLine("Enrolled Courses:");
					foreach (Class c in CourseList)
					{
						if (c.Enrolled.Contains(id))
						{
							Console.WriteLine($"Course: {c.Code}\nInstructor: {c.Instructor}\nTime: {c.Time}\n\n");
						}
					}
					Console.WriteLine("Press any key to continue...");
					Console.ReadKey();
					StudentHome(id);
					break;
				case 3:
					Console.WriteLine("Enter Your Request:");
					string request = Console.ReadLine();
					Console.WriteLine("Request Sent,you will get reply within 24-48 hours\n\n");
					StudentHome(id);
					break;
				case 4:
					Console.WriteLine("Logging Out");
					SystemHome();
					break;
				default:
					Console.WriteLine("Invalid Selection");
					StudentHome(id);
					break;
			}
		}
		public void InstructorHome(string id)
		{
			
			foreach (Instructor instructor in InstructorList)
			{
				if (id == instructor.InstructorID)
				{
					Console.WriteLine($"Welcom, {instructor.Name}");
					Console.WriteLine("Enter your option:");
					Console.WriteLine("1 - View all Classes Offered");
					Console.WriteLine("2 - View Classlist");
					Console.WriteLine("3 - Log out");
					int selection = int.Parse(Console.ReadLine());
					switch (selection)
					{
						case 1:
							foreach (Class c in CourseList)
							{
								Console.WriteLine($"Course: {c.Code}");
								Console.WriteLine($"Instructor: {c.Instructor}");
								Console.WriteLine($"Time: {c.Time}\n\n");
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							InstructorHome(id);
							break;
						case 2:
							foreach (Class c in CourseList)
							{
								if (instructor.Name.Contains(c.Instructor))
								{
									Console.WriteLine($"Course: {c.Code}");
									Console.WriteLine($"Time: {c.Time}");
									Console.WriteLine($"Class List: {string.Join(',', c.Enrolled)}");
								}
								bool viewStudent = true;
								while (viewStudent)
								{
									Console.WriteLine("Enter Student ID to view Student Information");
									Console.WriteLine("Or enter 0 to exit");
									string sID = Console.ReadLine();
									if (sID == "0")
									{
										viewStudent = false;
										InstructorHome(id);
									}
									else
									{
										foreach (Student student in StudentList)
										{
											if (sID == student.studentID)
											{
												Console.WriteLine($"Student ID: {student.studentID}");
												Console.WriteLine($"Student Name: {student.Name}");
												Console.WriteLine($"Email: {student.PrimaryEmail}\n");
												break;
											}
											else
											{
												Console.WriteLine("Invalid ID");
											}
										}
									}
								}

							}
							break;
						case 3:
							Console.WriteLine("Logging Out");
							SystemHome();
							break;
						default:
							Console.WriteLine("Invalid Selection");
							InstructorHome(id);
							break;
					}
				}
			}
		}

		public void AdminHome(string id)
		{
			foreach (Admin admin in AdminList)
			{
				if (id == admin.AdminID)
				{
					Console.WriteLine($"Welcom, {admin.Name}");
					Console.WriteLine("Enter your option:");
					Console.WriteLine("1 - View all Classes Offered");
					Console.WriteLine("2 - Manage Student Information");
					Console.WriteLine("3 - View Student Emergency Information");
					Console.WriteLine("4 - Log out");
					int selection = int.Parse(Console.ReadLine());
					switch (selection)
					{
						case 1:
							foreach (Class c in CourseList)
							{
								Console.WriteLine($"Course: {c.Code}");
								Console.WriteLine($"Instructor: {c.Instructor}");
								Console.WriteLine($"Time: {c.Time}\n\n");
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							AdminHome(id);
							break;
						case 2:
							Console.WriteLine("Enter Student ID:");
							string sID = Console.ReadLine();
							foreach (Student student in StudentList)
							{
								if (sID == student.studentID)
								{
									Console.WriteLine($"Student Name: {student.Name}");
									Console.WriteLine($"Email: {student.PrimaryEmail}");
									Console.WriteLine($"Contact Number: {student.PhoneNumber}");
									Console.WriteLine($"Education Level: {student.PostEdu}");
									Console.WriteLine($"Completed Courses:{string.Join(',', student.CompletedCourse)}");
									Console.WriteLine("Enrolled Courses:");
									foreach (Class c in CourseList)
									{
										if (c.Enrolled.Contains(sID))
										{
											Console.WriteLine($"Course: {c.Code}\nInstructor: {c.Instructor}\nTime: {c.Time}\n");
										}
									}

									break;
								}
								else
								{
									Console.WriteLine("Invalid ID");
								}
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							AdminHome(id);
							break;
						case 3:
							Console.WriteLine("Enter Student ID:");
							sID = Console.ReadLine();
							foreach (Student student in StudentList)
							{
								if (sID == student.studentID)
								{
									if (!string.IsNullOrEmpty(student.EmergencyName) && !string.IsNullOrWhiteSpace(student.EmergencyNumber))
									{
										Console.WriteLine($"Student Name: {student.Name}");
										Console.WriteLine($"Emergency Contact Name: {student.EmergencyName}");
										Console.WriteLine($"Emergency Phone Number: {student.EmergencyNumber}");
									}
									else
									{
										Console.WriteLine("Student Emergency information missing");
									}
								}
								else
								{
									Console.WriteLine("Invalid ID");
								}
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							AdminHome(id);
							break;
						case 4:
							Console.WriteLine("Logging Out");
							SystemHome();
							break;
						default:
							Console.WriteLine("Invalid Selection");
							AdminHome(id);
							break;




					}
				}
			}
		}
	}
}
