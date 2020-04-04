using System;
using RecycleEco.Model;
using RecycleEco.Utilities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace RecycleEco.ViewModel
{
    class SubmissionVM : INotifyPropertyChanged
    {

        private object selectedItem;

        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                if (selectedItem == null)
                {
                    OnPropertyChanged();
                }
                ViewSubmissionDetail.Execute(selectedItem);
                selectedItem = null;
                OnPropertyChanged();
            }
        }
        //create Collector object
        public static Submission Submit{ get; set; }
        public static Material Material { get; set; }

        public ObservableCollection<Submission> SubmissionsList { get; set; }
        //check if record was succesffuly create
        private bool createSubmit;
        public bool CreateSubmit
        {
            get
            {
              return Submit.Weight  ;
            }
            set
            {
              Submit.Weight=value;
               OnPropertyChanged();
            }

        }

        public int Points
        {
            get
            {
                return Submit.Points;
            }
            set
            {
                Submit.Points = value;
                OnPropertyChanged();
            }

        }

        public string ActualDate
        {
            get
            {
                return Submit.SubmittedDate;
            }
            set
            {
                String myDate = DateTime.Now.ToString();
                Submit.SubmittedDate = myDate;
                OnPropertyChanged();
            }

        }

        //check if record was succesffuly create
        private bool createSubmit;
        public bool CreateSubmit
        {
            get
            {
                return createSubmit;
            }
            set
            {
                createSubmit = value;
                OnPropertyChanged();
            }
        }

         // check if record was successfully submitted
        private string recordSubmitStatus;
        public string RecordSubmitStatus
        {
            get
            {
                return recordSubmitStatus;
            }
            set
            {
<<<<<<< HEAD
                recordSubmitStatus = value;
=======
                Submit.Points = value;
>>>>>>> 1ff0068172d439d8dc79a5d4451a0410637c7693
                OnPropertyChanged();
            }
        }

<<<<<<< HEAD
        //variables//////////////////////////////////////////////////////////////////////////////


        // Interfaces///////////////////////////////////////////////////////////////////////////////
        public ICommand submitCreated { get; set; }

        // Important methods////////////////////////////////////////////////////////////////////////
=======
        public string Status
        {
            get { return Submit.Status; }
            set
            {
                Submit.Status = value;
                CreateSubmit = CheckFields();
                OnPropertyChanged();
            }
        }
>>>>>>> 1ff0068172d439d8dc79a5d4451a0410637c7693

        private bool CheckFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Submit.SubmissionID)&&
                          !string.IsNullOrWhiteSpace(Submit.Weight) &&
                          !string.IsNullOrWhiteSpace(Submit.Date)&&
                          !string.IsNullOrWhiteSpace(Submit.Status);
            return result;
        }

        public ICommand AddSubmission { get; set; }
        public ICommand ViewSubmissionDetail { get; set; }

        public SubmissionVM()
        {
            AddSubmission = new Command(AddNewSubmissionExecute);
            ViewSubmissionDetail = new Command(ViewSubmissionExecute);
            SubmissionsList = new ObservableCollection<Submission>();
            GetAllSubmissions();
        }

        private async void AddNewSubmissionExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.RecyclerAddSumbission());
        }

        private async void ViewSubmissionExecute(object obj)
        {
            Submit = (Submission)obj;
            await Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerViewSubmissions());
        }

        private async void GetAllSubmissions()
        {
            SubmissionsList = await SubmissionAuth.GetAllSubmissions();
        }

        private void OnPropertyChanged()
        {
            throw new NotImplementedException();
        }

        //this method is important to save data and later can be used to update if necessary
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
