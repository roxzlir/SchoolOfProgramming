using Microsoft.Data.SqlClient;
using SchoolOfProgramming.Models;

namespace SchoolOfProgramming
{
    public class Program
    {
        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Database=SchoolOfProgramming;Trusted_Connection=True;MultipleActiveResultSets=True";
        static void Main(string[] args)
        {
            AppDbContextMethods.Run();
        }

        public static void ReadEmployeeDatabase() //Metod för att visa upp data ur Employee tabellen samt Professions tabellen (via ADO)
        {
            int choice;
            do //Kör med en do while loop för att visa upp all data en gång utan att ta in något värde för choice.
            { //Funderade på om jag skulle köra en while loop som tog all data om vilka professionName som fanns i Professions tabellen först men beslutade mig att bara visa upp 3 olika
                Console.Clear();
                Console.WriteLine("Welcome to your our school database!");
                Console.WriteLine("(1) - See all employees");
                Console.WriteLine("(2) - See all teachers");
                Console.WriteLine("(3) - See all substitute teachers");
                Console.WriteLine("(0) - Exit to menu");
                Console.Write("Please press the number of choise for what you want to see: ");
                choice = GetUserInput(); //Med hjälp av användarens input så kör jag olika frågor genom ett switch case som kör olika kommandon mot min databas

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();
                        switch (choice)
                        {          //Här söker vi fram endast ut employee tabellen genom att köra ingenom all data med en while loop till det ej finns med data att gå igenom.
                            case 1:
                                SqlCommand cmd = new SqlCommand("select EmpFirstName, EmpLastName, HiredDate  from Employees", sqlConnection);
                                SqlDataReader reader = cmd.ExecuteReader();
                                while (reader.Read())
                                {
                                    Console.WriteLine("\nFirstname: {0}\nLastname: {1}\nHired since: {2}\n-----------------------------------",
                                    reader["EmpFirstName"],
                                    reader["EmpLastName"],
                                    reader["HiredDate"]);
                                }
                                Console.Write("Press any key and return to menu: ");
                                Console.ReadKey();
                                break;
                            //Här kör vi kommando som söker igenom både Employee och Professions efter dem som är märkt med Teacher
                            case 2:
                                SqlCommand cmd1 = new SqlCommand("select EmpFirstName, EmpLastName, HiredDate, ProTitle from Employees " +
                                    "join Professions on FK_ProfessionID = ProfessionID where ProTitle = 'Teacher'", sqlConnection);
                                SqlDataReader reader1 = cmd1.ExecuteReader();
                                while (reader1.Read())
                                {
                                    Console.WriteLine("\nFirstname: {0}\nLastname: {1}\nHired since: {2}\nWork title: {3}\n-----------------------------------",
                                    reader1["EmpFirstName"],
                                    reader1["EmpLastName"],
                                    reader1["HiredDate"],
                                    reader1["ProTitle"]);
                                }
                                Console.Write("Press any key and return to menu: ");
                                Console.ReadKey();
                                break;
                            //Till sist kör vi efter Substitute Teacher. Jag har fler yrken men dem byggs förmodligen in framöver.
                            case 3:
                                SqlCommand cmd2 = new SqlCommand("select EmpFirstName, EmpLastName, HiredDate, ProTitle from Employees " +
                                    "join Professions on FK_ProfessionID = ProfessionID where ProTitle = 'Substitute Teacher'", sqlConnection);
                                SqlDataReader reader2 = cmd2.ExecuteReader();
                                while (reader2.Read())
                                {
                                    Console.WriteLine($"\nFirstname: {reader2["EmpFirstName"]}\nLastname: {reader2["EmpLastName"]}\nHired since: {reader2["HiredDate"]}\n" +
                                        $"Work title: {reader2["ProTitle"]}\n-----------------------------------");
                                }
                                Console.Write("Press any key and return to menu: ");
                                Console.ReadKey();
                                break;

                            default:
                                Console.WriteLine("Invalid input");
                                break;
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } while (choice != 0);
        }
        public static void SelectAllEmployees(SqlConnection insertedConnection)
        {

            SqlCommand cmd = new SqlCommand("select EmpFirstName, EmpLastName, HiredDate  from Employees", insertedConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("\nFirstname: {0}\nLastname: {1}\nHired since: {2}\n-----------------------------------",
                reader["EmpFirstName"],
                reader["EmpLastName"],
                reader["HiredDate"]);
            }
            Console.Write("Press any key and return to menu: ");
            Console.ReadKey();
        } //Denna metod är bara en start som jag funderar på att bygga vidare på,
        public static void SelectAllTeachers(SqlConnection insertedConnection) //även denna. Hade en tanke på att göra en metod som söker fram varje yrke separat istället för att få mer flexibel kod
        {                                                                     //men får se om det kommer med till slut projektet istället!

            SqlCommand cmd = new SqlCommand("select EmpFirstName, EmpLastName, HiredDate, ProTitle from Employees" +
                "join Professions on FK_ProfessionID = ProfessionID where ProTitle = 'Substitute Teacher'", insertedConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("\nFirstname: {0}\nLastname: {1}\nHired since: {2}\nWork title: {3}\n-----------------------------------",
                 reader["EmpFirstName"],
                 reader["EmpLastName"],
                 reader["HiredDate"],
                 reader["ProTitle"]);
            }
            Console.Write("Press any key and return to menu: ");
            Console.ReadKey();
        }
        public static int GetUserInput() //Då jag ska ta in mycket userInput's i heltal så gjorde jag tidigt en metod för just detta
        {
            int userChoice;
            while (true)
            {
                string userInput = Console.ReadLine();
                if (int.TryParse(userInput, out userChoice))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("You need to print a number input");
                }
            }
            return userChoice;
        }
        public static void PrintStudentGrades() //Här är metoden för att visa upp dem betyg som finns på eleverna (via ADO)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Please select which grades you would like to display: ");
                Console.WriteLine("(1) - See all given grades");
                Console.WriteLine("(2) - See grades within the last month");
                Console.WriteLine("(0) - Exit to menu");
                choice = GetUserInput();
                using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
                { //Kör även här med ett switch case som kör olika kommandon genom en while loop helt enkelt.
                    sqlConnection1.Open();
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            SqlCommand command = new SqlCommand("select StuFirstName, StuLastName, SubjectName, Grade, GradeDate from EnrollmentList " +
                            "join Students on FK_StudentID = StudentID " +
                            "join Courses on FK_CourseID = CourseID " +
                            "join GradeList on FK_EnrollmentID = EnrollmentID " +
                            "join Grades on FK_GradeID = GradeID order by GradeDate ASC", sqlConnection1);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                DateTime date = Convert.ToDateTime(reader["GradeDate"]); //Ville få bort dem tomma klockslagen från utskriften så sparar ner alla datum från reader[GradeDate] till
                                string dateOnly = date.ToString("yyyy-MM-dd");  //date och gör sedan om dem till en string med formatet jag önskar.

                                Console.WriteLine($"\nStudent: {reader["StuFirstName"]} {reader["SubjectName"]}\nCourse: {reader["SubjectName"]}\n" +
                                    $"Grade:  ({reader["Grade"]})  - since {dateOnly}");
                            }
                            Console.WriteLine("Press any key when done: ");
                            Console.ReadKey();
                            break;
                        case 2:
                            Console.Clear();
                            SqlCommand command1 = new SqlCommand("select StuFirstName, StuLastName, SubjectName, Grades.Grade, GradeList.GradeDate from EnrollmentList " +
                                "join Students on FK_StudentID = StudentID " +
                                "join Courses on FK_CourseID = CourseID " +
                                "join GradeList on FK_EnrollmentID = EnrollmentID " +
                                "join Grades on FK_GradeID = GradeID where GradeDate >= DATEADD(MONTH, -1, GETDATE()) order by GradeDate ASC ", sqlConnection1);
                            SqlDataReader reader1 = command1.ExecuteReader();
                            while (reader1.Read())
                            {
                                DateTime date = Convert.ToDateTime(reader1["GradeDate"]);
                                string dateOnly = date.ToString("yyyy-MM-dd");

                                Console.WriteLine($"\nStudent: {reader1["StuFirstName"]} {reader1["SubjectName"]}\nCourse: {reader1["SubjectName"]}\n" +
                                    $"Grade:  ({reader1["Grade"]})  - since {dateOnly}");
                            }
                            Console.WriteLine("Press any key when done: ");
                            Console.ReadKey();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Invalid input choice");
                            break;
                    }
                }
            } while (choice != 0);
        }
        public static void PrintGradePerCourse() //Här är metoden för att visa elevernas samlade betyg inom kurserna som går
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("Please select which grade details you would like to display for our courses: ");
                Console.WriteLine("(1) - See highest grade given/course");
                Console.WriteLine("(2) - See lowest grade given/course");
                Console.WriteLine("(3) - See the avarage grade given/course");
                Console.WriteLine("(0) - Exit to menu");
                choice = GetUserInput();
                using (SqlConnection sqlConnection1 = new SqlConnection(connectionString))
                {
                    Console.Clear();
                    sqlConnection1.Open(); //Här sparar jag ner som frågor jag vill köra som kommando i separata string datatyper först.
                    string maxGradeCourse = "select SubjectName, Round(Max(Cast(Grade as float)), 2) as AvarageGrade from EnrollmentList join Students on FK_StudentID = StudentID join Courses on FK_CourseID = CourseID join GradeList on FK_EnrollmentID = EnrollmentID join Grades on FK_GradeID = GradeID GROUP BY SubjectName";
                    string minGradeCourse = "select SubjectName, Round(Min(Cast(Grade as float)),2) as AvarageGrade from EnrollmentList join Students on FK_StudentID = StudentID join Courses on FK_CourseID = CourseID join GradeList on FK_EnrollmentID = EnrollmentID join Grades on FK_GradeID = GradeID GROUP BY SubjectName";
                    string avgGradeCourse = "select SubjectName, Round(Avg(Cast(Grade as float)),2) as AvarageGrade from EnrollmentList join Students on FK_StudentID = StudentID join Courses on FK_CourseID = CourseID join GradeList on FK_EnrollmentID = EnrollmentID join Grades on FK_GradeID = GradeID GROUP BY SubjectName";

                    switch (choice) //Sen kör jag dem i olika case med ett kommando som sen läses igenom och tar fram all data som finns i while loop
                    {
                        case 1:
                            SqlCommand command = new SqlCommand(maxGradeCourse, sqlConnection1);
                            SqlDataReader reader = command.ExecuteReader();
                            while (reader.Read())
                            {
                                Console.WriteLine($"\nGrade: {reader["AvarageGrade"]}\nCourse: {reader["SubjectName"]}\n");
                            }
                            Console.WriteLine("Press any key when done: ");
                            Console.ReadKey();
                            break;
                        case 2:
                            SqlCommand command1 = new SqlCommand(minGradeCourse, sqlConnection1);
                            SqlDataReader reader1 = command1.ExecuteReader();
                            while (reader1.Read())
                            {
                                Console.WriteLine($"\nGrade: {reader1["AvarageGrade"]}\nCourse: {reader1["SubjectName"]}\n");
                            }
                            Console.WriteLine("Press any key when done: ");
                            Console.ReadKey();
                            break;
                        case 3:
                            SqlCommand command2 = new SqlCommand(avgGradeCourse, sqlConnection1);
                            SqlDataReader reader2 = command2.ExecuteReader();
                            while (reader2.Read())
                            {
                                Console.WriteLine($"\nGrade: {reader2["AvarageGrade"]}\nCourse: {reader2["SubjectName"]}\n");
                            }
                            Console.WriteLine("Press any key when done: ");
                            Console.ReadKey();
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Invalid input choice");
                            break;
                    }
                }
            } while (choice != 0);
        }



    }
}
