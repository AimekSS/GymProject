using CourseProject.Users;
using Newtonsoft.Json;

namespace CourseProject
{
    internal class LogInAndRegister
    {
        public List<Visitor> visitorsData = new List<Visitor>();
        public List<Coach> coachesData = new List<Coach>();

        public Dictionary<string, string> visitorsCredentials = new Dictionary<string, string>();
        public Dictionary<string, string> coachesCredentials = new Dictionary<string, string>();

        public static string folderName = "MyJson";

        public static string visitorsFileName = "visitors.json";
        public static string coachesFileName = "coaches.json";

        public static string visitorsCredentialsFileName = "visitorsCredentials.json";
        public static string coachesCredentialsFileName = "coachesCredentials.json";

        public string visitorsJsonPath = GetFilePath(folderName, visitorsFileName);
        public string coachesJsonPath = GetFilePath(folderName, coachesFileName);
        public string visitorsCredentialsJsonPath = GetFilePath(folderName, visitorsCredentialsFileName);
        public string coachesCredentialsJsonPath = GetFilePath(folderName, coachesCredentialsFileName);

        public User Login()
        {
            Console.Write("Enter your login: ");
            string login = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            try
            {
                string jsonVisitorCredentialsData = File.ReadAllText(visitorsCredentialsJsonPath);
                visitorsCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("There are no users yet in the system\nSorry, but you would have to register a user first");
            }

            try
            {
                string jsonCoachCredentialsData = File.ReadAllText(coachesCredentialsJsonPath);
                coachesCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCoachCredentialsData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("There are no coaches yet in the system\nSorry");
            }

            foreach (var coach in coachesCredentials)
            {
                if (coach.Key == login && coach.Value == password)
                {
                    string jsonCoachData = File.ReadAllText(coachesJsonPath);
                    coachesData = JsonConvert.DeserializeObject<List<Coach>>(jsonCoachData);

                    foreach (var myCoach in coachesData)
                    {
                        if (login == myCoach.Login)
                        {
                            return myCoach;
                        }
                    }
                }
            }

            foreach (var visitor in visitorsCredentials)
            {
                if (visitor.Key == login && visitor.Value == password)
                {
                    string jsonVisitorData = File.ReadAllText(visitorsJsonPath);
                    visitorsData = JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);

                    foreach (var myVisitor in visitorsData)
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

        public User Register()
        {
            bool userIsACoach;

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
                            userIsACoach = false;
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
                        userIsACoach = true;
                        break;
                    }
                }
            }
            else
            {
                userIsACoach = false;
            }

            if (userIsACoach)
            {
                Coach newCoach = new Coach(userIsACoach);

                try
                {
                    string jsonCoachData = File.ReadAllText(coachesJsonPath);
                    coachesData = JsonConvert.DeserializeObject<List<Coach>>(jsonCoachData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read coaches data from\nCongratulations you are our first coach!");
                }

                if (coachesData.Count == 0)
                {
                    newCoach.Id = 1;
                }
                else
                {
                    newCoach.Id = coachesData.Count + 1;
                }

                newCoach.VerifyLogin(newCoach.GetLoginAndPassword()[0]);

                string[] userCredentials = newCoach.GetLoginAndPassword();

                try
                {
                    string jsonVisitorCredentialsData = File.ReadAllText(visitorsCredentialsJsonPath);
                    visitorsCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read credentials data from for visitors\nPlease create instances of one");
                }

                try
                {
                    string jsonCoachCredentialsData = File.ReadAllText(coachesCredentialsJsonPath);
                    coachesCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCoachCredentialsData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read credentials data from for coach\nPlease create instances of one");
                }

                for (; ; )
                {
                    if (visitorsCredentials.ContainsKey(userCredentials[0]) || coachesCredentials.ContainsKey(userCredentials[0]))
                    {
                        Console.WriteLine("The login you have typed is already taken\nPlease create a different login");
                        Console.WriteLine("New Login: ");
                        newCoach.VerifyLogin(Console.ReadLine());
                        userCredentials = newCoach.GetLoginAndPassword();
                    }
                    else
                    {
                        break;
                    }
                }

                coachesData.Add(newCoach);
                string coachJson = JsonConvert.SerializeObject(coachesData);
                File.WriteAllText(coachesJsonPath, coachJson);

                Console.WriteLine("\nPassword:");
                userCredentials[1] = Console.ReadLine();
                newCoach.VerifyPassword(userCredentials[1]);
                coachesCredentials.Add(userCredentials[0], userCredentials[1]);
                string coachesCredentialsJson = JsonConvert.SerializeObject(coachesCredentials);

                File.WriteAllText(coachesCredentialsJsonPath, coachesCredentialsJson);
                Console.WriteLine("You have successfully created a new coach account");

                return newCoach;
            }
            else
            {
                Visitor newVisitor = new Visitor(userIsACoach);

                try
                {
                    string jsonVisitorData = File.ReadAllText(visitorsJsonPath);
                    visitorsData = JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read users data from\nCongratulations you are our first user!");
                }

                if (visitorsData.Count == 0)
                {
                    newVisitor.Id = 1;
                }
                else
                {
                    newVisitor.Id = visitorsData.Count + 1;
                }

                newVisitor.VerifyLogin(newVisitor.GetLoginAndPassword()[0]);

                string[] userCredentials = newVisitor.GetLoginAndPassword();

                try
                {
                    string jsonVisitorCredentialsData = File.ReadAllText(visitorsCredentialsJsonPath);
                    visitorsCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read credentials data from for visitors\nPlease create instances of one");
                }

                try
                {
                    string jsonCoachCredentialsData = File.ReadAllText(coachesCredentialsJsonPath);
                    coachesCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCoachCredentialsData);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("No files to read credentials data from for coach\nPlease create instances of one");
                }

                for (; ; )
                {
                    if (visitorsCredentials.ContainsKey(userCredentials[0]) || coachesCredentials.ContainsKey(userCredentials[0]))
                    {
                        Console.WriteLine("The login you have typed is already taken\nPlease create a different login");
                        Console.WriteLine("New Login: ");
                        newVisitor.VerifyLogin(Console.ReadLine());
                        userCredentials = newVisitor.GetLoginAndPassword();
                    }
                    else
                    {
                        break;
                    }
                }

                visitorsData.Add(newVisitor);
                string visitorJson = JsonConvert.SerializeObject(visitorsData);
                File.WriteAllText(visitorsJsonPath, visitorJson);

                Console.WriteLine("\nPassword:");
                userCredentials[1] = Console.ReadLine();
                newVisitor.VerifyPassword(userCredentials[1]);
                visitorsCredentials.Add(userCredentials[0], userCredentials[1]);
                string visitorsCredentialsJson = JsonConvert.SerializeObject(visitorsCredentials);

                File.WriteAllText(visitorsCredentialsJsonPath, visitorsCredentialsJson);
                Console.WriteLine("You have successfully created a new user\nFree subscription is issued");

                return newVisitor;
            }
        }

        public static string GetFilePath(string folderName, string fileName)
        {
            string path = Path.Combine($"..\\..\\..\\{folderName}", fileName);

            return path;
        }
    }
}