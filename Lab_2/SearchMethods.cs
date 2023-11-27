using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lab_2
{
    internal class SearchMethods
    {
        public static bool IsValidEntry(string nodeValue, string criteriaValue)
        {
            return string.IsNullOrEmpty(criteriaValue) || nodeValue.Trim().ToLower().Contains(criteriaValue?.Trim().ToLower());
        } 

        public static bool IsValidPickerValue(string nodeValue, string criteriaValue)
        {
            return string.IsNullOrEmpty(criteriaValue) || nodeValue.Equals(criteriaValue);
        }
    }
}
