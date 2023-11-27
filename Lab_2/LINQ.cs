using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab_2
{
    internal class LINQ : ISearch
    {
        public LINQ () { }

        public ObservableCollection<ScientificWorker> Search(SearchCriteria criteria, string xmlPath)
        {
            ObservableCollection<ScientificWorker> results = new ObservableCollection<ScientificWorker>();
            var doc = XDocument.Load(xmlPath);
            var result = from worker in doc.Descendants("ScientificWorker")
                         where (
                         SearchMethods.IsValidEntry(worker.Element("Name").Value, criteria.Name) &&
                         SearchMethods.IsValidPickerValue(worker.Element("AuthorName").Value, criteria.AuthorName) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Faculty").Value, criteria.Faculty) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Department").Value, criteria.Department) &&
                         SearchMethods.IsValidPickerValue(worker.Element("DateOfBirth").Value, criteria.DateOfBirth) &&
                         SearchMethods.IsValidPickerValue(worker.Element("AuthorPosition").Value, criteria.AuthorPosition) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Gender").Value, criteria.Gender) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Address").Value, criteria.Address) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Age").Value, criteria.Age) &&
                         SearchMethods.IsValidPickerValue(worker.Element("Branch").Value, criteria.Branch)
                         )
                         select new ScientificWorker
                         {
                             Name = worker.Element("Name").Value,
                             AuthorName = worker.Element("AuthorName").Value,
                             Faculty=worker.Element("Faculty").Value,
                             Department = worker.Element("Department").Value,
                             DateOfBirth = worker.Element("DateOfBirth").Value,
                             AuthorPosition = worker.Element("AuthorPosition").Value,
                             Gender = worker.Element("Gender").Value,
                             Address = worker.Element("Address").Value,
                             Age = worker.Element("Age").Value,
                             Branch = worker.Element("Branch").Value
                         };

            foreach(var worker in result)
            {
                results.Add(worker);
            }

            return results;
        }
    }
}
