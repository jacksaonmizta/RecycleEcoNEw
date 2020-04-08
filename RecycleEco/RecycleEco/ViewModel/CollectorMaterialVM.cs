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
    class CollectorMaterialVM : INotifyPropertyChanged

	{

		public static Collector collector { get; set; }
		public static Material material { get; set; }

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
				OpenCollectorMaterialView.Execute(selectedItem);
				//selectedItem = null;
				OnPropertyChanged();
			}
		}


		public CollectorMaterialVM()
		{
			material = new Material();
			
			OpenCollectorMaterialView = new Command(OpenCollectorMaterialExecute);
			CollectorMaterialList = new List<Material>();
			GetAllMaterials();
		}

		private async void GetAllMaterials()
		{
			CollectorMaterialList = await MaterialAuth.GetMaterials();
		}


		private  void OpenCollectorMaterialExecute(object obj)
		{
			collector.MaterialCollection.Add((string)selectedItem);
		}

		public List<Material> CollectorMaterialList { get; set; }
		public ICommand OpenCollectorMaterialView { get; set; }


		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
