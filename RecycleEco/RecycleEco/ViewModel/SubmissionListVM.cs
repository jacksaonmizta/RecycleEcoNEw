using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
    class SubmissionListVM : INotifyPropertyChanged
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
				selectedItem = null;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Submission> SubmissionList { get; set; }

		public SubmissionListVM()
		{
			SubmissionList = new ObservableCollection<Submission>();
			GetAllSubmissions();
		}

		private async void GetAllSubmissions()
		{
			SubmissionList = await SubmissionAuth.GetAllSubmissions();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}