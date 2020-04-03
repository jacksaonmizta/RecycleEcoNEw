using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class UserTypeVM
    {
        public const string CollectorUT = "Collector";
        public const string RecyclerUT = "Recycler";
        public const string AdminUT = "Admin";
        public ICommand UserAdmin { get; set; }
        public ICommand UserRecycler { get; set; }
        public ICommand UserCollector { get; set; }
        public UserTypeVM()
        {
            UserAdmin = new Command(UserAsAdminExecute);
            UserRecycler = new Command(UserAsRecyclerExecute);
            UserCollector = new Command(UserAsCollectorExecute);
        }

        private void UserAsCollectorExecute(object obj)
        {
            LoginVM.UserType = CollectorUT;
            Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private void UserAsRecyclerExecute(object obj)
        {
            LoginVM.UserType = RecyclerUT;
            Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private void UserAsAdminExecute(object obj)
        {
            LoginVM.UserType = AdminUT;
            Application.Current.MainPage.Navigation.PushAsync(new Views.AdminLoginPage());
        }
    }
}
