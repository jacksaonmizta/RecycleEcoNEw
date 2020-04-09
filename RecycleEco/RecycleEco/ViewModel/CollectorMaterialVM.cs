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
    class CollectorMaterialVM : INotifyPropertyChanged

    {
        private Material chooseMaterial;

        public Material ChooseMaterial
        {
            get
            {
                return chooseMaterial;
            }
            set
            {
                chooseMaterial = value;
                if (chooseMaterial != null)
                    AddCollectorMaterial.Execute(chooseMaterial);
                chooseMaterial = null;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Material> collectorMaterialList;

        public ObservableCollection<Material> CollectorMaterialList
        {
            get { return collectorMaterialList; }
            set
            {
                collectorMaterialList = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddCollectorMaterial { get; set; }
        public CollectorMaterialVM()
        {
            collectorMaterialList = new ObservableCollection<Material>();
            GetAllMaterials();
            AddCollectorMaterial = new Command<Material>(OpenCollectorMaterialExecute); ;
        }

        private async void GetAllMaterials()
        {
            CollectorMaterialList = await MaterialAuth.GetMaterialsById(CollectorVM.Collector.MaterialCollection);
        }
        private async void OpenCollectorMaterialExecute(Material material)
        {
            Collector collector = CollectorVM.Collector;
            if (collector.MaterialCollection == null)
                collector.MaterialCollection = new List<string>();
            collector.MaterialCollection.Add(material.MaterialID);
            await CollectorAuth.UpdateCollector(collector);


            if (material.CollectorList == null)
                material.CollectorList = new List<string>();
            material.CollectorList.Add(collector.Username);
            await MaterialAuth.UpdateMaterial(material);
            CollectorMaterialList.Remove(material);

            await Application.Current.MainPage.DisplayAlert("Materials", "Material " + material.MaterialName + " is successfully added into collection.", "OK");
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

