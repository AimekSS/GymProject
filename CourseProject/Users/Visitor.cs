using Newtonsoft.Json;

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