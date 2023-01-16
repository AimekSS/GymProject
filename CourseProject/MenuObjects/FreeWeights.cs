using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.MenuObjects
{
    internal class FreeWeights : Inventory 
    {
        public FreeWeights(string category,  string name, int quantity) : base(category,  name, quantity) 
        {

        }
    }
}
