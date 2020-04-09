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
    class RecyclerVM :INotifyPropertyChanged
    {
        public const string EcoLevelOne = "NewBie";
        public const string EcoLevelTwo = "Eco-Saver";
        public const string EcoLevelThree = "Eco-Hero";
        public const string EcoLevelFour = "Eco-Warrior";
        public static Recycler Recycler { get; set; }

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
            get { return Recycler.Username; }
            set
            {
                Recycler.Username = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return Recycler.Password; }
            set
            {
                Recycler.Password = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return Recycler.FullName; }
            set
            {
                Recycler.FullName = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public int TotalPoints
        {
            get { return Recycler.TotalPoints; }
            set
            {
                Recycler.TotalPoints = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        public string EcoLevel
        {
            get { return Recycler.EcoLevel; }
            set
            {
                Recycler.EcoLevel = value;
                CanUpdate = CheckFields();
                CanSignUp = CanUpdate && !string.IsNullOrWhiteSpace(ConfirmPassword);
                OnPropertyChanged();
            }
        }

        private bool CheckFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Recycler.Username) &&
                          !string.IsNullOrWhiteSpace(Recycler.Password) &&
                          !string.IsNullOrWhiteSpace(Recycler.FullName);
            return result;
        }

        public ICommand SignUp { get; set; }
        public ICommand OpenUpdateRecyclerView { get; set; }
        public ICommand Update { get; set; }

        public ICommand OpenRecyclerMainMenu { get; set; } //jun's

        public ICommand OpenRecyclerProfilePage { get; set; } //view recycler profile page
        public ICommand OpenRecyclerEditProfilePage { get; set; } //edit recycler's profile page

        public ICommand OpenMaterialSubmissionView { get; set; } //view recycler's submission page
        public ICommand OpenMakeSubmissionView { get; set; } //make submission form page

        public ICommand SignOut { get; set; }

        public RecyclerVM()
        {
            if (Recycler == default(Recycler))
            {
                Recycler = new Recycler();
            }
            SignUp = new Command(SignUpExecute, CanSignUpM);
            SignOut = new Command(SignOutExecute);
            OpenMaterialSubmissionView = new Command(RecyclerSubmission);  //View All Submissions
            OpenRecyclerProfilePage = new Command(RecyclerProfilePage); //Recycler profile page
            OpenMakeSubmissionView = new Command(RecyclerSubmissionForm); //Add Submission
            OpenRecyclerEditProfilePage = new Command(RecyclerEditProfile); //edit profile
            Update = new Command(UpdateExecute); //update recycler info
        }

        private async void UpdateExecute()
        {
            await RecyclerAuth.UpdateRecycler(Recycler);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void RecyclerEditProfile(object obj) //Edit Recycler profile page
        {
            Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerEditProfilePage());
        }

        private void RecyclerSubmissionForm(object obj) //Add Submission
        {
            Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerChooseMaterial());
        }

        private void RecyclerProfilePage(object obj) //Recycler profile page
        {
            Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerProfilePage());
        }

        private void RecyclerSubmission(object obj) //View All Submissions
        {
            Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerViewSubmissions());
        }

        private async void SignUpExecute(object obj)
        {
            SignUpStatus = string.Empty;
            if (CheckPassword())
            {
                Recycler recycler = await RecyclerAuth.GetRecycler(Recycler);
                if (recycler == null)
                {
                    Recycler.EcoLevel = EcoLevelOne;
                    Recycler.TotalPoints = 0;
                    await RecyclerAuth.AddRecycler(Recycler);
                    Username = string.Empty;
                    Password = string.Empty;
                    FullName = string.Empty;
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
                SignUpStatus = "Confirmation password does not matched with your password";
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
            Recycler = null;
            Application.Current.MainPage = new NavigationPage(new Views.MainStartView());
        }

       

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
