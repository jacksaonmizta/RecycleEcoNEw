﻿using RecycleEco.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecycleEco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RecyclerChooseMaterial : ContentPage
    {
        public RecyclerChooseMaterial()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            materialListView.ItemsSource = await MaterialAuth.GetAllMaterials();
        }
    }
}