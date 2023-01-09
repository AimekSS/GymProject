namespace CourseProject.MenuObjects
{
    internal class Inventory
    {
        public string Category { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }

        public Inventory()
        {

        }

        public Inventory(string category, string name, int quantity)
        {
            Category = category;
            Name = name;
            Quantity = quantity;
        }
    }
}