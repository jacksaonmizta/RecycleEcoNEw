﻿using RecycleEco.Model;
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

		public List<Material> MaterialList { get; set; }
		public ICommand ViewMaterialDetail { get; set; }

		public MaterialListVM()
		{
			ViewMaterialDetail = new Command(ViewMaterialDetailExecute);
			MaterialList = new List<Material>();
			GetAllMaterials();
		}

		private async void ViewMaterialDetailExecute(object obj)
		{
			CollectorListVM.material = (Material)selectedItem;
			await Application.Current.MainPage.Navigation.PushAsync(
				new Views.RecyclerChooseCollector());
		}

		private async void GetAllMaterials()
		{
			MaterialList = await MaterialAuth.GetMaterials();
		}

		private void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}