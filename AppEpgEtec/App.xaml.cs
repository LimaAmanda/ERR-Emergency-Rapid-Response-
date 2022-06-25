using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppEpgEtec
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.Usuarios.LoginView());
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


        /*static AcessoDB database;
        public static AcessoDB Database
        {
            get
            {
                if (database == null)
                {
                    database =
                        new AcessoDB(Path.Combine(Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData), "AppEpgEtecDb"));
                           
                }
                return database;
            }
        }*/


    }
}
