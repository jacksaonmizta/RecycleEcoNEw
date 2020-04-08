using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class CollectorVM :INotifyPropertyChanged
    {
        public static Collector Collector { get; set; }

        private bool canSignUp;
        public bool CanSignUp
        {
            get
            {
                return canSignUp;
            }
            set
            {
                canSignUp = value;
                OnPropertyChanged();
            }
        }

        private bool canUpdate;
        public bool CanUpdate
        {
            get
            {
                return canUpdate;
            }
            set
            {
                canUpdate = value;
                OnPropertyChanged();
            }
        }

        private string signUpStatus;
        public string SignUpStatus
        {
            get
            {
                return signUpStatus;
            }
            set
            {
                signUpStatus = value;
                OnPropertyChanged();
            }
        }
        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                CanSignUp = CheckFields() && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get { return Collector.Username; }
            set
            {
                Collector.Username = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }
        public string Password
        {
            get { return Collector.Password; }
            set
            {
                Collector.Password = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }
        public string FullName
        {
            get { return Collector.FullName; }
            set
            {
                Collector.FullName = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public string Address
        {
            get { return Collector.Address; }
            set
            {
                Collector.Address = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        private bool CheckFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Collector.Username) &&
                          !string.IsNullOrWhiteSpace(Collector.Password) &&
                          !string.IsNullOrWhiteSpace(Collector.FullName) &&
                          !string.IsNullOrWhiteSpace(Collector.Address);
            return result;
        }

        public ICommand SignUp { get; set; }
        public ICommand OpenUpdateCollectorView { get; set; }
        public ICommand UpdateCollector { get; set; }
        public ICommand OpenAddCollectionView { get; set; }
        public ICommand OpenSubmissionView { get; set; }
        public ICommand OpenMaterialSubmissionView { get; set; }
        public ICommand SignOut { get; set; }

       

        public ICommand OpenRecordSubmissionView { get; set; }

        public ICommand OpenCollectorProfileView { get; set; }

        public ICommand OpenCollectorEditView { get; set; }

        public ICommand UpdateProfile { get; set; }

        public ICommand OpenSubmissionListView{ get; set; }

        public ICommand OpenMaterialSelectView { get; set; }

        public CollectorVM()
        {
            if (Collector == default(Collector))
            {
                Collector = new Collector();
            }
            SignUp = new Command(SignUpExecute, CanSignUpM);
            
            SignOut = new Command(SignOutExecute);

            //to open Collector profile page
            OpenCollectorProfileView = new Command(OpenCollectorProfileExecute);

            //to open Collector edit profile page
            OpenCollectorEditView = new Command(OpenCollectorEditExecute);

            //to connect to page where submission can be editted
            OpenRecordSubmissionView = new Command(OpenRecordMaterialsExecute);

            //to update collector profile
            UpdateProfile = new Command(UpdateCollectorExecute);

            //to view submission list
            OpenSubmissionListView = new Command(OpenSubmissionListExecute);

            //to open material selection view

            OpenMaterialSelectView = new Command(OpenMaterialSelectExecute);

            
            
        }

        //collector profile
        private void OpenCollectorProfileExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.CollectorProfilePage());
        }

        //edit collector profile
        private void OpenCollectorEditExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.CollectorEditProfilePage());
        }

        //open record submission page
        private void OpenRecordMaterialsExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.RecordSubMat());
        }

        //to update collector profile

        private async void UpdateCollectorExecute()
        {
            await CollectorAuth.UpdateCollector(Collector);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //to view submission list
        private void OpenSubmissionListExecute()
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.RecyclerViewSubmissions());
        }

        //to select materials for collector

        private void OpenMaterialSelectExecute(object obj)
        {
            Application.Current.MainPage.Navigation.PushAsync(new Views.MaterialSelectCollector());
        }




        private async void SignUpExecute(object obj)
        {
            SignUpStatus = string.Empty;
            if (CheckPassword())
            {
                Collector collector = await CollectorAuth.GetCollector(Collector);
                if (collector == null)
                {
                    Collector.TotalPoints = 0;
                    await CollectorAuth.AddCollector(Collector);
                    Username = string.Empty;
                    Password = string.Empty;
                    FullName = string.Empty;
                    Address = string.Empty;
                    ConfirmPassword = string.Empty;
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    SignUpStatus = "Username already exists! Please choose another username";
                }
            }
            else
            {
                SignUpStatus = "Confirmation password is not matched with your password";
            }
        }

        private bool CheckPassword()
        {
            if (ConfirmPassword == Password)
            {
                return true;
            }
            return false;
        }


        private bool CanSignUpM(object arg)
        {
            return CanSignUp;
        }
        
        private void SignOutExecute(object obj)
        {
            Application.Current.Properties["loggedIn"] = 0;
            Collector = null;
            Application.Current.MainPage = new NavigationPage(new Views.MainStartView());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //-----Profile
       

    }
}

