using Newtonsoft.Json;

namespace CourseProject.Users
{
    internal class Coach : User
    {
        [JsonConstructor]
        public Coach()
        {

        }

        public Coach(bool isCoach) : base(isCoach)
        {
            
        }
    }
}
