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
        public CollectorVM()
        {
            if (Collector == default(Collector))
            {
                Collector = new Collector();
            }
            SignUp = new Command(SignUpExecute, CanSignUpM);
            
            SignOut = new Command(SignOutExecute);
        }

        private async void SignUpExecute(object obj)
        {
            SignUpStatus = string.Empty;
            if (CheckPassword())
            {
                Collector collector = await CollectorAuth.GetCollector(Collector);
                if (collector == null)
                {
                    Collector.Points = 0;
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
    }
}

