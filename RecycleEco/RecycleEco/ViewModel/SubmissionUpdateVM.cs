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
    class SubmissionUpdateVM :INotifyPropertyChanged
    {
		private Submission selectedItem;
		public Submission SelectedItem
		{
			get
			{
				return selectedItem;
			}
			set
			{
				selectedItem = value;
				if (selectedItem != null)
					//send submission object in the method
					OpenMaterialSubmissionView.Execute(selectedItem);
				selectedItem = null;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<Submission> submissionList;

		public ObservableCollection<Submission> SubmissionList
		{
			get { return submissionList; }
			set
			{
				submissionList = value;
				OnPropertyChanged();
			}
		}
		//ICommand

		public ICommand OpenMaterialSubmissionView { get; set; }

		//Constructor
		public SubmissionUpdateVM()
		{

			SubmissionList = new ObservableCollection<Submission>();
			ViewSubmittedAppointments();

			
			OpenMaterialSubmissionView = new Command<Submission>(OpenSubmissionMaterialExecute);
		}

		private async void ViewSubmittedAppointments()
		{
			
			SubmissionList = await SubmissionAuth.GetProposedSubmissionsByCollector(CollectorVM.Collector);
		}

		private async void ViewAllSubmissions()
		{
			SubmissionList = await SubmissionAuth.GetAllSubmissions();
		}

		// To open page to update submission with weight
		private void OpenSubmissionMaterialExecute(Submission submission)
		{
			SubmissionVM.Submission = submission;
			Application.Current.MainPage.Navigation.PushAsync(new Views.RecordSubMat());
		}


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}

