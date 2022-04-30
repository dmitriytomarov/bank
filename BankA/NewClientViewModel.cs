using System;
using System.Diagnostics;
using System.Windows;

namespace BankA
{
    internal class NewClientViewModel : ViewModel
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }

        private ViewModel vmInstance; // родительская вм
        public Command Ok
        {
            get
            {
                return new Command(o =>
                {
                    vmInstance.NewClient.LastName = LastName;
                    vmInstance.NewClient.FirstName = FirstName;
                    vmInstance.NewClient.MiddleName = MiddleName;
                    vmInstance.NewClient.Phone = Phone;
                    vmInstance.NewClient.Passport = Passport;
                    this.Cansel.Execute(null);  //просто закрываем окно
                    vmInstance.User.AddNewClient(vmInstance.DataBase, vmInstance.SelectedDepartment, vmInstance);
                },
                o => (!String.IsNullOrWhiteSpace(LastName) &&
                    !String.IsNullOrWhiteSpace(FirstName) &&
                    !String.IsNullOrWhiteSpace(MiddleName) &&
                    !String.IsNullOrWhiteSpace(Phone) &&
                    !String.IsNullOrWhiteSpace(Passport))
                );

            }
        }

        public Command Cansel
        {
            get
            {
                return new Command(o =>
                {
                    LastName = "";
                    FirstName = "";
                    MiddleName = "";
                    Phone = "";
                    Passport = "";

                    Window w = null;
                    foreach (Window e in App.Current.Windows)
                    {
                        if (e.Title == "New Client") w = e;
                    }
                    w.Close();
                });

            }
        }

        public NewClientViewModel() { }
        public NewClientViewModel(ViewModel vMInstance)
        {
            LastName = "";
            FirstName = "";
            MiddleName = "";
            Phone = "";
            Passport = "";
            this.vmInstance = vMInstance;
        }

    }
}