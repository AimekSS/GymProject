using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace CourseProject.Users
{
    internal class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsCoach { get; set; }
        public DateTime BirthData { get; set; }
        public Subscription userSubscription = new Subscription();
        public string Login { get; set; }
        [NonSerialized] public string Password;

        public int Id;


        [JsonConstructor]
        public User()
        {

        }

        public User(bool isCoach)
        {
            IsCoach = isCoach;
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Name = name;

            Console.Write("Surname: ");
            string surname = Console.ReadLine();
            Surname = surname;

            Console.Write(@"What is your birthdate (year-month-day)");
            Regex regex = new Regex(@"\d+");
            string birthdata = "";
            MatchCollection matchCollection = regex.Matches(Console.ReadLine());

            foreach (Match match in matchCollection)
            {
                birthdata += match.Value + "/";
            }

            if (birthdata.Length != 0)
            {
                birthdata = birthdata.Remove(birthdata.Length - 1, 1);
                BirthData = DateTime.Parse(birthdata);
            }

            Console.WriteLine("Login:");
            Login = Console.ReadLine();
        }

        public string VerifyLogin(string data)
        {
            while (data == "" || data.Length < 5 || data.Length > 12)
            {
                Console.WriteLine("The data you have typed is not applicable \nLogin should be 5-12 symbols long\nPlease try again");
                data = Console.ReadLine();
            }

            Console.WriteLine("\nType your login again for verification:");
            string verificationData = Console.ReadLine();

            while (verificationData != data)
            {
                Console.WriteLine("Error, data you have typed doesnt resemble the first occurence");
                Console.WriteLine("Please, enter the correct data");
                verificationData = Console.ReadLine();
            }

            Login = verificationData;

            return verificationData;
        }

        public string VerifyPassword(string data)
        {
            while (data == "")
            {
                Console.WriteLine("\nThe data you have typed is not applicable \nThe password should be of any length\nPlease type again");
                data = Console.ReadLine();
            }

            Console.WriteLine("Type again for verifying:");
            string verificationData = Console.ReadLine();
            while (verificationData != data)
            {
                Console.WriteLine("Error, data you have typed doesnt resemble the first one");
                Console.WriteLine("Please, enter the correct data");
                verificationData = Console.ReadLine();
            }

            Password = verificationData;

            return verificationData;
        }
    }
}