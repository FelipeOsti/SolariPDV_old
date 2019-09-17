using SolariPDV.Page;
using SolariPDV.Page.Menu;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SolariPDV
{
    public partial class App : Application
    {
        public static App current;

        public string sdsServidorApp = "ec2-18-228-228-28.sa-east-1.compute.amazonaws.com"; //18.229.119.232
        public int nnrPorta = 212;

        public string sdsUsuario { get; set; }
        public string sdsSenha { get; set; }

        public App()
        {
            InitializeComponent();
            current = this;

            MainPage = new MenuPage(); // new NavigationPage(new MenuPage());
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.White;
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Gold;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
