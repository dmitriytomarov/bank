using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankA
{
    public class TestData : IDataProvider
    {

        static Random rnd = new Random();

        public void GetData(ObservableCollection<Department> departments, List<Log> logs)
        {

            
            for (int i = 0; i < 5; i++)
            {
                Department department = new Department($"Группа клиентов {i}");
                departments.Add(department);
                for (int j = 0; j < 100; j++)
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

            // logs (история) в тестовой базе не загружается
        }
    }
}
