using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

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
