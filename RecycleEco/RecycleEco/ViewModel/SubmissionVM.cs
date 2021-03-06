﻿using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;


namespace RecycleEco.ViewModel
{
    class SubmissionVM : INotifyPropertyChanged
    {
        public const string StatusInitial = "Proposed";
        public const string StatusApproved = "Submitted";

        private bool canAdd;
        public bool CanAdd
        {
            get { return canAdd; }
            set
            {
                canAdd = value;
                OnPropertyChanged();
            }
        }


        //--- update submission 
        private bool canUpdate;

        public bool CanUpdate
        {
            get { return canUpdate; }
            set
            {
                canUpdate = value;
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

        private string updateStatus;
        public string UpdateStatus
        {
            get
            {
                return updateStatus;
            }
            set
            {
                updateStatus = value;
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

        public static Material Material { get; set; }
        public static Submission Submission { get; set; }
        public Recycler Recycler { get; set; }
        public Collector Collector { get; set; }
        public ObservableCollection<Collector> CollectorList { get; set; }

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
                CanAdd = CheckFields();
                selectedCollector = null;
                OnPropertyChanged();
            }
        }

        //Variables-----------------------------------------------------------
        public DateTime SubmittedDate
        {
            get
            {
                return Submission.SubmittedDate;
            }
            set
            {
                Submission.SubmittedDate = value;
                CanAdd = CheckFields();
                OnPropertyChanged();
            }
        }

        public int Weight
        {
            get
            {
                return Submission.Weight;
            }
            set
            {
                Submission.Weight = value;
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
                CanUpdate = Weight > 0 && !string.IsNullOrWhiteSpace(MaterialName);
                CanCreate = CanUpdate && !string.IsNullOrWhiteSpace(RecyclerUsername);
                OnPropertyChanged();
            }
        }



        private bool CheckFields()
        {
            return SelectedCollector != null && SubmittedDate != default(DateTime) && DateTime.Compare(SubmittedDate, DateTime.Today) >= 0;
        }

        public ICommand AddSubmission { get; set; }

        //to update submission of material 
        public ICommand OpenUpdateSubmissionView { get; set; }

        public SubmissionVM()
        {
            CollectorList = new ObservableCollection<Collector>();
            Recycler = new Recycler();
            Collector = new Collector();

            //to open update submission
            OpenUpdateSubmissionView = new Command(OpenUpdateSubmissionExecute);

            AddSubmission = new Command(AddSubmissionExecute);
            if (Material == default(Material))
            {
                Material = new Material();
            }
            if (Submission == default(Submission))
            {
                Submission = new Submission();
            }
            else
            {
                InitializeFromSubmission();
            }
            if (RecyclerVM.Recycler != null && Material != null)
                GetCollectorList();
        }

        private async void GetCollectorList()
        {
            CollectorList = await CollectorAuth.GetCollectorsByUsername(Material.CollectorList);
        }

        private async void AddSubmissionExecute(object obj)
        {
            Submission.SubmissionID = Guid.NewGuid().ToString();
            Submission.Recycler = RecyclerVM.Recycler.Username;
            //Submission.Collector = Collector.Username;
            Submission.Status = StatusInitial;
            Submission.Material = Material.MaterialID;
            Submission.MaterialName = Material.MaterialName;
            Submission.SubmittedDate = SubmittedDate;
            await SubmissionAuth.AddSubmission(Submission);
            await Application.Current.MainPage.DisplayAlert("Success",
                "You have successfully made an appointment with " + Submission.Collector, "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void InitializeFromSubmission()
        {
            Recycler = await RecyclerAuth.GetRecyclerByUsername(Submission.Recycler);
            if (Recycler != null)
                RecyclerUsername = Recycler.Username;
            Collector = await CollectorAuth.GetCollectorByUsername(Submission.Collector);
            Material = await MaterialAuth.GetMaterialById(Submission.Material);
            if (Material != null)
                MaterialName = Material.MaterialName;
        }


        //method to update submission of material
        private async void OpenUpdateSubmissionExecute(object obj)
        {
            UpdateStatus = string.Empty;
            Material material = new Material();
            if (MaterialName.ToLower() == Material.MaterialName.ToLower())
            {
                material = await MaterialAuth.GetMaterialByName(MaterialName);
            }
            if (material != null)
            {
                if (CollectorVM.Collector.MaterialCollection.Contains(material.MaterialID))
                {
                    Material = material;
                    UpdateAllSub();
                    await Application.Current.MainPage.DisplayAlert("Record Submitted Material", "Submission Approved!.", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    UpdateStatus = "This material does not belong in your collection";
                }
            }
            else
            {
                UpdateStatus = "Material for recycling not found!";
            }
        }

        private async void UpdateAllSub()
        {
            Submission.Material = Material.MaterialID;
            Submission.MaterialName = Material.MaterialName;
            Submission.Status = StatusApproved;
            Submission.Points = Weight * Material.PointsPK;
            Submission.ApprovedDate = DateTime.Today;
            Recycler.TotalPoints += Submission.Points;
            UpdateRecyclerLevel();
            await RecyclerAuth.UpdateRecycler(Recycler);
            Collector.TotalPoints += Submission.Points;
            await CollectorAuth.UpdateCollector(Collector);
            await SubmissionAuth.UpdateSubmission(Submission);
        }

        private void UpdateRecyclerLevel()
        {
            if (Recycler.TotalPoints >= 1000)
            {
                Recycler.EcoLevel = RecyclerVM.EcoLevelFour;
            }
            else if (Recycler.TotalPoints >= 500)
            {
                Recycler.EcoLevel = RecyclerVM.EcoLevelThree;
            }
            else if (Recycler.TotalPoints >= 100)
            {
                Recycler.EcoLevel = RecyclerVM.EcoLevelTwo;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}