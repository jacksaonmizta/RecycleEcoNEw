﻿using RecycleEco.Model;
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
    class MaterialVM
    {
        public static Material Material { get; set; }

        private bool canAdd;
        public bool CanAdd
        {
            get
            {
                return canAdd;
            }
            set
            {
                canAdd = value;
                OnPropertyChanged();
            }
        }
        public string MaterialName
        {
            get { return Material.MaterialName; }
            set
            {
                Material.MaterialName = value;
                CanAdd = CheckMaterialFields();
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return Material.Description; }
            set
            {
                Material.Description = value;
                CanAdd = CheckMaterialFields();
                OnPropertyChanged();
            }
        }
        public int PointsPerKg
        {
            get { return Material.PointsPK; }
            set
            {
                Material.PointsPK = value;
                CanAdd = CheckMaterialFields();
                OnPropertyChanged();
            }
        }

        private bool CheckMaterialFields()
        {
            bool result = !string.IsNullOrWhiteSpace(Material.MaterialName) &&
                          !string.IsNullOrWhiteSpace(Material.Description) &&
                          Material.PointsPK > 0;
            return result;
        }

        public ICommand UploadMaterial { get; set; }
        public ICommand UpdateMaterial { get; set; }
        public ICommand DeleteMaterial { get; set; }

        public MaterialVM()
        {
            if (Material == default(Material)) //check
            {
                Material = new Material();
            }
            UploadMaterial = new Command(UploadMaterialExecute, CanAddMaterial);
            UpdateMaterial = new Command(UpdateMaterialExecute, CanAddMaterial);
            DeleteMaterial = new Command(DeleteMaterialExecute);
        }

        private bool CanAddMaterial(object arg)
        {
            return CanAdd;
        }

        private async void UploadMaterialExecute(object obj)
        {
            Material.MaterialID = Guid.NewGuid().ToString();
            await MaterialAuth.AddMaterial(Material);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void UpdateMaterialExecute(object obj)
        {
            await MaterialAuth.UpdateMaterial(Material);
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void DeleteMaterialExecute(object obj)
        {
            await MaterialAuth.DeleteMaterial(Material);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
