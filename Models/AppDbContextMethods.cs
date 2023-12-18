using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOfProgramming.Models
{
    partial class AppDbContextMethods : DbContext
    {
        public static void Run()
        {
            int userMenuChoice;
            do
            {
                Console.Clear();
                Logo();
                Console.WriteLine(" - Welcome to The School Of Programming Database - ");
                Console.WriteLine("(1) - Employees info");
                Console.WriteLine("(2) - Students info");
                Console.WriteLine("(3) - Classes info");
                Console.WriteLine("(4) - Grades info");
                Console.WriteLine("(5) - Add new Student");
                Console.WriteLine("(6) - Add new Employee");
                Console.WriteLine("(0) - Exit");
                Console.Write("Please select from the menu above: ");
                userMenuChoice = Program.GetUserInput();
                switch (userMenuChoice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("- Employee Info -");
                        Program.ReadEmployeeDatabase();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("- Student Info -");
                        Console.WriteLine("(1) - Lastname");
                        Console.WriteLine("(2) - Firstname");
                        Console.WriteLine("(0) - Exit");
                        Console.Write("Please chose what you would like to sort the studentrecord by: ");
                        int choice = Program.GetUserInput();
                        switch (choice)
                        {
                            case 1:
                                int i;
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("(1) - Ascending");
                                    Console.WriteLine("(2) - Descending");
                                    Console.WriteLine("(0) - Exit to menu");
                                    Console.Write("Please chose in what order: ");
                                    i = Program.GetUserInput();
                                    switch (i)
                                    {
                                        case 1:
                                            Console.Clear();
                                            PrintStuLastAsc();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            PrintStuLastDes();
                                            break;
                                        default:
                                            Console.WriteLine("Invalid choice");
                                            break;
                                    }
                                } while (i != 0);
                                break;
                            case 2:
                                int ii;
                                do
                                {
                                    Console.Clear();
                                    Console.WriteLine("(1) - Ascending");
                                    Console.WriteLine("(2) - Descending");
                                    Console.WriteLine("(0) - Exit");
                                    Console.Write("Please chose in what order: ");
                                    ii = Program.GetUserInput();
                                    switch (ii)
                                    {
                                        case 1:
                                            Console.Clear();
                                            PrintStuFirstAsc();
                                            break;
                                        case 2:
                                            Console.Clear();
                                            PrintStuFirstDes();
                                            break;
                                        default:
                                            Console.WriteLine("Invalid choice");
                                            break;
                                    }
                                } while (ii != 0);
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("- Class Info -");
                        Console.WriteLine("(1) - List all classes");
                        Console.WriteLine("(2) - List students by class");
                        Console.WriteLine("(0) - Exit to menu");
                        Console.Write("Please chose what you would like to sort the student record by: ");
                        int iy = Program.GetUserInput();
                        switch (iy)
                        {

                            case 1:
                                Console.Clear();
                                PrintClasses();
                                Console.WriteLine("Press any key when done: ");
                                Console.ReadKey();
                                break;
                            case 2:
                                Console.Clear();
                                ClassChoice();
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("- Grades Info -");
                        Console.WriteLine("(1) - Course grades");
                        Console.WriteLine("(2) - Student grades");
                        Console.WriteLine("(0) - Exit");
                        Console.Write("Please chose what you would like to sort the student record by: ");
                        int y = Program.GetUserInput();
                        switch (y)
                        {
                            case 1:
                                Console.Clear();
                                Program.PrintGradePerCourse();
                                break;
                            case 2:
                                Console.Clear();
                                Program.PrintStudentGrades();
                                break;
                            case 0:
                                break;
                            default:
                                Console.WriteLine("Invalid choice");
                                break;
                        }
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("- Add a new student to database -");
                        AddStudent();
                        break;
                    case 6:
                        Console.Clear();
                        AddEmployee();
                        break;
                    case 0:
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            } while (userMenuChoice != 0);
        }


        public static void ClassChoice() //Kör metod för att få fram studenter i varje klass som finns
        {
            int classChoice;
            do //Kör med en DO/WHILE så man kan välja om klass och låter användaren välja vilken klass den vill se. Kör då
            { //metoden PrintStuClasses där jag har en inparameter som tar in vilken klass som ska skickas med till SQL
                Console.Clear();
                Console.WriteLine("This is all our classes at the present time: ");
                Console.WriteLine("(1) - BinaryBuddies");
                Console.WriteLine("(2) - CodeSlingers");
                Console.WriteLine("(3) - GitRDone");
                Console.WriteLine("(4) - TheC#s");
                Console.WriteLine("(0) - Exit");
                Console.Write("Please select which class you would like to see a complete studentlist for: ");
                classChoice = Program.GetUserInput();
                switch (classChoice)
                {
                    case 1:
                        PrintStuClass("BinaryBuddies");
                        Console.WriteLine("Press any key when done: ");
                        Console.ReadKey();
                        break;
                    case 2:
                        PrintStuClass("CodeSlingers");
                        Console.WriteLine("Press any key when done: ");
                        Console.ReadKey();
                        break;
                    case 3:
                        PrintStuClass("GitRDone");
                        Console.WriteLine("Press any key when done: ");
                        Console.ReadKey();
                        break;
                    case 4:
                        PrintStuClass("TheC#s");
                        Console.WriteLine("Press any key when done: ");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("You have made a invalide input.");
                        break;
                }
            } while (classChoice != 0);
        }
        public static void PrintStuClass(string userChoice) //Den metod som faktiskt printar ut studenter i resp. klass
        {                                      //Istället för att göra 4 metoder så gjorda jag 1 där man skickar med 
            Console.Clear();                  //namnet på klassen i en string som går direkt till SQL frågan mot db.
            using (AppDbContext dbContext = new AppDbContext())
            { //Skapar en lista från EnrollmentList tabellen och joinar in Students och Classes för att kunna 
                //få fram studenter kopplad till sin klass då EnrollmentList är mitt connection table
                var studentClassList = dbContext.EnrollmentLists
                    .Join(dbContext.Students, e => e.FkStudentId, s => s.StudentId, (enrollment, student) => new
                    { //joinar med ett "sant kommando" för att komma åt FkStudentId och StudentID från Students och 
                        //EnrollmentList. Sen döper jag dem till enrollment och student.
                        enrollment,
                        student
                    })
                    .Join(dbContext.Classes, combined => combined.enrollment.FkClassId, c => c.ClassId, (combined, _class) => new
                    { //joinar in Classes och "sant kommando".enrollment som är FkStudentId och matchar det mot FkClassId
                        //samt kör "sant kommando" för att nå ClassId och det blir sen då combined och _class.
                        combined.student.StuFirstName,
                        combined.student.StuLastName,
                        _class.ClassName
                    }).Where(Classes => Classes.ClassName == userChoice).ToList();


                foreach (var person in studentClassList)
                {
                    Console.WriteLine($"Class - {person.ClassName}: {person.StuFirstName} {person.StuLastName}");
                }
            }
        }
        public static void PrintClasses() //Enkel metod för att lista alla ClassName som finns inuti Classes
        {
            Console.Clear();
            using (AppDbContext dbContext = new AppDbContext())
            { //Sparar ner allt från Classes i en lista
                var classList = dbContext.Classes.ToList();
                int count = 1;
                foreach (var names in classList) //Kör sedan en foreach loop och radar upp alla ClassName
                {
                    Console.WriteLine(count++ + ". Class: " + names.ClassName);
                }

            }
        }
        public static void PrintStuLastAsc() //Printa all data i Students med Last Name Ascending
        {
            Console.Clear();
            using (AppDbContext dbContext = new AppDbContext())
            {
                //Skapar en lista här med men kör OrderBy och säljer att det ska vara på StuLastName, OrderBy lägger dem
                //i ASC ordning
                var studentList = dbContext.Students.OrderBy(Students => Students.StuLastName).ToList();
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\nStudent lastname: {student.StuLastName}\nStudent firstname: {student.StuFirstName}\n");
                }
            }
            Console.WriteLine("Press any key when done: ");
            Console.ReadKey();
        }
        public static void PrintStuLastDes() //Printa all data i Students med Last Name Descending
        {
            Console.Clear();
            using (AppDbContext dbContext = new AppDbContext())
            {
                //Skapar en lista här med men kör OrderByDescending och säljer att det ska vara på StuLastName igen
                //OrderByDescending lägger dem såklart i Descending ordning.
                var studentList = dbContext.Students.OrderByDescending(Students => Students.StuLastName).ToList();
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\nStudent lastname: {student.StuLastName}\nStudent firstname: {student.StuFirstName}\n");
                }
            }
            Console.WriteLine("Press any key when done: ");
            Console.ReadKey();
        }
        public static void PrintStuFirstDes() //Likadant som i LastName fast nu mot FirstName
        {
            Console.Clear();
            using (AppDbContext dbContext = new AppDbContext())
            {
                var studentList = dbContext.Students.OrderByDescending(Students => Students.StuFirstName).ToList();
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\nStudent firstname: {student.StuFirstName}\nStudent lastname: {student.StuLastName}\n");
                }
            }
            Console.WriteLine("Press any key when done: ");
            Console.ReadKey();
        }
        public static void PrintStuFirstAsc()
        {
            Console.Clear();
            using (AppDbContext dbContext = new AppDbContext())
            {
                var studentList = dbContext.Students.OrderBy(Students => Students.StuFirstName).ToList();
                foreach (var student in studentList)
                {
                    Console.WriteLine($"\nStudent firstname: {student.StuFirstName}\nStudent lastname: {student.StuLastName}\n");
                }
            }
            Console.WriteLine("Press any key when done: ");
            Console.ReadKey();
        }//Likadant som i LastName fast nu mot FirstName
        public static void AddEmployee() //Metoden för att lägga till data i Employee tabellen
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                Console.WriteLine("Please fill out these details to add a new employee to the database");
                Console.Write("First name: ");
                string empFirstName = Console.ReadLine();

                Console.Write("Last name: ");
                string empLastName = Console.ReadLine();

                Console.Write("Date of birth (YYYYMMDD)");
                long DoB = Program.GetUserInput();

                //Då min HiredDate är av datatypen DateTime så måste jag se till att vi får in användarens input till just DateTime
                //och väljer då att köra en while loop för att detta måste ske innan vi går vidare
                DateTime result;
                while (true)
                {
                    Console.Write("Hired date (YYYYMMDD): "); //Ber användaren fylla i år, månad och dag i denna ordning
                    string userInputDate = Console.ReadLine();
                    if (DateTime.TryParseExact(userInputDate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out result))
                    { //Kör här en IF sats som endast byts om detta lyckas. Kör med TryParseExact metoden för att "rätta" userInputDate och den går ut till datatypen DateTime result sen.
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You made a invalid input");
                    }
                }
                int profID = 0;
                int profIDUserInput;
                do //Då jag också har FK_ProfessionID i min Employee tabell som styr vilket yrke som den anställda har så hårdkodar jag en lista över dem yrkena
                { //som användaren får välja  vilken anställning den nya employee ska ha
                    Console.WriteLine("Please chose which profession this employee are hired as: ");
                    Console.WriteLine($"(0) - Principal");
                    Console.WriteLine($"(1) - Vice Principal");
                    Console.WriteLine($"(2) - IT Responsible");
                    Console.WriteLine($"(3) - Financial Manager");
                    Console.WriteLine($"(4) - Teacher");
                    Console.WriteLine($"(5) - Substitute Teacher");
                    profIDUserInput = Program.GetUserInput();
                    if (profIDUserInput < 6) //För att säkerställa att det inte blir en siffa som ej existerar så har jag gjort en IF sats att userInput måste vara mindre än 6 för att den ska
                    {                       //bryta loopen. Mina FkProfessionId går dock från 1-6 så jag lägger till +1 på vad än användaren skriver in.
                        profID = profIDUserInput + 1;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have made a invalid input");
                    }
                } while (true);

                string role = "";
                Console.WriteLine("New employee added to database:");
                switch (profID)
                {
                    case 1:
                        role = "Principal";
                        break;
                    case 2:
                        role = "Vice Principal";
                        break;
                    case 3:
                        role = "IT Responsible";
                        break;
                    case 4:
                        role = "Financial Manager";
                        break;
                    case 5:
                        role = "Teacher";
                        break;
                    case 6:
                        role = "Substitute Teacher";
                        break;
                    default:
                        break;
                }   //Då jag vill ge en utskrift av all data som kommer spara in i databasen så tar jag profID genom en switch sats med samma hårkodade yrkes roll namn
                    //för att kunna få med det i utskriften nedan

                Console.WriteLine($"Name: {empLastName}, {empFirstName}\nDate of birth: {DoB}\n" +
                    $"Hired as: {role}\nFirst day of work: {result}");

                var emp = new Employee //Här skapar jag då ett nytt Employee objekt av all data jag samlta in
                {
                    EmpFirstName = empFirstName,
                    EmpLastName = empLastName,
                    EmpDoB = DoB,
                    HiredDate = result,
                    FkProfessionId = profID
                };
                dbContext.Employees.Add(emp); //Slutligen så lägger jag till det nya Employee objektet till databasen
                dbContext.SaveChanges();     //och spara ändringarna

                Console.WriteLine("Press any key when done: ");
                Console.ReadKey();
            }
        }
        public static void AddStudent() //Metoden för att lägga till en Student har samma tänk bakom sig som Employee men den är mycket enklare pga mindre saker som behöver skriva in
        {
            using (AppDbContext dbContext = new AppDbContext())
            {
                Console.WriteLine("Please fill out these details to add a new student to the database");
                Console.Write("First name: ");
                string stuFirstName = Console.ReadLine();

                Console.Write("Last name: ");
                string stuLastName = Console.ReadLine();

                Console.Write("Date of birth (YYYYMMDD)");
                long stuDoB = Program.GetUserInput();

                var stu = new Student
                {
                    StuFirstName = stuFirstName,
                    StuLastName = stuLastName,
                    StuDoB = stuDoB
                };
                Console.WriteLine("Added student to database:\n");
                Console.WriteLine($"Name: {stuLastName}, {stuFirstName}\nDate of birth: {stuDoB}\n");

                dbContext.Students.Add(stu);
                dbContext.SaveChanges();
                Console.WriteLine("Press any key when done: ");
                Console.ReadKey();
            }
        }
        public static void Logo() //Tyckte det skulle vara lite roligt med en logo så lekte lite med det också.
        {
            string logo = @"         
         _______
        |.-----.|  The
        ||     ||  School
        ||_____||  Of
        `--)-(--`  Programming
       __[=== o]___ -> T.S.O.P
      |:::::::::::|\ 
      `-=========-`()";
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(logo);
            Console.ResetColor();
        }
    }
}
