using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    /// <summary>
    /// Хранилище департаментов, клиентов, логов (коллекция)
    /// </summary>
    public class DataBase
    {

        public List<Log> Logs { get; set; }
        public ObservableCollection<Department> Departments { get; set; }

        public DataBase(IDataProvider source)
        {
            Departments = new ObservableCollection<Department>();
            Logs = new List<Log>();


            source.GetData(Departments, Logs);

        }
    }
}
