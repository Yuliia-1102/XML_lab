using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal class ScientificWorker
    {
        public ScientificWorker() 
        {
            Name = "";
            AuthorName = "";
            Faculty = "";
            Department = "";
            DateOfBirth = "";
            AuthorPosition = "";
            Gender = "";
            Address = "";
            Age = "";
            Branch = "";
        }

        public string Name { get; set; }
        public string AuthorName { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string DateOfBirth { get; set; }
        public string AuthorPosition { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Age { get; set; }
        public string Branch { get; set; }
    }
}
