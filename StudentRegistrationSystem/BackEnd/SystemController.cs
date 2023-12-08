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

		//Read Login data file and convert it into a dicionary of id/password, for validation purpose
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

		//Read student data file, create a student object per line of data, add information to the object, then create a list of student.
		public void LoadStudentData()
		{
			//Read File
			string baseDir = Directory.GetCurrentDirectory();
			string studentFile = Path.Combine(baseDir, "Data", STUDENT_FILE);
			string[] lines = File.ReadAllLines(studentFile);
			//Get information in each line
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string password = column[1];
				string name = column[2] + ' ' + column[3];
				string email = column[4];
				string phone = column[5];
				string postEdu = column[6];
				//Student object creation and add information onto the object.
				Student student = new Student();
				student.studentID = id;
				student.Password = password;
				student.Name = name;
				student.PrimaryEmail = email;
				student.PhoneNumber = phone;
				student.PostEdu = postEdu;
				//Add object into a student list
				StudentList.Add(student);
			}
			//Read student's completed course file
			string recordFile = Path.Combine(baseDir, "Data", STUDENT_RECORD_FILE);
			string[] lines2 = File.ReadAllLines(recordFile);
			//Get information in each line
			foreach (string line in lines2)
			{
				string[] column = line.Split(',');
				string id = column[2];
				//checking if the ID from the file any existing Student Object, add completed course information when matches.
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

		//Read instructor file, create instructor object and put them into a list
		public void LoadInstructorData()
		{
			//Read File
			string baseDir = Directory.GetCurrentDirectory();
			string instructorFile = Path.Combine(baseDir, "Data", INSTRUCTOR_FILE);
			string[] lines = File.ReadAllLines(instructorFile);
			//Get information in each line
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string name = column[1] + ' ' + column[2];
				//Create instructor object and add information onto it.
				Instructor instructor = new Instructor();
				instructor.InstructorID = id;
				instructor.Name = name;
				//Add instructor into a instructor list
				InstructorList.Add(instructor);
			}
		}

		//Read admin file, create admin object and put the admin object into a list.
		public void LoadAdminData()
		{
			//Read file
			string baseDir = Directory.GetCurrentDirectory();
			string adminFile = Path.Combine(baseDir, "Data", ADMIN_FILE);
			string[] lines = File.ReadAllLines(adminFile);
			//Get information in each line
			foreach (string line in lines)
			{
				string[] column = line.Split(',');
				string id = column[0];
				string password = column[1];
				string name = column[2] + ' ' + column[3];
				//Create admin object, add information to it.
				Admin admin = new Admin();
				admin.AdminID = id;
				admin.Password = password;
				admin.Name = name;
				//Add admin object into a admin list
				AdminList.Add(admin);
			}
		}

		//Read assigned course file, create class object and put class object into a list
		public void LoadClassData()
		{
			//Read file
			string baseDir = Directory.GetCurrentDirectory();
			string assignedFile = Path.Combine(baseDir, "Data", ASSIGNED_COURSE_FILE);
			string[] lines2 = File.ReadAllLines(assignedFile);
			//Get information in each line
			foreach (string line in lines2)
			{
				string[] column = line.Split(',');
				string name = column[0];
				string code = column[1];
				string time = column[2] + '-' + column[3];
				//Create class object and add information into it.
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
				//Add class into a list.
				CourseList.Add(_class);
			}
		}
		//Homescreen when the system start.
		public void SystemHome()
		{
			//Show available options for user
			Console.WriteLine("Enter your option:");
			Console.WriteLine("1 - Login");
			Console.WriteLine("2 - Exit");
			//Ask for user input
			int selection = int.Parse(Console.ReadLine());
			switch (selection)
			{
				//1 for Login
				case 1:
					Login();
					break;
				case 2:
				//2 for Program exit
					Environment.Exit(0);
					break;
				default:
				//other input will return a line of invalid selection and return to SystemHome.
					Console.WriteLine("Invalid Selection");
					SystemHome();
					break;
			}
		}
		
		//Login method with validation and role-checking
		public void Login()
		{
			//Input ID and password
			Console.WriteLine("Enter your ID:");
			string userID = Console.ReadLine();
			Console.WriteLine("Enter your password:");
			string password = Console.ReadLine();

			//Validation
			if (ValidLoginData.ContainsKey(userID))
			{
				if (ValidLoginData[userID] == password)
				{
					//if id and password matches, it will check for role
					//Send user to their correspoinding homepage
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
					//if id or password is wrong
					Console.WriteLine("Invalid ID or Password");
					Login();
				}
			}
			else
			{
				//if id doesn't exist in database
				Console.WriteLine("ID doesn't exist, returning back to home selection");
				SystemHome();
			}
		}

		//Student homepage
		public void StudentHome(string id)
		{
			//loop through student list to find the one that matches the login id
			foreach (Student student in StudentList)
			{
				if (id == student.studentID)
				{
					//Display student homepage
					Console.WriteLine("Welcome");
					Console.WriteLine($"Name: {student.Name}");
					Console.WriteLine($"Email: {student.PrimaryEmail}");
					Console.WriteLine($"Contact Number: {student.PhoneNumber}");
					Console.WriteLine($"Education Level: {student.PostEdu}");
					Console.WriteLine($"Completed Courses:{string.Join(',', student.CompletedCourse)}");
				}
			}
			//display options for student
			Console.WriteLine("\nEnter your option:");
			Console.WriteLine("1 - View all Classes Offered");
			Console.WriteLine("2 - View Schedule");
			Console.WriteLine("3 - Contact Student Service");
			Console.WriteLine("4 - Log out");
			int selection = int.Parse(Console.ReadLine());
			switch (selection)
			{
				//View all class offered by school
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
				//View student's schedule
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
				//Send request to student service
				case 3:
					Console.WriteLine("Enter Your Request:");
					string request = Console.ReadLine();
					Console.WriteLine("Request Sent,you will get reply within 24-48 hours\n\n");
					StudentHome(id);
					break;
				//Log out
				case 4:
					Console.WriteLine("Logging Out");
					SystemHome();
					break;
				//Wrong input message
				default:
					Console.WriteLine("Invalid Selection");
					StudentHome(id);
					break;
			}
		}

		//Instructor Homepage
		public void InstructorHome(string id)
		{
			//Loop through instructor list to find the one matches the login id
			foreach (Instructor instructor in InstructorList)
			{
				if (id == instructor.InstructorID)
				{
					//Display homepage and options
					Console.WriteLine($"Welcom, {instructor.Name}");
					Console.WriteLine("Enter your option:");
					Console.WriteLine("1 - View all Classes Offered");
					Console.WriteLine("2 - View Classlist");
					Console.WriteLine("3 - Log out");
					int selection = int.Parse(Console.ReadLine());

					switch (selection)
					{
						//Display all classes offered
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
						//Display instructor's classes with student list
						case 2:
							//loop through all classes and find the ones that matches instructor name
							foreach (Class c in CourseList)
							{
								if (instructor.Name.Contains(c.Instructor))
								{
									Console.WriteLine($"Course: {c.Code}");
									Console.WriteLine($"Time: {c.Time}");
									Console.WriteLine($"Class List: {string.Join(',', c.Enrolled)}");
								}
							}
							//Instructor can view student information by input student id, exit to homepage by inputing 0.
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
									//loop through student list to find matching id
									foreach (Student student in StudentList)
									{
										if (sID == student.studentID)
										{
											//Display student information
											Console.WriteLine($"Student ID: {student.studentID}");
											Console.WriteLine($"Student Name: {student.Name}");
											Console.WriteLine($"Email: {student.PrimaryEmail}\n");
											break;
										}
									}
								}


							}
							break;
						//Logout
						case 3:
							Console.WriteLine("Logging Out");
							SystemHome();
							break;
						//Invalid input message
						default:
							Console.WriteLine("Invalid Selection");
							InstructorHome(id);
							break;
					}
				}
			}
		}

		//Admin homepage
		public void AdminHome(string id)
		{
			//loops through admin list to find the matching login id
			foreach (Admin admin in AdminList)
			{
				if (id == admin.AdminID)
				{
					//Display homepage and options
					Console.WriteLine($"Welcom, {admin.Name}");
					Console.WriteLine("Enter your option:");
					Console.WriteLine("1 - View all Classes Offered");
					Console.WriteLine("2 - Manage Student Information");
					Console.WriteLine("3 - View Student Emergency Information");
					Console.WriteLine("4 - Log out");
					int selection = int.Parse(Console.ReadLine());
					switch (selection)
					{
						//View all classes offered
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
						//Manage student information
						case 2:
							//Input student if and loop through student list to find matching one
							Console.WriteLine("Enter Student ID:");
							string sID = Console.ReadLine();
							int n = 0;
							foreach (Student student in StudentList)
							{
								if (sID == student.studentID)
								{
									//Display student information when a match is found
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
									n++;
									break;
								}
							}
							if (n == 0)
							{
								Console.WriteLine("No Result Found");
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							AdminHome(id);
							break;
						//View student emergency information
						case 3:
							Console.WriteLine("Enter Student ID:");
							sID = Console.ReadLine();
							int k = 0;
							//loops through student list to find a matching one
							foreach (Student student in StudentList)
							{
								if (sID == student.studentID)
								{
									if (!string.IsNullOrEmpty(student.EmergencyName) && !string.IsNullOrWhiteSpace(student.EmergencyNumber))
									{
										//display emergency information if it exist
										Console.WriteLine($"Student Name: {student.Name}");
										Console.WriteLine($"Emergency Contact Name: {student.EmergencyName}");
										Console.WriteLine($"Emergency Phone Number: {student.EmergencyNumber}");
									}
									else
									{
										//display missing information message
										Console.WriteLine($"Student Name: {student.Name}");
										Console.WriteLine("Student Emergency information missing");
									}
								}
								else
								{
									k++;
								}

							}
							if (k == 0)
							{
								Console.WriteLine("No Result Found");
							}
							Console.WriteLine("Press any key to continue...");
							Console.ReadKey();
							AdminHome(id);
							break;
						//Logout
						case 4:
							Console.WriteLine("Logging Out");
							SystemHome();
							break;
						//Invalid input
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
