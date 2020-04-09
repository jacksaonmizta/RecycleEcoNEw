using RecycleEco.Utilities;
using RecycleEco.ViewModel;
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
    public partial class RecyclerViewSubmissions : ContentPage
    {
        public RecyclerViewSubmissions()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            submissionListView.ItemsSource = await SubmissionAuth.GetAllSubmissions();
            
        }
    }
}