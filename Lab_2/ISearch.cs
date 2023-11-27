using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2
{
    internal interface ISearch
    {
        ObservableCollection<ScientificWorker> Search(SearchCriteria criteria, string xmlPath);
    }
}
