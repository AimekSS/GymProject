using CourseProject.MenuObjects;
using CourseProject.Users;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace CourseProject.HelpersAndConstants
{
    internal class JsonHelper
    {
        public static void WriteJsonData(User newUser, Gym gym)
        {
            if (newUser.IsCoach)
            {
                gym.coachesData.Add((Coach)newUser);
                string coachJson = JsonConvert.SerializeObject(gym.coachesData);
                File.WriteAllText(Constants.coachesJsonPath, coachJson);

                gym.coachesCredentials.Add(newUser.Login, newUser.Password);
                string coachesCredentialsJson = JsonConvert.SerializeObject(gym.coachesCredentials);
                File.WriteAllText(Constants.coachesCredentialsJsonPath, coachesCredentialsJson);
            }
            else
            {
                gym.visitorsData.Add((Visitor)newUser);
                string visitorJson = JsonConvert.SerializeObject(gym.visitorsData);
                File.WriteAllText(Constants.visitorsJsonPath, visitorJson);

                gym.visitorsCredentials.Add(newUser.Login, newUser.Password);
                string visitorsCredentialsJson = JsonConvert.SerializeObject(gym.visitorsCredentials);
                File.WriteAllText(Constants.visitorsCredentialsJsonPath, visitorsCredentialsJson);
            }
        }
        public static Dictionary<string, string> CheckForVisitorCredentials(Dictionary<string, string> VisitorC, string msg)
        {
            try
            {
                string jsonVisitorCredentialsData = File.ReadAllText(Constants.visitorsCredentialsJsonPath);
                VisitorC = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonVisitorCredentialsData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(msg);
            }

            return VisitorC;
        }

        public static List<List<Inventory>> CheckForEquipment(List<List<Inventory>> Equipment, List<Inventory> Cardio, List<Inventory> StrenghtTraining, List<Inventory> FreeWeights, List<Inventory> SwedenWallsAndTurns)
        {
            try
            {
                string jsonEquipmentList = File.ReadAllText(Constants.equipmentJsonPath);
                Equipment = JsonConvert.DeserializeObject<List<List<Inventory>>>(jsonEquipmentList);
            }
            catch (FileNotFoundException)
            {
                Equipment.Add(Cardio);
                Equipment.Add(StrenghtTraining);
                Equipment.Add(FreeWeights);
                Equipment.Add(SwedenWallsAndTurns);

                string equipmentJson = JsonConvert.SerializeObject(Equipment);
                File.WriteAllText(Constants.equipmentJsonPath, equipmentJson);
            }
            return Equipment;
        }

        public static Dictionary<string, string> CheckForCoachesCredentials(Dictionary<string, string> CoachC, string msg)
        {
            try
            {
                string jsonCoachCredentialsData = File.ReadAllText(Constants.coachesCredentialsJsonPath);
                CoachC = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonCoachCredentialsData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(msg);
            }

            return CoachC;
        }

        public static List<Coach> CheckForCoachesData(List<Coach> CoachD, string msg)
        {
            try
            {
                string jsonCoachData = File.ReadAllText(Constants.coachesJsonPath);
                CoachD = JsonConvert.DeserializeObject<List<Coach>>(jsonCoachData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(msg);
            }

            return CoachD;
        }

        public static List<Visitor> CheckForVisitorsData(List<Visitor> VisitorD, string msg)
        {
            try
            {
                string jsonVisitorData = File.ReadAllText(Constants.visitorsJsonPath);
                VisitorD = JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(msg);
            }

            return VisitorD;
        }

        public static List<Coach> GetCoachesData()
        {
            string jsonCoachData = File.ReadAllText(Constants.coachesJsonPath);
            return JsonConvert.DeserializeObject<List<Coach>>(jsonCoachData);
        }

        public static List<Visitor> GetVisitorsData()
        {
            string jsonVisitorData = File.ReadAllText(Constants.visitorsJsonPath);
            return JsonConvert.DeserializeObject<List<Visitor>>(jsonVisitorData);
        }
    }
}
