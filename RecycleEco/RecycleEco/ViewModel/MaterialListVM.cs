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
        private Material selectedItem;

        public Material SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                if (selectedItem != null)
                    SubMaterial.Execute(selectedItem);
                selectedItem = null;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Material> MaterialList { get; set; }
        public ICommand SubMaterial { get; set; }
        public MaterialListVM()
        {
            MaterialList = new ObservableCollection<Material>();
            GetAllMaterials();
            SubMaterial = new Command<Material>(RecycleMaterialExecute);
        }

        private async void GetAllMaterials()
        {
            MaterialList = await MaterialAuth.GetAllMaterials();
        }
        private void RecycleMaterialExecute(Material material)
        {
            if (material.CollectorList == null)
            {
                Application.Current.MainPage.DisplayAlert("No Collectors found", 
                    "Unfortunatly, there are no collectors for the selected material", "OK");
            }
            else
            {
                SubmissionVM.Material = material;
                Application.Current.MainPage.Navigation.PushAsync(new Views.RecyclerChooseCollector());
            }         
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
