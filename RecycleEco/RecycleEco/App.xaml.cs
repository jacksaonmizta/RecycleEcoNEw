using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RecycleEco.Views;

namespace RecycleEco
{
    public partial class App : Application
    {
        public static string Username { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainStartView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
