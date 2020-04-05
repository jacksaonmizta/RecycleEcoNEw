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
    class RecyclerEditProfilePageVM : INotifyPropertyChanged
    {
        public static Recycler Recycler { get; set; }
        public string Username
        {
            get { return Recycler.Username; }
            set
            {
                Recycler.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return Recycler.Password; }
            set
            {
                Recycler.Password = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return Recycler.FullName; }
            set
            {
                Recycler.FullName = value;
                OnPropertyChanged();
            }
        }

        public int TotalPoints
        {
            get { return Recycler.TotalPoints; }
            set
            {
                Recycler.TotalPoints = value;
                OnPropertyChanged();
            }
        }

        public string EcoLevel
        {
            get { return Recycler.EcoLevel; }
            set
            {
                Recycler.EcoLevel = value;
                OnPropertyChanged();
            }
        }

        public ICommand Update { get; set; }

        public RecyclerEditProfilePageVM()
        {
            Update = new Command(UpdateExecute);
        }

        private async void UpdateExecute()
        {
            await RecyclerAuth.UpdateRecycler(Recycler);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
