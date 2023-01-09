using Newtonsoft.Json;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseProject.Users
{
    internal class Visitor : User
    {
        [JsonConstructor]
        public Visitor()
        {

        }

        public Visitor(bool isCoach) : base(isCoach)
        {

        }
    }
}