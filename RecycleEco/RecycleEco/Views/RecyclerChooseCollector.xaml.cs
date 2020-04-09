using RecycleEco.Utilities;
using RecycleEco.ViewModel;

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
            collectorListView.ItemsSource = await CollectorAuth.GetCollectorsByUsername(SubmissionVM.Material.CollectorList);
        }
    }
}