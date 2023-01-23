

using System;
using System.IO;
using System.Linq;

namespace consoleApp
{
    public class Program
    {
        public static void DisplayMenu()
        {
            Console.WriteLine("1.Login");
            Console.WriteLine("2.Registration");
            Console.WriteLine("3.Display current user information");
            Console.WriteLine("4.Quit");
        }
        public static string CreateUser()
        {
            Console.Write("Enter username: ");
            var userName = Console.ReadLine();
            Console.Write("Enter password: ");
            var password = Console.ReadLine();
            Console.Write("Enter birth date (mm-d-yyyy): ");
            var birthDate = DateTime.Parse(Console.ReadLine());// 04-12-2002
            Console.Write("Enter city: ");
            var city = Console.ReadLine();

            var userAge = (int)((DateTime.Now - birthDate).TotalDays / 365.242199);

            return
                $"UserName:{userName}@" +
                $"BirthDate:{birthDate}@" +
                $"City:{city}@" +
                $"Age:{userAge}@" +
                $"Password:{password}";
        }

        public static void AddUser(string userInfo, string filePath)
        {
            File.AppendAllText(filePath, userInfo + Environment.NewLine);
            Console.Clear();
            Console.WriteLine("User Added !");
        }
        public static void Main(string[] args)
        {
            const string FILE_PATH = @"C:\Users\Administrator\source\repos\ConsoleApp2\ConsoleApp2\TextFile1.txt";

            


            if (File.Exists(FILE_PATH) == false)
            {
                Console.WriteLine($"File doesn't exist at this path: {FILE_PATH}");
            }
            else
            {
                var users = File.ReadAllLines(FILE_PATH).ToList();


                var userChoice = -1;

                string currentUserInfo = "";

                while (userChoice != 4)
                {
                    DisplayMenu();

                    userChoice = int.Parse(Console.ReadLine());

                    // Login
                    if (userChoice == 1)
                    {
                        Console.Clear();
                        Console.Write("Enter username: ");
                        var enteredUserName = Console.ReadLine();
                        Console.Write("Enter password: ");
                        var enteredPassword = Console.ReadLine();

                        bool userExists = false;

                        foreach (var user in users)
                        {
                            // UserName:goga@BirthDate:10/3/1996 12:00:00 AM@City:tbilisi@Age:26@Password:goga1234

                            var userInfo = user.Split('@');// [UserName:goga,BirthDate:10/3/1996 12:00:00 AM,BirthDate:10/3/1996 12:00:00 AM..]
                            Console.WriteLine();
                            //First() -> UserName:goga
                            //Split(":") -> [UserName,goga]
                            //Last() -> goga
                            var userName = userInfo.First().Split(':').Last();//get username
                            var password = userInfo.Last().Split(':').Last();//get password

                            if (enteredUserName == userName
                                && enteredPassword == password)
                            {
                                currentUserInfo = user;
                                userExists = true;
                                Console.WriteLine("Logged In!");
                                break;
                            }


                        }

                        if (userExists == false)
                        {
                            Console.WriteLine("Incorrect credentials");
                        }

                    }
                    // Registration
                    else if (userChoice == 2)
                    {
                        Console.Clear();
                        var createdUser = CreateUser();

                        AddUser(createdUser, FILE_PATH);
                        users.Add(createdUser);
                    }
                    else if (userChoice == 3)
                    {
                        if (currentUserInfo == "")
                        {
                            Console.Clear();
                            Console.WriteLine("You're not logged in");
                        }
                        else
                        {

                            foreach (var user in users)
                            {
                                // UserName:goga@BirthDate:10/3/1996 12:00:00 AM@City:tbilisi@Age:26@Password:goga1234
                                if (user == currentUserInfo)
                                {
                                    Console.Clear();
                                    var userInfo = user.Split('@');
                                    var userName = userInfo[0];
                                    var birthDate = userInfo[1];
                                    var city = userInfo[2];
                                    var age = userInfo[3];

                                    Console.WriteLine(userName);
                                    Console.WriteLine(birthDate);
                                    Console.WriteLine(city);
                                    Console.WriteLine(age);
                                }



                            }

                        }


                    }
                    else if (userChoice == 4)
                    {
                        Console.Clear();
                        Console.WriteLine("Thank you for using our app!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid menu choice");
                    }

                }

            }
        }
    }
}









