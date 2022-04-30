using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public class Log
    {
        public string ID { get; }
        public string Field { get; }
        public string LastChangeTime { get; }
        public string LastChangeUserRole { get; }
        public string LastChange { get; }
        public string LastChangeUserName { get; }


        public Log(string id, string field, string time, string userRole, string userName)
        {
            ID = id;
            Field = field;
            LastChangeTime = time;
            LastChangeUserRole = userRole;
            LastChange = time + " " + userRole + " " + userName;
        }
    }
}
