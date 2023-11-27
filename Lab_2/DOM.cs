using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab_2
{
    internal class DOM : ISearch
    {
        public DOM() { }

        private void ProcessNode(XmlNode n, ObservableCollection<ScientificWorker> results, SearchCriteria criteria)
        {
            string name = "";
            string authorName = "";
            string faculty = "";
            string department = "";
            string dateOfBirth = "";
            string authorPosition = "";
            string gender = "";
            string address = "";
            string age = "";
            string branch = "";

            foreach (XmlNode childNode in n.ChildNodes)
            {
                string nodeValue = childNode.InnerText;

                if (childNode.Name.Equals("Name") && SearchMethods.IsValidEntry(nodeValue, criteria.Name))
                {
                    name = nodeValue;
                }
                if (childNode.Name.Equals("AuthorName") && SearchMethods.IsValidPickerValue(nodeValue, criteria.AuthorName))
                {
                    authorName = nodeValue;
                }
                if (childNode.Name.Equals("Faculty") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Faculty))
                {
                    faculty = nodeValue;
                }
                if (childNode.Name.Equals("Department") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Department))
                {
                    department = nodeValue;
                }
                if (childNode.Name.Equals("DateOfBirth") && SearchMethods.IsValidPickerValue(nodeValue, criteria.DateOfBirth))
                {
                    dateOfBirth = nodeValue;
                }
                if (childNode.Name.Equals("AuthorPosition") && SearchMethods.IsValidPickerValue(nodeValue, criteria.AuthorPosition))
                {
                    authorPosition = nodeValue;
                }
                if (childNode.Name.Equals("Gender") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Gender))
                {
                    gender = nodeValue;
                }
                if (childNode.Name.Equals("Address") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Address))
                {
                    address = nodeValue;
                }
                if (childNode.Name.Equals("Age") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Age))
                {
                    age = nodeValue;
                }
                if (childNode.Name.Equals("Branch") && SearchMethods.IsValidPickerValue(nodeValue, criteria.Branch))
                {
                    branch = nodeValue;
                }
            }

            if (name != "" && authorName != "" && faculty != "" && department != "" && dateOfBirth != "" && authorPosition != "" && gender != "" && address != "" && age != "" && branch != "")
            {
                ScientificWorker worker = new ScientificWorker();
                worker.Name = name;
                worker.AuthorName = authorName;
                worker.Faculty = faculty;
                worker.Department = department;
                worker.DateOfBirth = dateOfBirth;
                worker.AuthorPosition = authorPosition;
                worker.Gender = gender;
                worker.Address = address;
                worker.Age = age;
                worker.Branch = branch;

                results.Add(worker);
            }
        }

        public ObservableCollection<ScientificWorker> Search(SearchCriteria criteria, string xmlPath)
        {
            ObservableCollection<ScientificWorker> results = new ObservableCollection<ScientificWorker>();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlPath);

            XmlNode node = doc.DocumentElement;
            foreach (XmlNode nod in node.ChildNodes)
            {
                ProcessNode(nod, results, criteria);
            }
            return results;
        }
    }
}
