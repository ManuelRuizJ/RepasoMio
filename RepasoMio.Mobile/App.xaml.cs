namespace RepasoMio.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Pages.LoginPage());
            //MainPage = new AppShell();
        }
    }
}
