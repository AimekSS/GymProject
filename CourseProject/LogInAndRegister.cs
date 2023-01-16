using CourseProject.HelpersAndConstants;
using CourseProject.Users;
using Newtonsoft.Json;

namespace CourseProject
{
    internal class LogInAndRegisterService
    {
        public User Login(Gym gym)
        {
            Console.Write("Enter your login: ");
            string login = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            gym.visitorsCredentials = JsonHelper.CheckForVisitorCredentials(gym.visitorsCredentials, "There are no users yet in the system\nSorry, but you would have to register a user first");
            gym.coachesCredentials = JsonHelper.CheckForCoachesCredentials(gym.coachesCredentials, "There are no coaches yet in the system\nSorry");

            foreach (var coach in gym.coachesCredentials)
            {
                if (coach.Key == login && coach.Value == password)
                {
                    gym.coachesData = JsonHelper.GetCoachesData();

                    foreach (var myCoach in gym.coachesData)
                    {
                        if (login == myCoach.Login)
                        {
                            return myCoach;
                        }
                    }
                }
            }

            foreach (var visitor in gym.visitorsCredentials)
            {
                if (visitor.Key == login && visitor.Value == password)
                {
                    gym.visitorsData = JsonHelper.GetVisitorsData();

                    foreach (var myVisitor in gym.visitorsData)
                    {
                        if (login == myVisitor.Login)
                        {
                            return myVisitor;
                        }
                    }
                }
            }

            return null;
        }

        public User Register(Gym gym)
        {
            bool IsUserCoach;

            Console.WriteLine("If you are a coach, you could register a coach account directly");
            Console.WriteLine("1. Register a coach account (requires a secret key)");
            Console.WriteLine("2. Proceed with normal registration");

            string registrationPick = Console.ReadLine();
            if (registrationPick == "1")
            {
                for (; ; )
                {
                    Console.Clear();
                    Console.WriteLine("Please type a secret key");
                    string secretKey = Console.ReadLine();
                    if (secretKey != "1234")
                    {
                        Console.WriteLine("The key you have typed is incorrect\n1. Try again\n2. Go to visitor registration");
                        registrationPick = Console.ReadLine();
                        if (registrationPick == "1")
                        {

                        }
                        else if (registrationPick == "2")
                        {
                            IsUserCoach = false;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid choice");
                            Thread.Sleep(500);
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Creating a coach account");
                        IsUserCoach = true;
                        break;
                    }
                }
            }
            else
            {
                IsUserCoach = false;
            }

            if (IsUserCoach)
            {
                Coach newCoach = new Coach(IsUserCoach);
                gym.coachesData = JsonHelper.CheckForCoachesData(gym.coachesData, "No files to read coaches data from\nCongratulations you are our first coach!");

                if (gym.coachesData.Count == 0)
                {
                    newCoach.Id = 1;
                }
                else
                {
                    newCoach.Id = gym.coachesData.Count + 1;
                }

                newCoach.VerifyLogin(newCoach.Login);

                gym.visitorsCredentials=JsonHelper.CheckForVisitorCredentials(gym.visitorsCredentials, "No files to read credentials data from for visitors\nPlease create instances of one");
                gym.coachesCredentials=JsonHelper.CheckForCoachesCredentials(gym.coachesCredentials, "No files to read credentials data from for coach\nPlease create instances of one");

                for (; ; )
                {
                    if (gym.visitorsCredentials.ContainsKey(newCoach.Login) || gym.coachesCredentials.ContainsKey(newCoach.Login))
                    {
                        Console.WriteLine("The login you have typed is already taken\nPlease create a different login");
                        Console.WriteLine("New Login: ");
                        newCoach.VerifyLogin(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine("\nPassword:");
                newCoach.Password = Console.ReadLine();
                newCoach.VerifyPassword(newCoach.Password);
                Console.WriteLine("You have successfully created a new coach account");

                return newCoach;
            }
            else
            {
                Visitor newVisitor = new Visitor(IsUserCoach);
                gym.visitorsData = JsonHelper.CheckForVisitorsData(gym.visitorsData, "No files to read users data from\nCongratulations you are our first user!");

                if (gym.visitorsData.Count == 0)
                {
                    newVisitor.Id = 1;
                }
                else
                {
                    newVisitor.Id = gym.visitorsData.Count + 1;
                }

                newVisitor.VerifyLogin(newVisitor.Login);

                gym.visitorsCredentials=JsonHelper.CheckForVisitorCredentials(gym.visitorsCredentials, "No files to read credentials data from for visitors\nPlease create instances of one");
                gym.coachesCredentials=JsonHelper.CheckForCoachesCredentials(gym.coachesCredentials, "No files to read credentials data from for coach\nPlease create instances of one");

                for (; ; )
                {
                    if (gym.visitorsCredentials.ContainsKey(newVisitor.Login) || gym.coachesCredentials.ContainsKey(newVisitor.Login))
                    {
                        Console.WriteLine("The login you have typed is already taken\nPlease create a different login");
                        Console.WriteLine("New Login: ");
                        newVisitor.VerifyLogin(Console.ReadLine());
                    }
                    else
                    {
                        break;
                    }
                }

                Console.WriteLine("\nPassword:");
                newVisitor.Password = Console.ReadLine();
                newVisitor.VerifyPassword(newVisitor.Password);
                Console.WriteLine("You have successfully created a new user\nFree subscription is issued");

                return newVisitor;
            }
        }
    }
}