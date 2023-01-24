using ConsoleTables;
using CourseProject.HelpersAndConstants;

namespace CourseProject.MenuObjects
{
    internal class InventoryListHelper
    {
        public ConsoleTable table;

        public void GroupingByCriterias(Gym gym)
        {
            ShowInventoryItems("null",gym);
            Console.WriteLine("By which criteria would you like to sort?");
            Console.WriteLine("1. ID\n2. Name\n3. Quantity");

            string userPick = Console.ReadLine();
            List<Inventory> mergedList;
            table = new ConsoleTable(Constants.columnNames);
            int cardioItemsQuantity = gym.cardioList.Count;
            int strengthTrainingItemsQuantity = gym.strengthTrainingList.Count;
            int freeWeightsItemsQuantity = gym.freeWeightsList.Count;
            int swedenWallsAndTurnsItemsQuantity = gym.swedenWallsAndTurnsList.Count;

            for (; ; )
            {
                table.Rows.Clear();
                if (userPick == "1")
                {
                    while (true)
                    {
                        foreach (var i in gym.equipmentList)
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
                        cardioItemsQuantity--;
                        strengthTrainingItemsQuantity--;
                        freeWeightsItemsQuantity--;
                        swedenWallsAndTurnsItemsQuantity--;

                        if (cardioItemsQuantity <= 0 && strengthTrainingItemsQuantity <= 0 && freeWeightsItemsQuantity <= 0 && swedenWallsAndTurnsItemsQuantity <= 0)
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

                    foreach (var list in gym.equipmentList)
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

                    foreach (var list in gym.equipmentList)
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

        public string ShowInventoryItems(string s, Gym gym)
        {
            gym.GetDataForEquipmentLists();
            Console.WriteLine();
            table = new ConsoleTable(Constants.columnNames);

            for (; ; )
            {
                for (; ; )
                {
                    if (s == "null")
                    {
                        foreach (var e in gym.equipmentList)
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
                        foreach (var i in gym.equipmentList[0])
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
                        foreach (var i in gym.equipmentList[1])
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
                        foreach (var i in gym.equipmentList[2])
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
                        foreach (var i in gym.equipmentList[3])
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

        public void RemoveEquipmentItemFromjson(Gym gym)
        {
            ShowInventoryItems("null",gym);
            Console.WriteLine("Which category would you like to pick to \ndelete item from");
            Console.WriteLine("1. Cardio\n2. Strength training\n3. Free weights\n4. Sweden walls and turns");
            string userCategoryPick = Console.ReadLine();
            string itemCategory = ShowInventoryItems(userCategoryPick,gym);
            Console.WriteLine("Which item would you like to delete? (Pick item ID)");
            string ui = Console.ReadLine();
            bool itemExists = false;

            foreach (var i in gym.equipmentList[Convert.ToInt32(itemCategory) - 1])
            {
                if (Convert.ToString(i.ID) == ui)
                {
                    int uiID = Convert.ToInt32(ui);
                    gym.equipmentList[Convert.ToInt32(itemCategory) - 1].Remove(i);
                    if (uiID <= gym.equipmentList[Convert.ToInt32(itemCategory) - 1].Count)
                    {
                        for (int j = uiID - 1; j < gym.equipmentList[Convert.ToInt32(itemCategory) - 1].Count; j++)
                        {
                            gym.equipmentList[Convert.ToInt32(itemCategory) - 1][j].ID = j + 1;
                        }
                    }

                    itemExists = true;
                    gym.AddInventoryListToList();

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

        public void AddInventoryItemToList(Gym gym)
        {
            gym.GetDataForEquipmentLists();

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

                        cardio.ID = gym.cardioList.Count + 1;

                        if (gym.cardioList.Count < 1)
                        {
                            gym.cardioList.Add(cardio);
                            Console.WriteLine($"Inventory successfully added: {cardio.ID} {cardio.Category} {cardio.Name} {cardio.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (gym.cardioList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var c in gym.cardioList)
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
                                gym.cardioList.Add(cardio);
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

                        strengthTraining.ID = gym.strengthTrainingList.Count + 1;

                        if (gym.strengthTrainingList.Count < 1)
                        {
                            gym.strengthTrainingList.Add(strengthTraining);
                            Console.WriteLine($"Inventory successfully added: {strengthTraining.ID} {strengthTraining.Category} {strengthTraining.Name} {strengthTraining.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (gym.strengthTrainingList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var st in gym.strengthTrainingList)
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
                                gym.strengthTrainingList.Add(strengthTraining);
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

                        freeWeights.ID = gym.freeWeightsList.Count + 1;

                        if (gym.freeWeightsList.Count < 1)
                        {
                            gym.freeWeightsList.Add(freeWeights);
                            Console.WriteLine($"Inventory successfully added: {freeWeights.ID} {freeWeights.Category} {freeWeights.Name} {freeWeights.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (gym.freeWeightsList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var fw in gym.freeWeightsList)
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
                                gym.freeWeightsList.Add(freeWeights);
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

                        swedenWallsAndTurns.ID = gym.swedenWallsAndTurnsList.Count + 1;

                        if (gym.swedenWallsAndTurnsList.Count < 1)
                        {
                            gym.swedenWallsAndTurnsList.Add(swedenWallsAndTurns);
                            Console.WriteLine($"Inventory successfully added: {swedenWallsAndTurns.ID} {swedenWallsAndTurns.Category} {swedenWallsAndTurns.Name} {swedenWallsAndTurns.Quantity}");
                            Thread.Sleep(1000);
                        }
                        else if (gym.swedenWallsAndTurnsList.Count >= 1)
                        {
                            bool doesNotContain = false;
                            foreach (var swat in gym.swedenWallsAndTurnsList)
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
                                gym.swedenWallsAndTurnsList.Add(swedenWallsAndTurns);
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

            gym.AddInventoryListToList();
        }
    }
}
