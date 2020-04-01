using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class UserTypeVM
    {
        public const string CollectorUserType = "Collector";
        public const string RecyclerUserType = "Recycler";
        public const string AdminUserType = "Admin";
        public ICommand UserAsAdmin { get; set; }
        public ICommand UserAsRecycler { get; set; }
        public ICommand UserAsCollector { get; set; }
        public UserTypeVM()
        {
            UserAsAdmin = new Command(UserAsAdminExecute);
            UserAsRecycler = new Command(UserAsRecyclerExecute);
            UserAsCollector = new Command(UserAsCollectorExecute);
        }

        private void UserAsCollectorExecute(object obj)
        {
            LoginVM.UserType = CollectorUserType;
            Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private void UserAsRecyclerExecute(object obj)
        {
            LoginVM.UserType = RecyclerUserType;
            Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }

        private void UserAsAdminExecute(object obj)
        {
            LoginVM.UserType = AdminUserType;
            Application.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        }
    }
}
