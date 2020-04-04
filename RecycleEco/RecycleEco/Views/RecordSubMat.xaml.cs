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
    public partial class RecordSubMat : ContentPage
    {
        public RecordSubMat()
        {
            InitializeComponent();
        }

        private void MainDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            MainLabel.Text = e.NewDate.ToLongDateString();
        }
    }
}