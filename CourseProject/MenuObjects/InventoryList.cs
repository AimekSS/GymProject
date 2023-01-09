using ConsoleTables;
using Newtonsoft.Json;

namespace CourseProject.MenuObjects
{
    internal class InventoryList
    {
        public List<Inventory> cardioList = new List<Inventory>();
        public List<Inventory> freeWeightsList = new List<Inventory>();
        public List<Inventory> strengthTrainingList = new List<Inventory>();
        public List<Inventory> swedenWallsAndTurnsList = new List<Inventory>();
        public List<List<Inventory>> equipmentList = new List<List<Inventory>>();

        public static string folderName = "MyJson";
        public static string equipmentFileName = "equipment.json";
        public string equipmentJsonPath = GetFilePath(folderName, equipmentFileName);
        public ConsoleTable table;

        public void GroupingByCriterias()
        {
            ShowInventory("null");
            Console.WriteLine("By which criteria would you like to sort?");
            Console.WriteLine("1. ID\n2. Name\n3. Quantity");

            string userPick = Console.ReadLine();
            List<Inventory> mergedList;
            table = new ConsoleTable("Id", "Category", "Name", "Quantity");
            int c = cardioList.Count;
            int st = strengthTrainingList.Count;
            int fw = freeWeightsList.Count;
            int swat = swedenWallsAndTurnsList.Count;

            for (; ; )
            {
                table.Rows.Clear();
                if (userPick == "1")
                {
                    while (true)
                    {
                        foreach (var i in equipmentList)
                        {
                            try
                            {
                                var first = i.First();
                                table.AddRow(first.ID, first.Category, first.Name, Convert.ToString(first.Quantity));
                                i.Remove(first);
                            }
                            catch (InvalidOperationException)
                            {

                            }
                        }
                        c--;
                        st--;
                        fw--;
                        swat--;

                        if (c <= 0 && st <= 0 && fw <= 0 && swat <= 0)
                        {
                            break;
                        }
                    }

                    table.Write();
                    Console.ReadKey();
                    break;
                }
                else if (userPick == "2")
                {
                    mergedList = new List<Inventory>();

                    foreach (var list in equipmentList)
                    {
                        mergedList.AddRange(list);
                    }

                    var newMerge = mergedList.OrderBy(x => x.Name).ToList();
                    foreach (var i in newMerge)
                    {
                        table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                    }

                    table.Write();
                    Console.ReadKey();
                    break;
                }
                else if (userPick == "3")
                {
                    mergedList = new List<Inventory>();

                    foreach (var list in equipmentList)
                    {
                        mergedList.AddRange(list);
                    }

                    var newMerge = mergedList.OrderBy(x => x.Quantity).ToList();
                    foreach (var i in newMerge)
                    {
                        table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                    }

                    table.Write();
                    Console.ReadKey();
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid pick, please try again");
                    Console.ReadKey();
                    break;
                }
            }
        }

        public static string GetFilePath(string folderName, string fileName)
        {
            string path = Path.Combine($"..\\..\\..\\{folderName}", fileName);

            return path;
        }

        public string ShowInventory(string s)
        {
            GetJsonData();
            Console.WriteLine();
            table = new ConsoleTable("Id", "Category", "Name", "Quantity");

            for (; ; )
            {
                for (; ; )
                {
                    if (s == "null")
                    {
                        foreach (var e in equipmentList)
                        {
                            foreach (var i in e)
                            {
                                table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                            }
                        }
                        table.Write();
                        Console.WriteLine();
                        Console.ReadKey();
                        break;
                    }
                    else if (s == "1")
                    {
                        foreach (var i in equipmentList[0])
                        {
                            table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                        }
                        table.Write();
                        Console.WriteLine();
                        Console.ReadLine();
                        break;
                    }
                    else if (s == "2")
                    {
                        foreach (var i in equipmentList[1])
                        {
                            table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                        }
                        table.Write();
                        Console.WriteLine();
                        Console.ReadLine();
                        break;
                    }
                    else if (s == "3")
                    {
                        foreach (var i in equipmentList[2])
                        {
                            table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                        }
                        table.Write();
                        Console.WriteLine();
                        Console.ReadLine();
                        break;
                    }
                    else if (s == "4")
                    {
                        foreach (var i in equipmentList[3])
                        {
                            table.AddRow(i.ID, i.Category, i.Name, Convert.ToString(i.Quantity));
                        }
                        table.Write();
                        Console.WriteLine();
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                        Thread.Sleep(1000);
                        Console.WriteLine("Which category would you like to pick to \ndelete item from");
                        Console.WriteLine("1. Cardio\n2. Strength training\n3. Free weights\n4. Sweden walls and turns");
                        s = Console.ReadLine();
                    }
                }

                break;
            }

            return s;
        }

        public bool GetJsonData()
        {
            string equipmentJson;
            bool exceptioFound = false;

            while (true)
            {
                try
                {
                    string jsonEquipmentList = File.ReadAllText(equipmentJsonPath);
                    equipmentList = JsonConvert.DeserializeObject<List<List<Inventory>>>(jsonEquipmentList);
                }
                catch (FileNotFoundException)
                {
                    equipmentList.Add(cardioList);
                    equipmentList.Add(strengthTrainingList);
                    equipmentList.Add(freeWeightsList);
                    equipmentList.Add(swedenWallsAndTurnsList);

                    exceptioFound = true;
                    equipmentJson = JsonConvert.SerializeObject(equipmentList);

                    File.WriteAllText(equipmentJsonPath, equipmentJson);
                    break;
                }

                cardioList = equipmentList[0];
                strengthTrainingList = equipmentList[1];
                freeWeightsList = equipmentList[2];
                swedenWallsAndTurnsList = equipmentList[3];

                break;
            }

            return exceptioFound;
        }

        public void RemoveEquipmentItemFromjson()
        {
            ShowInventory("null");
            Console.WriteLine("Which category would you like to pick to \ndelete item from");
            Console.WriteLine("1. Cardio\n2. Strength training\n3. Free weights\n4. Sweden walls and turns");
            string userCategoryPick = Console.ReadLine();
            string itemCategory = ShowInventory(userCategoryPick);
            Console.WriteLine("Which item would you like to delete? (Pick item ID)");
            string ui = Console.ReadLine();
            bool itemExists = false;

            foreach (var i in equipmentList[Convert.ToInt32(itemCategory) - 1])
            {
                if (Convert.ToString(i.ID) == ui)
                {
                    int uiID = Convert.ToInt32(ui);
                    equipmentList[Convert.ToInt32(itemCategory) - 1].Remove(i);
                    if (uiID <= equipmentList[Convert.ToInt32(itemCategory) - 1].Count)
                    {
                        for (int j = uiID - 1; j < equipmentList[Convert.ToInt32(itemCategory) - 1].Count; j++)
                        {
                            equipmentList[Convert.ToInt32(itemCategory) - 1][j].ID = j + 1;
                        }
                    }

                    itemExists = true;
                    AddInventoryListToList();

                    break;
                }
                else
                {
                    itemExists = false;
                }
            }

            if (!itemExists)
            {
                Console.WriteLine("This item does not exist");
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("Item successfully deleted");
                Thread.Sleep(1000);
            }
        }

        public void AddInventoryListToList()
        {
            string equipmentJson;

            equipmentList[0] = cardioList;
            equipmentList[1] = strengthTrainingList;
            equipmentList[2] = freeWeightsList;
            equipmentList[3] = swedenWallsAndTurnsList;

            equipmentJson = JsonConvert.SerializeObject(equipmentList);

            File.WriteAllText(equipmentJsonPath, equipmentJson);
        }

        public void AddInventoryItemToList()
        {
            GetJsonData();

            for (; ; )
            {
                Console.Clear();
                Console.WriteLine("Please select the Category for the inventory item you would like to add ");
                Console.WriteLine("\n1. Cardio\n2. Strength training\n3. Free weights\n4. Sweden walls and turns");
                string inventoryItem = Console.ReadLine();
                for (; ; )
                {

                    if (inventoryItem == "1")
                    {
                        Console.Write("What is the name of equipment: ");
                        string name = Console.ReadLine();
                        Console.Write("\nWhat is the quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        Cardio cardio = new Cardio("Cardio", name, quantity);

                        cardio.ID = cardioList.Count + 1;

                        if (cardioList.Count < 1)
                        {
                            cardioList.Add(cardio);
                            Console.WriteLine($"Inventory successfully added: {cardio.ID} {cardio.Category} {cardio.Name} {cardio.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (cardioList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var c in cardioList)
                            {
                                doesNotContain = false;
                                if (c.Name == cardio.Name)
                                {
                                    c.Quantity += cardio.Quantity;
                                    Console.WriteLine($"Inventory successfully updated: {c.ID} - Quantity = {c.Quantity}");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else
                                {
                                    doesNotContain = true;
                                }
                            }
                            if (doesNotContain)
                            {
                                Console.WriteLine($"Inventory successfully added: {cardio.ID} {cardio.Category} {cardio.Name} {cardio.Quantity}");
                                Thread.Sleep(1000);
                                cardioList.Add(cardio);
                            }
                        }
                    }
                    else if (inventoryItem == "2")
                    {
                        Console.Write("What is the name of equipment: ");
                        string name = Console.ReadLine();
                        Console.Write("\nWhat is the quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        StrengthTraining strengthTraining = new StrengthTraining("Strength training", name, quantity);

                        strengthTraining.ID = strengthTrainingList.Count + 1;

                        if (strengthTrainingList.Count < 1)
                        {
                            strengthTrainingList.Add(strengthTraining);
                            Console.WriteLine($"Inventory successfully added: {strengthTraining.ID} {strengthTraining.Category} {strengthTraining.Name} {strengthTraining.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (strengthTrainingList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var st in strengthTrainingList)
                            {
                                doesNotContain = false;
                                if (st.Name == strengthTraining.Name)
                                {
                                    st.Quantity += strengthTraining.Quantity;
                                    Console.WriteLine($"Inventory successfully updated: {st.ID} - Quantity = {st.Quantity}");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else
                                {
                                    doesNotContain = true;
                                }
                            }
                            if (doesNotContain)
                            {
                                Console.WriteLine($"Inventory successfully added: {strengthTraining.ID} {strengthTraining.Category} {strengthTraining.Name} {strengthTraining.Quantity}");
                                Thread.Sleep(1000);
                                strengthTrainingList.Add(strengthTraining);
                            }
                        }
                    }
                    else if (inventoryItem == "3")
                    {
                        Console.Write("What is the name of equipment: ");
                        string name = Console.ReadLine();
                        Console.Write("\nWhat is the quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        FreeWeights freeWeights = new FreeWeights("Free weights", name, quantity);

                        freeWeights.ID = freeWeightsList.Count + 1;

                        if (freeWeightsList.Count < 1)
                        {
                            freeWeightsList.Add(freeWeights);
                            Console.WriteLine($"Inventory successfully added: {freeWeights.ID} {freeWeights.Category} {freeWeights.Name} {freeWeights.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (freeWeightsList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var fw in freeWeightsList)
                            {
                                doesNotContain = false;
                                if (fw.Name == freeWeights.Name)
                                {
                                    fw.Quantity += freeWeights.Quantity;
                                    Console.WriteLine($"Inventory successfully updated: {fw.ID} - Quantity = {fw.Quantity}");
                                    Thread.Sleep(1000);

                                    break;
                                }
                                else
                                {
                                    doesNotContain = true;
                                }
                            }
                            if (doesNotContain)
                            {
                                Console.WriteLine($"Inventory successfully added: {freeWeights.ID} {freeWeights.Category} {freeWeights.Name} {freeWeights.Quantity}");
                                Thread.Sleep(1000);
                                freeWeightsList.Add(freeWeights);
                            }
                        }
                    }
                    else if (inventoryItem == "4")
                    {
                        Console.Write("What is the name of equipment: ");
                        string name = Console.ReadLine();
                        Console.Write("\nWhat is the quantity: ");
                        int quantity = Convert.ToInt32(Console.ReadLine());
                        SwedenWallsAndTurns swedenWallsAndTurns = new SwedenWallsAndTurns("Sweden walls and turns", name, quantity);

                        swedenWallsAndTurns.ID = swedenWallsAndTurnsList.Count + 1;

                        if (swedenWallsAndTurnsList.Count < 1)
                        {
                            swedenWallsAndTurnsList.Add(swedenWallsAndTurns);
                            Console.WriteLine($"Inventory successfully added: {swedenWallsAndTurns.ID} {swedenWallsAndTurns.Category} {swedenWallsAndTurns.Name} {swedenWallsAndTurns.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (swedenWallsAndTurnsList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var swat in swedenWallsAndTurnsList)
                            {
                                doesNotContain = false;
                                if (swat.Name == swedenWallsAndTurns.Name)
                                {
                                    swat.Quantity += swedenWallsAndTurns.Quantity;
                                    Console.WriteLine($"Inventory successfully updated: {swat.ID} - Quantity = {swat.Quantity}");
                                    Thread.Sleep(1000);
                                    break;
                                }
                                else
                                {
                                    doesNotContain = true;
                                }
                            }
                            if (doesNotContain)
                            {
                                Console.WriteLine($"Inventory successfully added: {swedenWallsAndTurns.ID} {swedenWallsAndTurns.Category} {swedenWallsAndTurns.Name} {swedenWallsAndTurns.Quantity}");
                                Thread.Sleep(1000);
                                swedenWallsAndTurnsList.Add(swedenWallsAndTurns);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid pick, please try again");
                        Thread.Sleep(1000);
                    }

                    break;
                }

                string inventoryAddPick;
                for (; ; )
                {
                    Console.WriteLine("Would you like to add more equipment?");
                    Console.WriteLine("1. Yes\n2. No");
                    string continueOrNot = Console.ReadLine();
                    inventoryAddPick = continueOrNot;

                    if (continueOrNot == "1")
                    {
                        break;
                    }
                    else if (continueOrNot == "2")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid pick, please try again");
                        Thread.Sleep(1000);
                    }
                }

                if (inventoryAddPick == "2")
                {
                    break;
                }
            }

            AddInventoryListToList();
        }
    }
}
