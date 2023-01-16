using ConsoleTables;
using CourseProject.Helpers;
using CourseProject.HelpersAndConstants;
using CourseProject.MenuObjects;
using CourseProject.Users;
using Newtonsoft.Json;

namespace CourseProject
{
    internal class MainMenuHelper
    {
        public static LogInAndRegisterService myLogInAndRegister = new LogInAndRegisterService();
        public static InventoryListHelper myInventoryList = new InventoryListHelper();
        public static Gym gym = new Gym();
        public static void DisplayMainScreen()
        {
            for (; ; )
            {
                Console.Clear();
                LogoHelper.DisplayMenuLogo();

                Console.WriteLine("\nWelcome to the Gym!");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Create an account");
                Console.WriteLine("3. Buy a subscription");
                Console.WriteLine("4. Quit");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        LoginProcedure();
                        break;

                    case "2":
                        Console.Clear();
                        User newUser = myLogInAndRegister.Register(gym);
                        JsonHelper.WriteJsonData(newUser, gym);

                        break;

                    case "3":
                        BuySubscriptionCall();
                        break;

                    case "4":
                        Environment.Exit(0);
                        break;

                    case "z":
                        SecretMenu();
                        break;

                    default:
                        Console.WriteLine("\nInvalid choice");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }

        public static void BuySubscriptionCall()
        {
            for (; ; )
            {
                Console.Clear();
                LogoHelper.DisplayMenuLogo();
                Console.WriteLine("To buy a subscription, you would have to log into your account");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Go back");

                string choice2 = Console.ReadLine();
                if (choice2 == "1")
                {
                    LoginProcedure();
                    break;
                }
                else if (choice2 == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice");
                    Thread.Sleep(500);
                }
            }
        }

        public static void PostLogInScreen(User userData)
        {
            Console.Clear();
            Console.WriteLine("\nWelcome, Visitor!");
            Thread.Sleep(1000);

            if (userData.IsCoach)
            {
                for (; ; )
                {
                    Console.Clear();
                    LogoHelper.DisplayCustomMenuLogo(userData);
                    Console.WriteLine("1. Inventory");
                    Console.WriteLine("2. Add inventory");
                    Console.WriteLine("3. Remove inventory");
                    Console.WriteLine("4. Group inventory");
                    Console.WriteLine("5. Visitors");
                    Console.WriteLine("6. Log out");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            myInventoryList.ShowInventoryItems("null", gym);
                            break;

                        case "2":
                            myInventoryList.AddInventoryItemToList(gym);
                            break;

                        case "3":
                            myInventoryList.RemoveEquipmentItemFromjson(gym);
                            break;

                        case "4":
                            myInventoryList.GroupingByCriterias(gym);
                            break;

                        case "5":
                            string jsonVisitorData = File.ReadAllText(Constants.visitorsJsonPath);
                            gym.visitorsData = JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);
                            List<Visitor> coachVisitorList = new List<Visitor>();

                            var myTable = new ConsoleTable("ID", "Name", "Surname", "Year of birth", "Subscription Validity");

                            for (; ; )
                            {
                                myTable.Rows.Clear();
                                coachVisitorList.Clear();
                                foreach (var v in gym.visitorsData)
                                {
                                    if (v.userSubscription.userCoachName == userData.Name)
                                    {
                                        coachVisitorList.Add(v);
                                    }
                                }

                                foreach (var d in coachVisitorList)
                                {
                                    myTable.AddRow(d.Id, d.Name, d.Surname, d.BirthData, d.userSubscription.seasonTicketValidity);
                                }

                                myTable.Write();
                                Console.WriteLine();
                                Console.WriteLine("Would you like to delete the visitor account?");
                                Console.WriteLine("1. Yes\n2. No");
                                string deleteAccountPick = Console.ReadLine();
                                if (deleteAccountPick == "1")
                                {
                                    while (true)
                                    {
                                        int visitorPick;
                                        Console.WriteLine("Here are the visitors you can delete:");
                                        foreach (Visitor visitor in gym.visitorsData)
                                        {
                                            Console.WriteLine($"{visitor.Id}. {visitor.Name} {visitor.Surname}");
                                        }

                                        for (; ; )
                                        {
                                            Console.WriteLine("\nWhich visitor would you like to delete (pick a number)");
                                            Console.WriteLine("If you would like to go back press 0");
                                            try
                                            {
                                                visitorPick = Convert.ToInt32(Console.ReadLine());
                                                break;
                                            }
                                            catch (Exception)
                                            {
                                                Console.WriteLine("Invalid pick, please try again");
                                            }
                                        }
                                        if (visitorPick == 0)
                                        {
                                            break;
                                        }

                                        string loginToDelete = gym.visitorsData[visitorPick - 1].Login;
                                        var newVisitorsData = gym.visitorsData.Where(x => x.Id == visitorPick);
                                        if (newVisitorsData.First().userSubscription.userCoachName == userData.Name)
                                        {
                                            if (DateTime.Compare(newVisitorsData.First().userSubscription.seasonTicketValidity, DateTime.Now) > 0)
                                            {
                                                Console.WriteLine($"The visitor's ID ({newVisitorsData.First().Id}) subscription is still valid\nuntil: {newVisitorsData.First().userSubscription.seasonTicketValidity}");
                                                Console.WriteLine("Press any key to continue");
                                                Console.ReadKey();
                                                break;
                                            }
                                            else if (newVisitorsData.First().Id == visitorPick)
                                            {
                                                gym.visitorsData.Remove(gym.visitorsData[visitorPick - 1]);
                                                if (visitorPick <= gym.visitorsData.Count)
                                                {
                                                    for (int i = visitorPick - 1; i < gym.visitorsData.Count; i++)
                                                    {
                                                        gym.visitorsData[i].Id = i + 1;
                                                    }
                                                }
                                                string visitorJson = JsonConvert.SerializeObject(gym.visitorsData);
                                                File.WriteAllText(Constants.visitorsJsonPath, visitorJson);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Sorry, the user you have picked does not exist");
                                            }

                                            string jsonVisitorCredentialsData = File.ReadAllText(Constants.visitorsCredentialsJsonPath);
                                            gym.visitorsCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
                                            foreach (var visitor in gym.visitorsCredentials)
                                            {
                                                if (visitor.Key == loginToDelete)
                                                {
                                                    gym.visitorsCredentials.Remove(visitor.Key);
                                                    string visitorsCredentialsJson = JsonConvert.SerializeObject(gym.visitorsCredentials);
                                                    File.WriteAllText(Constants.visitorsCredentialsJsonPath, visitorsCredentialsJson);
                                                }
                                            }

                                            Console.WriteLine("You have successfully deleted a visitor account");
                                            Console.ReadKey();
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("This user cannot be deleted");
                                            Console.ReadKey();
                                            break;
                                        }
                                    }
                                }
                                else if (deleteAccountPick == "2")
                                {
                                    Console.WriteLine("Going back");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid option, going to visitors menu");
                                    Thread.Sleep(1000);
                                }
                            }
                            break;

                        case "6":
                            goto exitLoop;

                        default:
                            Console.WriteLine("\nInvalid choice");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            exitLoop:;

                gym.coachesData[userData.Id - 1] = (Coach)userData;
                string coachJson = JsonConvert.SerializeObject(gym.coachesData);
                File.WriteAllText(Constants.coachesJsonPath, coachJson);
            }
            else
            {
                gym.GetDataForEquipmentLists();

                for (; ; )
                {
                    ConsoleTable inventoryTable = new ConsoleTable("ID", "Name", "Quantity");
                    Console.Clear();
                    LogoHelper.DisplayCustomMenuLogo(userData);
                    Console.WriteLine("1. Buy a subscription");
                    Console.WriteLine("2. Pick a coach");
                    Console.WriteLine("3. Inventory");
                    Console.WriteLine("4. Change season ticket");
                    Console.WriteLine("5. Log out");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                        case "2":
                            if (choice == "1")
                            {
                                ProlongueSubscription(userData);

                                string subscriptionPick = userData.userSubscription.SubcriptionType();

                                if (subscriptionPick != "free")
                                {
                                    Console.WriteLine($"Thank you {userData.Name}, you have purchased a subscription " +
                                    $"for {userData.userSubscription.NumberOfDays(userData.userSubscription.seasonTicketTerm.ToString())} days." +
                                    $"The subscription would be valid until {userData.userSubscription.seasonTicketValidity}");
                                }
                                else
                                {
                                    Console.WriteLine("You can come back at any time to purchase the subscription plan" +
                                        "\nYour current plan is: " + subscriptionPick);
                                }

                                Console.WriteLine("\nType anything to continue");
                                Console.ReadLine();
                            }
                            else if (choice == "2")
                            {
                                ProlongueSubscription(userData);

                                if (userData.userSubscription.userCoachName != null)
                                {
                                    Console.WriteLine("You already have a coach: " + userData.userSubscription.userCoachName);
                                    for (; ; )
                                    {
                                        Console.WriteLine("Would you like to change your coach?\n1.Yes\n2.No, go back");
                                        string userCoachChangePick = Console.ReadLine();
                                        if (userCoachChangePick == "1")
                                        {
                                            userData.userSubscription.PickingCoach(gym);
                                            break;
                                        }
                                        else if (userCoachChangePick == "2")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Invalid pick, please try again");
                                            Thread.Sleep(1000);
                                        }
                                    }

                                }
                                else
                                {
                                    userData.userSubscription.PickingCoach(gym);
                                    Console.WriteLine($"Your coach is now: {userData.userSubscription.userCoachName}");
                                    Console.WriteLine("\nType anything to continue");
                                    Console.ReadKey();
                                }
                            }
                            break;

                        case "3":
                            ProlongueSubscription(userData);

                            if (userData.userSubscription.subscriptionTerm == Subscription.SubscriptionTerm.Free)
                            {
                                foreach (var c in gym.cardioList)
                                {
                                    inventoryTable.AddRow(c.ID, c.Name, c.Quantity);
                                }
                                inventoryTable.Write();
                                Console.ReadKey();
                            }
                            else if (userData.userSubscription.subscriptionTerm == Subscription.SubscriptionTerm.OneMonth)
                            {
                                foreach (var c in gym.cardioList)
                                {
                                    inventoryTable.AddRow(c.ID, c.Name, c.Quantity);
                                }
                                foreach (var swat in gym.swedenWallsAndTurnsList)
                                {
                                    inventoryTable.AddRow(swat.ID, swat.Name, swat.Quantity);
                                }
                                inventoryTable.Write();
                                Console.ReadKey();
                            }
                            else if (userData.userSubscription.subscriptionTerm == Subscription.SubscriptionTerm.SixMonth)
                            {
                                foreach (var c in gym.cardioList)
                                {
                                    inventoryTable.AddRow(c.ID, c.Name, c.Quantity);
                                }
                                foreach (var swat in gym.swedenWallsAndTurnsList)
                                {
                                    inventoryTable.AddRow(swat.ID, swat.Name, swat.Quantity);
                                }
                                foreach (var fw in gym.freeWeightsList)
                                {
                                    inventoryTable.AddRow(fw.ID, fw.Name, fw.Quantity);
                                }
                                inventoryTable.Write();
                                Console.ReadKey();
                            }
                            else if (userData.userSubscription.subscriptionTerm == Subscription.SubscriptionTerm.Year)
                            {
                                foreach (var e in gym.equipmentList)
                                {
                                    foreach (var i in e)
                                    {
                                        inventoryTable.AddRow(i.ID, i.Name, i.Quantity);
                                    }
                                }
                                inventoryTable.Write();
                                Console.ReadKey();
                            }
                            break;

                        case "4":
                            ProlongueSubscription(userData);

                            for (; ; )
                            {
                                Console.Clear();
                                Console.WriteLine("The date of validity of current subscription is: " + userData.userSubscription.seasonTicketValidity);
                                Console.WriteLine("Would you like to update or cancel the subscription\n1. Update\n2. Cancel\n3. Go back");
                                string userUpdateCancelPick = Console.ReadLine();

                                if (userUpdateCancelPick == "1")
                                {
                                    string subscriptionPick = userData.userSubscription.SubcriptionType();

                                    if (subscriptionPick != "free")
                                    {
                                        Console.WriteLine($"Thank you {userData.Name}, you have purchased a subscription " +
                                        $"for {userData.userSubscription.NumberOfDays(userData.userSubscription.seasonTicketTerm.ToString())} days." +
                                        $"The subscription would be valid until {userData.userSubscription.seasonTicketValidity}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("You can come back at any time to purchase the subscription plan" +
                                            "\nYour current plan is: " + subscriptionPick);
                                    }
                                    break;
                                }
                                else if (userUpdateCancelPick == "2")
                                {
                                    Console.WriteLine("Your subsciption has been deleted successfully\nType something to continue");
                                    string usersCoach = userData.userSubscription.userCoachName;
                                    userData.userSubscription = new Subscription();
                                    userData.userSubscription.userCoachName = usersCoach;
                                    Console.ReadKey();
                                    break;
                                }
                                else if (userUpdateCancelPick == "3")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid pick, please try again");
                                    Thread.Sleep(1000);
                                }
                            }
                            break;

                        case "5":
                            goto exitLoop;

                        default:
                            Console.WriteLine("\nInvalid choice");
                            Thread.Sleep(1000);
                            break;
                    }
                }
            exitLoop:;
                gym.visitorsData[userData.Id - 1] = (Visitor)userData;
                string visitorJson = JsonConvert.SerializeObject(gym.visitorsData);
                File.WriteAllText(Constants.visitorsJsonPath, visitorJson);
            }
        }

        public static void ProlongueSubscription(User user)
        {
            if (DateTime.Compare(user.userSubscription.seasonTicketValidity, DateTime.Now) < 0)
            {
                Console.WriteLine("\nYour subscription has expired\nPlease, extend your subscription\n \nType to continue");
                Console.ReadKey();
            }
        }

        public static void LoginProcedure()
        {
            Console.Clear();

            while (true)
            {
                User user = myLogInAndRegister.Login(gym);
                if (user == null)
                {
                    Console.WriteLine("\nLogin or password is incorrect");
                    Console.WriteLine("Would you like to register a new user?\n1. Yes\n2. No (Try to login again)\n3. Go back to main menu");
                    string userInput = Console.ReadLine();

                    if (userInput == "1")
                    {
                        Console.Clear();
                        User newUser = myLogInAndRegister.Register(gym);
                        JsonHelper.WriteJsonData(newUser, gym);
                        break;
                    }
                    else if (userInput == "2")
                    {
                        Console.Clear();
                    }
                    else if (userInput == "3")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice");
                        Thread.Sleep(500);
                    }
                }

                else if (user.IsCoach || !user.IsCoach)
                {
                    PostLogInScreen(user);
                    break;
                }
            }
        }

        public static void SecretMenu()
        {
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("Hi dev!");
                Console.WriteLine("Here are your options");
                Console.WriteLine("1. Delete visitors");
                Console.WriteLine("2. Delete coaches");
                Console.WriteLine("3. Go Back");
                string devInput = Console.ReadLine();

                if (devInput == "3")
                {
                    break;
                }

                for (; ; )
                {
                    if (devInput == "1")
                    {
                        DevUserDeleter(devInput);
                        break;
                    }
                    else if (devInput == "2")
                    {
                        DevUserDeleter(devInput);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid choice");
                        Thread.Sleep(500);
                        break;
                    }
                }
            }
        }

        public static void DevUserDeleter(string input)
        {
            if (input == "1")
            {
                while (true)
                {
                    string jsonVisitorData = File.ReadAllText(Constants.visitorsJsonPath);
                    gym.visitorsData = JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);

                    Console.WriteLine("Here are the visitors you can delete:");
                    foreach (Visitor visitor in gym.visitorsData)
                    {
                        Console.WriteLine($"{visitor.Id}. {visitor.Name} {visitor.Surname}");
                    }

                    Console.WriteLine("\nWhich visitor would you like to delete (pick a number)");
                    Console.WriteLine("If you would like to go back press 0");

                    int visitorPick = Convert.ToInt32(Console.ReadLine());
                    if (visitorPick == 0)
                    {
                        break;
                    }

                    string loginToDelete = gym.visitorsData[visitorPick - 1].Login;
                    var newVisitorsData = gym.visitorsData.Where(x => x.Id == visitorPick);
                    if (newVisitorsData.First().Id == visitorPick)
                    {
                        gym.visitorsData.Remove(gym.visitorsData[visitorPick - 1]);
                        if (visitorPick <= gym.visitorsData.Count)
                        {
                            for (int i = visitorPick - 1; i < gym.visitorsData.Count; i++)
                            {
                                gym.visitorsData[i].Id = i + 1;
                            }
                        }
                        string visitorJson = JsonConvert.SerializeObject(gym.visitorsData);
                        File.WriteAllText(Constants.visitorsJsonPath, visitorJson);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, the user you have picked does not exist");
                    }

                    string jsonVisitorCredentialsData = File.ReadAllText(Constants.visitorsCredentialsJsonPath);
                    gym.visitorsCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
                    foreach (var visitor in gym.visitorsCredentials)
                    {
                        if (visitor.Key == loginToDelete)
                        {
                            gym.visitorsCredentials.Remove(visitor.Key);
                            string visitorsCredentialsJson = JsonConvert.SerializeObject(gym.visitorsCredentials);
                            File.WriteAllText(Constants.visitorsCredentialsJsonPath, visitorsCredentialsJson);
                        }
                    }

                    Console.WriteLine("You have successfully deleted a visitor account");
                    Thread.Sleep(1000);
                    break;
                }

            }
            else if (input == "2")
            {
                while (true)
                {
                    string jsonCoachData = File.ReadAllText(Constants.coachesJsonPath);
                    gym.coachesData = JsonConvert.DeserializeObject<List<Coach>>(jsonCoachData);

                    Console.WriteLine("Here are the coaches you can delete:");
                    foreach (Coach coach in gym.coachesData)
                    {
                        Console.WriteLine($"{coach.Id}. {coach.Name} {coach.Surname}");
                    }

                    Console.WriteLine("\nWhich coach would you like to delete (pick a number)");
                    Console.WriteLine("If you would like to go back press 0");

                    int coachPick = Convert.ToInt32(Console.ReadLine());
                    if (coachPick == 0)
                    {
                        break;
                    }

                    string loginToDelete = gym.coachesData[coachPick - 1].Login;
                    var newCoachesData = gym.coachesData.Where(x => x.Id == coachPick);
                    if (newCoachesData.First().Id == coachPick)
                    {
                        gym.coachesData.Remove(gym.coachesData[coachPick - 1]);
                        if (coachPick <= gym.coachesData.Count)
                        {
                            for (int i = coachPick - 1; i < gym.coachesData.Count; i++)
                            {
                                gym.coachesData[i].Id = i + 1;
                            }
                        }
                        string coachJson = JsonConvert.SerializeObject(gym.coachesData);
                        File.WriteAllText(Constants.coachesJsonPath, coachJson);
                    }
                    else
                    {
                        Console.WriteLine("Sorry, the user you have picked does not exist");
                    }

                    string jsonCoachCredentialsData = File.ReadAllText(Constants.coachesCredentialsJsonPath);
                    gym.coachesCredentials = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCoachCredentialsData);
                    foreach (var coach in gym.coachesCredentials)
                    {
                        if (coach.Key == loginToDelete)
                        {
                            gym.coachesCredentials.Remove(coach.Key);
                            string coachesCredentialsJson = JsonConvert.SerializeObject(gym.coachesCredentials);
                            File.WriteAllText(Constants.coachesCredentialsJsonPath, coachesCredentialsJson);
                        }
                    }

                    Console.WriteLine("You have successfully deleted a coach account");
                    Thread.Sleep(1000);
                    break;
                }
            }
        }
    }
}