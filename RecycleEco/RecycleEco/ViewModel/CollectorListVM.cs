using RecycleEco.Model;
using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RecycleEco.ViewModel
{
	class CollectorListVM : INotifyPropertyChanged
	{
		public static Material material {get;set;}

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

		public ObservableCollection<Collector> CollectorList { get; set; }
		public ICommand ViewCollectorDetail { get; set; }

		public CollectorListVM()
		{
			ViewCollectorDetail = new Command(ViewCollectorDetailExecute);
			CollectorList = new ObservableCollection<Collector>();
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

			private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
