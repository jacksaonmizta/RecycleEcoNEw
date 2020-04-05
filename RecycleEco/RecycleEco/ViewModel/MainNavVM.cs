using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class MainNavVM
    {
        public ICommand OpenManageMaterialView { get; set; }
        public ICommand OpenMaterialSubmissionView { get; set; }
        public ICommand SignOut { get; set; }

        
       
        public MainNavVM()
        {
            OpenManageMaterialView = new Command(OpenManageMaterialExecute);
            
            SignOut = new Command(SignOutExecute);
        }

        private void SignOutExecute(object obj)
        {
            Application.Current.Properties["loggedIn"] = 0;
            Application.Current.MainPage = new NavigationPage(new Views.MainStartView());
        }

        

        private void OpenManageMaterialExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.MaterialListPage());
        }
    }
}

