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
    public partial class RecyclerChooseCollector : ContentPage
    {
        public RecyclerChooseCollector()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //collectorListView.ItemsSource = await CollectorAuth.GetAllCollectors();
        }
    }
}