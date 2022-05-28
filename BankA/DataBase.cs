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
        static Random rnd = new Random();
        public List<Log> Logs { get; set; }
        public ObservableCollection<Department> Departments { get; set; }



        public DataBase()
        {
            Departments = new ObservableCollection<Department>();
            Logs = new List<Log>();


            for (int i = 0; i < 30; i++)
            {
                Department department = new Department($"Департамент {i}");
                Departments.Add(department);
                for (int j = 0; j < 10000; j++)
                {
                    Client client = new Client(
                        $"Фамилия{i}{j}",
                        $"Имя{i}{j}",
                        $"Отчество{i}{j}",
                        $"+7 977-{rnd.Next(1000, 9999)}",
                        $"{rnd.Next(1000, 9999)} {rnd.Next(100000, 900000)}"); //паспорт
                    client.Accounts.Add(new StandartCurrentAccount(Account.Currency.RUR, client));
                    client.Accounts.Add(new StandartCurrentAccount(Account.Currency.EUR, client));
                    client.Accounts.Add(new StandartCurrentAccount(Account.Currency.USD, client));
                    department.Clients.Add(client);
                }

            }



        }
    }
}
