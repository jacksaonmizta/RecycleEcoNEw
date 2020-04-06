using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecycleEco.Utilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RecycleEco.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MaterialListPage : ContentPage
    {
        public MaterialListPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MyListView.ItemsSource = await MaterialAuth.GetAllMaterials();
        }
    }
}