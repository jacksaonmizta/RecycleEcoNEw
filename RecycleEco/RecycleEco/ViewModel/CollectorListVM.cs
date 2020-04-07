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
    class CollectorListVM : INotifyPropertyChanged
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
				ViewCollectorDetail.Execute(selectedItem);
				selectedItem = null;
				OnPropertyChanged();
			}
		}

		public List<Collector> CollectorList { get; set; }
		public ICommand ViewCollectorDetail { get; set; }

		public CollectorListVM()
		{
			ViewCollectorDetail = new Command(ViewCollectorDetailExecute);
			CollectorList = new List<Collector>();
			GetAllCollectors();
		}

		private async void ViewCollectorDetailExecute(object obj)
		{
			//BookDetailViewModel.Book = (Book)obj;
			await Application.Current.MainPage.Navigation.PushAsync(
				new Views.RecyclerChooseDate());
		}

		private async void GetAllCollectors()
		{
			CollectorList = await CollectorAuth.GetCollectors();
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
