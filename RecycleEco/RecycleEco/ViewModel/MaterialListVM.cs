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
    class MaterialListVM : INotifyPropertyChanged
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
				ViewMaterialDetail.Execute(selectedItem);
				selectedItem = null;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Material> MaterialList { get; set; }
		public ICommand ViewMaterialDetail { get; set; }

		public MaterialListVM()
		{
			
			MaterialList = new ObservableCollection<Material>();
			GetAllMaterials();
			ViewMaterialDetail = new Command<Material>(ViewMaterialDetailExecute);
		}

		private  void ViewMaterialDetailExecute(Material m)
		{
			CollectorListVM.material = m;
			Application.Current.MainPage.Navigation.PushAsync(
				new Views.RecyclerChooseCollector());
		}

		private ObservableCollection<Material> availableMaterialList;

		public ObservableCollection<Material> AvailableMaterialList
		{
			get { return availableMaterialList; }
			set
			{
				availableMaterialList = value;
				OnPropertyChanged();
			}
		}




		private async void GetAllMaterials()
		{
			MaterialList = await MaterialAuth.GetAllMaterials();
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
