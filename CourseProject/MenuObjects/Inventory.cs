using CourseProject.Users;
using System.Text.Json.Serialization;

namespace CourseProject.MenuObjects
{
    [JsonDerivedType(typeof(Coach), typeDiscriminator: "Coach")]
    [JsonDerivedType(typeof(FreeWeights), typeDiscriminator: "FreeWeights")]
    [JsonDerivedType(typeof(StrengthTraining), typeDiscriminator: "StrengthTraining")]
    [JsonDerivedType(typeof(SwedenWallsAndTurns), typeDiscriminator: "SwedenWallsAndTurns")]

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