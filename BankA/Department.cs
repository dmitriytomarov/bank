using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankA
{
    public class Department
    {

        public string DepartmentName { get; set; }
        public string DepartmentID { get; }
        public ObservableCollection<Client> Clients { get; set; }


        public Department(string depName)
        {
            this.DepartmentName = depName;
            this.DepartmentID = Guid.NewGuid().ToString();
            Clients = new ObservableCollection<Client>();
        }

        public bool AddClientByID(Client clientID)
        {
            if (Clients.Contains(clientID))
            {
                MessageBox.Show("Этот клиент уже есть в данном департаменте");
                return false;
            }
            Clients.Add(clientID);
            return true;
        }
        public bool RemoveClientByID(Client clientID)
        {
            if (!Clients.Contains(clientID))
            {
                MessageBox.Show($"Такой клиент не найден в {DepartmentName}");
                return false;
            }
            Clients.Remove(clientID);
            return true;
        }


    }
}
