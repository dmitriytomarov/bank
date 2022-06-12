using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public interface IDataProvider
    {
        public void GetData(ObservableCollection<Department> departments, List<Log> logs);
    }
}
