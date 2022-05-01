using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace BankA
{
    public interface IEmployee
    {


        /// <summary>
        /// Изменение данных текущего клиента
        /// </summary>
        /// <param name="dataBase">ссылка на БД</param>
        /// <param name="target">обновляемый клиент</param>
        /// <param name="currentClient">новые данные (все поля в виде обекта Client</param>
        void SetInfo(DataBase dataBase, Client target, Client currentClient);
        string LastName { get; set; }
        string FirstName { get; set; }
        string Login { get; set; }


        /// <summary>
        /// Просмотр данных текущего клиента
        /// </summary>
        /// <param name="dataBase">ссылка на БД</param>
        /// <param name="client">ссылка на экземпляр клиента</param>
        /// <returns></returns>
        Client GetInfo(DataBase dataBase, Client client);
        /// <summary>
        /// Получение инфо из логов о последнем изменении полей
        /// </summary>
        /// <param name="dataBase"></param>
        /// <param name="iD"></param>
        /// <returns></returns>
        Client GetChanges(DataBase dataBase, string iD);
        /// <summary>
        /// проверка пароля пользователя
        /// </summary>
        /// <param name="password">введенный пароль</param>
        /// <returns></returns>
        bool CheckPassword(string? password);
        void AddNewClient(DataBase dataBase, Department department, ViewModel vmInstance);

    }

    public class Consultant : IEmployee
    {

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Login { get; set; }
        private string _password;

        public Consultant(string lastName, string firstName, string login, string password = "11111")
        {
            LastName = lastName;
            FirstName = firstName;
            Login = login;
            _password = password;
        }
        public Consultant() : this("newUser", "", "", "") { }

        public virtual void SetInfo(DataBase dataBase, Client target, Client currentClient)
        {
            if (!string.IsNullOrWhiteSpace(currentClient.Phone))
            {
                if (target.Phone != currentClient.Phone)
                {
                    target.Phone = currentClient.Phone;
                    dataBase.Logs.Add(new Log(currentClient.ID, "Phone", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
                }
            }
            else
            {
                MessageBox.Show("Телефон не должен быть пустым. Данные не сохранены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }



        public virtual Client GetInfo(DataBase dataBase, Client client)
        {
            return new Client
                (
                    client.LastName,
                    client.FirstName,
                    client.MiddleName,
                    client.Phone,
                    String.IsNullOrEmpty(client.Passport) ? "" : "**** ******",
                    client.ID
                );
        }


        public Client GetChanges(DataBase dataBase, string iD)
        {
            return new Client(
                dataBase.Logs.FindLast((o) => (o.ID == iD && o.Field == "LastName"))?.LastChange,
                dataBase.Logs.FindLast((o) => (o.ID == iD && o.Field == "FirstName"))?.LastChange,
                dataBase.Logs.FindLast((o) => (o.ID == iD && o.Field == "MiddleName"))?.LastChange,
                dataBase.Logs.FindLast((o) => (o.ID == iD && o.Field == "Phone"))?.LastChange,
                dataBase.Logs.FindLast((o) => (o.ID == iD && o.Field == "Passport"))?.LastChange,
                iD
            );
        }

        public virtual bool CheckPassword(string password)
        {
            if (this._password == password) return true;
            else return false;
        }

        public static List<IEmployee> CreateTestUsers()
        {
            return new List<IEmployee> {
                new Consultant("Легков", "Алексей","legkov", "123" ),
                new Manager("Мальков", "Дмитрий", "malkov", "789") };
        }

        public void AddNewClient(DataBase dataBase, Department department, ViewModel vmInstance)
        {
            MessageBox.Show("Недостаточно прав для добавления клиента");
            //throw new NotImplementedException();
        }
    }


    public class Manager : Consultant, IEmployee
    {

        public new string LastName { get; set; }
        public new string FirstName { get; set; }
        public new string Login { get; set; }
        private string _password;

        public Manager(string lastName, string firstName, string login, string password = "11111")
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.Login = login;
            this._password = password;
        }



        public override void SetInfo(DataBase dataBase, Client target, Client currentClient)
        {
            if (target.LastName != currentClient.LastName)  //только если данные в поле изменились
            {
                target.LastName = currentClient.LastName;
                dataBase.Logs.Add(new Log(currentClient.ID, "LastName", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
            }
            if (target.FirstName != currentClient.FirstName)
            {
                target.FirstName = currentClient.FirstName;
                dataBase.Logs.Add(new Log(currentClient.ID, "FirstName", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
            }
            if (target.MiddleName != currentClient.MiddleName)
            {
                target.MiddleName = currentClient.MiddleName;
                dataBase.Logs.Add(new Log(currentClient.ID, "MiddleName", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
            }
            if (target.Phone != currentClient.Phone)
            {
                target.Phone = currentClient.Phone;        // менеджер может сохранить в т.числе пустой телефон
                dataBase.Logs.Add(new Log(currentClient.ID, "Phone", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
            }
            if (target.Passport != currentClient.Passport)
            {
                target.Passport = currentClient.Passport;
                dataBase.Logs.Add(new Log(currentClient.ID, "Passport", DateTime.Now.ToString(), this.GetType().Name, (this.LastName + " " + this.FirstName)));
            }
        }


        public override Client GetInfo(DataBase dataBase, Client client)
        {
            return new Client
                (
                    client.LastName,
                    client.FirstName,
                    client.MiddleName,
                    client.Phone,
                    client.Passport,
                    client.ID
                );
        }

        /// <summary>
        /// Добавление нового клиента. Возвращает количество клиентов после добавления
        /// </summary>
        /// <param name="dataBase"></param>
        /// <returns></returns>
        public new void AddNewClient(DataBase dataBase, Department department, ViewModel vmInstance)
        {
            //здесь будет добавление клиента в базу. Должны прийти поля, либо объект клиент с полями

            Client newClient = new Client(vmInstance.NewClient.LastName,
                                            vmInstance.NewClient.FirstName,
                                            vmInstance.NewClient.MiddleName,
                                            vmInstance.NewClient.Phone,
                                            vmInstance.NewClient.Passport);

            department.Clients.Add(newClient);

            dataBase.Logs.Add(new Log(
                    newClient.ID, "LastName", DateTime.Now.ToString(), $"{this.GetType().Name} {this.LastName} {this.FirstName} добавил нового клиента", ""));
            dataBase.Logs.Add(new Log(
                    newClient.ID, "FirstName", DateTime.Now.ToString(), $"{this.GetType().Name} {this.LastName} {this.FirstName} добавил нового клиента", ""));
            dataBase.Logs.Add(new Log(
                    newClient.ID, "MiddleName", DateTime.Now.ToString(), $"{this.GetType().Name} {this.LastName} {this.FirstName} добавил нового клиента", ""));
            dataBase.Logs.Add(new Log(
                    newClient.ID, "Phone", DateTime.Now.ToString(), $"{this.GetType().Name} {this.LastName} {this.FirstName} добавил нового клиента", ""));
            dataBase.Logs.Add(new Log(
                    newClient.ID, "Passport", DateTime.Now.ToString(), $"{this.GetType().Name} {this.LastName} {this.FirstName} добавил нового клиента", ""));

            vmInstance.SelectedClient = newClient;
        }

        public override bool CheckPassword(string password)
        {
            if (this._password == password) return true;
            else return false;
        }
    }
}
