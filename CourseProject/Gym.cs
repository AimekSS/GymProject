using CourseProject.HelpersAndConstants;
using CourseProject.MenuObjects;
using CourseProject.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
namespace CourseProject
{
    internal class Gym
    {
        public List<Visitor> visitorsData = new List<Visitor>();
        public List<Coach> coachesData = new List<Coach>();

        public Dictionary<string, string> visitorsCredentials = new Dictionary<string, string>();
        public Dictionary<string, string> coachesCredentials = new Dictionary<string, string>();

        public List<Inventory> cardioList = new List<Inventory>();
        public List<Inventory> freeWeightsList = new List<Inventory>();
        public List<Inventory> strengthTrainingList = new List<Inventory>();
        public List<Inventory> swedenWallsAndTurnsList = new List<Inventory>();
        public List<List<Inventory>> equipmentList = new List<List<Inventory>>();

        public void AddInventoryListToList()
        {
            equipmentList[0] = cardioList;
            equipmentList[1] = strengthTrainingList;
            equipmentList[2] = freeWeightsList;
            equipmentList[3] = swedenWallsAndTurnsList;

            string equipmentJson = JsonConvert.SerializeObject(equipmentList);

            File.WriteAllText(Constants.equipmentJsonPath, equipmentJson);
        }

        public void GetDataForEquipmentLists()
        {
            equipmentList = JsonHelper.CheckForEquipment(equipmentList, cardioList, strengthTrainingList, freeWeightsList, swedenWallsAndTurnsList);

            cardioList = equipmentList[0];
            strengthTrainingList = equipmentList[1];
            freeWeightsList = equipmentList[2];
            swedenWallsAndTurnsList = equipmentList[3];
        }
    }
}
