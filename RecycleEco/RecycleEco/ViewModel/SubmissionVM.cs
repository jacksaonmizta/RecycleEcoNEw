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
        public const string StatusProposed = "Proposed";
        public const string StatusSubmitted = "Submitted";

        public static Submission Submission { get; set; }
        public static Material Material { get; set; }
        public Recycler Recycler { get; set; }
        public Collector Collector { get; set; }
        public ObservableCollection<Collector> CollectorList { get; set; }

        private bool canAddSub;
        public bool CanAddsub
        {
            get { return canAddSub; }
            set
            {
                canAddSub = value;
                OnPropertyChanged();
            }
        }

        private bool canCreate;

        public bool CanCreate
        {
            get { return canCreate; }
            set
            {
                canCreate = value;
                OnPropertyChanged();
            }
        }

        private string materialName;

        public string MaterialName
        {
            get
            {
                return materialName;
            }
            set
            {
                materialName = value;
                updateSubmit = Weight > 0 && !string.IsNullOrWhiteSpace(MaterialName);
                CanCreate = updateSubmit && !string.IsNullOrWhiteSpace(RecyclerUsername);
                OnPropertyChanged();
            }
        }

        private string recyclerUsername;

        public string RecyclerUsername
        {
            get
            {
                return recyclerUsername;
            }
            set
            {
                recyclerUsername = value;
                updateSubmit = Weight > 0 && !string.IsNullOrWhiteSpace(MaterialName);
                CanCreate = updateSubmit && !string.IsNullOrWhiteSpace(RecyclerUsername);
                OnPropertyChanged();
            }
        }

        private string createStatus;
        public string CreateStatus
        {
            get
            {
                return createStatus;
            }
            set
            {
                createStatus = value;
                OnPropertyChanged();
            }
        }


        private Collector selectedCollector;
        public Collector SelectedCollector
        {
            get
            {
                return selectedCollector;
            }
            set
            {
                selectedCollector = value;
                if (selectedCollector != null)
                    Submission.Collector = selectedCollector.Username;
                CanAddsub = CheckFields();
                selectedCollector = null;
                OnPropertyChanged();
            }
        }

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
        public static Material material { get; set; }//jun's

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
        public int Weight
        {
            get { return Submit.Weight; }
            set
            {
                Submit.Weight = value;
               // CreateSubmit = CheckFields();
                OnPropertyChanged();
            }
        }

        public DateTime Date
        {
            get { return Submit.SubmittedDate; }
            set
            {
                Submit.SubmittedDate = value;
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
            return SelectedCollector != null &&
                Date != default(DateTime) && 
                DateTime.Compare(Date, DateTime.Today) >= 0;
        }

        public ICommand AddSubmission { get; set; } //jun's
        public ICommand ViewSubmissionDetail { get; set; } //jun's
        public ICommand AddComman { get; private set; } //jun's

        public ICommand ToUpdateSubmission { get; set; }

        public SubmissionVM()
        {
            Submit = new Submission();
            AddSubmission = new Command(AddNewSubmissionExecute, CanAddSub); //go to submission page //jun's
            ViewSubmissionDetail = new Command(ViewSubmissionExecute); //display the submissions //jun's
            SubmissionsList = new ObservableCollection<Submission>(); //jun's
            CollectorList = new ObservableCollection<Collector>(); // display list of collectors in RecyclerChooseCollector
            GetAllCollectors();
            GetAllSubmissions();

            //serena
            // to update submission and edit it with weight
            ToUpdateSubmission = new Command(UpdateSubmissionExecute);
        }

        private bool CanAddSub(object arg)
        {
            return CanAddsub;
        }

        private async void AddNewSubmissionExecute(object obj) //jun's
        {
            CreateStatus = string.Empty;
            Recycler = await RecyclerAuth.GetRecyclerByUsername(RecyclerUsername);
            Material = await MaterialAuth.GetMaterialByName(MaterialName);
            Collector = CollectorVM.Collector;
            if (Recycler == null)
            {
                CreateStatus = "Recycler not found!";
            }
            else
            {
                if (Collector.MaterialCollection.Contains(Material.MaterialID))
                {
                    Submission.Recycler = Recycler.Username;
                    Submission.SubmittedDate = DateTime.Today;
                    Submission.Collector = Collector.Username;
                    Submission.SubmissionID = Guid.NewGuid().ToString();
                    Submission.Status = StatusProposed;
                    Submission.Material = Material.MaterialID;
                    await SubmissionAuth.AddSubmissions(Submission);
                    UpdateSubmissionExecute();
                    await Application.Current.MainPage.DisplayAlert("Record Material Submission", "You have successfully recorded the submission.", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();

                }
                else
                {
                    CreateStatus = "You do not collect this type of material!";
                }

            }            
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

        private async void GetAllCollectors() //Recycler's add submission
        {
            List<Collector> collectorListFB = await CollectorAuth.GetAllCollectors();

            foreach (Collector collector in collectorListFB)
            {
                if (collector.MaterialCollection != null)
                {
                    if (collector.MaterialCollection.Contains(material.MaterialName))
                    {
                        CollectorList.Add(collector);
                    }
                }
            }

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
