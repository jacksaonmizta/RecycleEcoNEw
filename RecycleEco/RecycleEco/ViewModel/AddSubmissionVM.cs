using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using RecycleEco.Model;
using RecycleEco.Utilities;
using System.Collections.ObjectModel;

namespace RecycleEco.ViewModel
{
    class AddSubmissionVM : INotifyPropertyChanged
    {

        public const string StatusProposed = "Proposed";
        public const string StatusSubmitted = "Submitted";

        public static Submission Submission { get; set; }
        public static Material Material { get; set; }
        public Recycler Recycler { get; set; }
        public Collector Collector { get; set; }
        public ObservableCollection<Collector> CollectorList { get; set; }

        public static Submission Submit { get; set; }
        public string SubmissionID
        {
            get { return Submit.SubmissionID; }
            set
            {
                Submit.SubmissionID = value;              
                OnPropertyChanged();
            }
        }
        public int Weight
        {
            get { return Submit.Weight; }
            set
            {
                Submit.Weight = value;            
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return Submit.SubmittedDate; }
            set
            {
                Submit.SubmittedDate = value;
                OnPropertyChanged();
            }
        }

        public int Points
        {
            get { return Submit.Points; }
            set
            {
                Submit.Points = value;
                OnPropertyChanged();
            }
        }

        public string Status
        {
            get { return Submit.Status; }
            set
            {
                Submit.Status = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddComman { get; private set; } //jun's

        public AddSubmissionVM()
        {
            Submit = new Submission();
            Submit.Username = App.Username;
            AddComman = new Command(AddNewSubmission); //add a submission //jun's
        }

        private async void AddNewSubmission() //jun's
        {
            
            await SubmissionAuth.AddSubmissions(Submit);
            await Application.Current.MainPage.Navigation.PopAsync();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
