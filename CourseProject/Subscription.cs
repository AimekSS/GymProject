using CourseProject.HelpersAndConstants;
using CourseProject.Users;
using Newtonsoft.Json;
using System.Numerics;
using System.Text.RegularExpressions;

namespace CourseProject
{
    internal class Subscription
    {
        public TimeSpan seasonTicketTerm;
        public DateTime seasonTicketValidity;
        public string subscriptionType { get; set; } = "free";
        public Coach userCoach;
        public string userCoachName;
        public SubscriptionTerm subscriptionTerm = SubscriptionTerm.Free;
        public enum SubscriptionTerm
        {
            Free,
            OneMonth,
            SixMonth,
            Year
        }

        public string SubcriptionType()
        {
            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("\nPlease pick one of the options");
                Console.WriteLine("1. 1 month subscription");
                Console.WriteLine("2. 6 month subscription");
                Console.WriteLine("3. 12 month subscription");
                Console.WriteLine("4. Go back");


                string subscriptionPick = Console.ReadLine();
                DateTime dateOfSubscriptionPurchase = DateTime.Now;

                if (subscriptionPick == "1")
                {
                    subscriptionType = "1 month";
                    subscriptionTerm = SubscriptionTerm.OneMonth;
                    seasonTicketValidity = dateOfSubscriptionPurchase.AddMonths(1);
                    seasonTicketTerm = seasonTicketValidity.Subtract(dateOfSubscriptionPurchase);
                    break;
                }
                else if (subscriptionPick == "2")
                {
                    subscriptionType = "6 month";
                    subscriptionTerm = SubscriptionTerm.SixMonth;
                    seasonTicketValidity = dateOfSubscriptionPurchase.AddMonths(6);
                    seasonTicketTerm = seasonTicketValidity.Subtract(dateOfSubscriptionPurchase);
                    break;
                }
                else if (subscriptionPick == "3")
                {
                    subscriptionType = "12 month";
                    subscriptionTerm = SubscriptionTerm.Year;
                    seasonTicketValidity = dateOfSubscriptionPurchase.AddMonths(12);
                    seasonTicketTerm = seasonTicketValidity.Subtract(dateOfSubscriptionPurchase);
                    break;
                }
                else if (subscriptionPick == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please try again");
                }
            }

            return subscriptionType;
        }

        public void PickingCoach(Gym gym)
        {
            gym.coachesData = JsonHelper.GetCoachesData();
            Console.WriteLine("Here are the coaches available in out gym:");
            foreach (Coach coach in gym.coachesData)
            {
                Console.WriteLine($"{coach.Id}. {coach.Name} {coach.Surname}");
            }

            for (; ; )
            {
                Console.WriteLine("Which coach would you like to be training with (pick a number)");

                try
                {
                    int coachPick = Convert.ToInt32(Console.ReadLine());
                    var newCoachesData = gym.coachesData.Where(x => x.Id == coachPick);
                    if (newCoachesData.First().Id == coachPick)
                    {
                        userCoachName = newCoachesData.First().Name;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Sorry, gigabug");
                        break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invald input, please try again");
                    Thread.Sleep(1000);
                }
            }
        }

        public Match NumberOfDays(string ticketTerm)
        {
            Regex regex = new Regex(@"\d+");
            Match match = regex.Match(ticketTerm);

            return match;
        }
    }
}