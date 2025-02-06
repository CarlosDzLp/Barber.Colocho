using Barber.Colocho.App.Db;
using Barber.Colocho.App.Views.Page;
using Barber.Colocho.App.Views.Session;

namespace Barber.Colocho.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Page page = null;
            var token = DbContext.Instance.GetToken();
            if (token == null)
                page = new OnBoardingPage();
            else
                page = new BarberPage();
            return new Window(new NavigationPage(page));
        }
    }
}
