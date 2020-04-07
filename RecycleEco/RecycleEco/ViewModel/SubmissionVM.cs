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

        public static Submission Submit { get; set; } //jun's
        public static Material Material { get; set; } //jun's
         
        public ObservableCollection<Submission> SubmissionsList { get; set; } //jun's

        //check if record was succesffuly created
        private bool updateSubmit;
        public bool UpdateSubmit
        {
            get
            {
                return updateSubmit;
            }
            set
            {
                updateSubmit = value;
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
                recordSubmitStatus = value;
                OnPropertyChanged();
            }
        }

// ------- Variables ----------------------------------------------------------------------------------------
        public string SubmissionID
        {
            get { return Submit.SubmissionID; }
            set
            {
                Submit.SubmissionID = value;
                //CreateSubmit = CheckFields();
                OnPropertyChanged();
            }
        }
        public string Weight
        {
            get { return Submit.Weight; }
            set
            {
                Submit.Weight = value;
               // CreateSubmit = CheckFields();
                OnPropertyChanged();
            }
        }

        public DatePicker Date
        {
            get { return Submit.Date; }
            set
            {
                Submit.Date = value;
               // CreateSubmit = CheckFields();
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
              //  CreateSubmit = CheckFields();
                OnPropertyChanged();
            }
        }
// ------- End of Variables ----------------------------------------------------------------------------------------



        private bool CheckFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Submit.SubmissionID) &&
                          !string.IsNullOrWhiteSpace(Submit.Weight) &&
                          !string.IsNullOrWhiteSpace(Submit.Status);
            return result;
        }

        public ICommand AddSubmission { get; set; } //jun's
        public ICommand ViewSubmissionDetail { get; set; } //jun's
        public ICommand AddComman { get; private set; } //jun's

        public ICommand ToUpdateSubmission { get; set; }



        public SubmissionVM()
        {
            Submit = new Submission();
            AddSubmission = new Command(AddNewSubmissionExecute); //go to submission page //jun's
            ViewSubmissionDetail = new Command(ViewSubmissionExecute); //display the submissions //jun's
            SubmissionsList = new ObservableCollection<Submission>(); //jun's
            GetAllSubmissions();

            //serena
            // to update submission and edit it with weight
            ToUpdateSubmission = new Command(UpdateSubmissionExecute);


        }

        private async void AddNewSubmissionExecute(object obj) //jun's
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Views.RecyclerAddSumbission());
        }

        private async void ViewSubmissionExecute(object obj) //jun's
        {
            Submit = (Submission)obj;
            await Application.Current.MainPage.Navigation.PushAsync(
                new Views.RecyclerViewSubmissions());
        }

        private async void GetAllSubmissions() //jun's
        {
            SubmissionsList = await SubmissionAuth.GetAllSubmissions();
        }

       

        //this method is important to save data and later can be used to update if necessary
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Serena

        //to execute update submissions
        private async void UpdateSubmissionExecute()  
        {
            await SubmissionAuth.UpdateSubmission(Submit); 
        }



    }




}
