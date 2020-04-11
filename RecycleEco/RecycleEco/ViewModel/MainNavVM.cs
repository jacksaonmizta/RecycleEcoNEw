using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class MainNavVM 

    {

        

       
        public ICommand OpenMaterialView { get; set; }
        public ICommand OpenAdminMaterialSubmissionView { get; set; }
        public ICommand SignOut { get; set; }



        public MainNavVM()
        {
            OpenMaterialView = new Command(OpenMaterialViewExecute);

            OpenAdminMaterialSubmissionView = new Command(OpenAdminMaterialSubmissionViewExecute);

            SignOut = new Command(SignOutExecute);
        }

        public event PropertyChangedEventHandler PropertyChanged;

       
        private void OpenAdminMaterialSubmissionViewExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.AdminMaterialSubmissionView());
        }

        private void OpenMaterialViewExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.MaterialListPage());
        }




        private void SignOutExecute(object obj)
        {
            Application.Current.Properties["loggedIn"] = 0;
            Application.Current.MainPage = new NavigationPage(new Views.MainStartView());
        }



    }  
}

