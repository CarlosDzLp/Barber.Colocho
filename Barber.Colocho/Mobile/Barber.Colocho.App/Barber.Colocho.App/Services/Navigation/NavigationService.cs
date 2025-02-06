using Barber.Colocho.App.Views.Page;
using Barber.Colocho.App.Views.Session;

namespace Barber.Colocho.App.Services.Navigation
{
    public class NavigationService
    {
        #region Singleton
        private static NavigationService _instance = null;
        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NavigationService();
                }
                return _instance;
            }
        }
        #endregion

        public async Task GoBack()
        {
            try
            {
                var listNav = App.Current.MainPage.Navigation.NavigationStack.ToList();
                int item = listNav.Count - 1;
                var page = listNav[item];
                App.Current.MainPage.Navigation.RemovePage(page);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task NavigateToPage(Page page)
        {
            try
            {
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
            catch(Exception ex)
            {

            }
        }

        public async Task OnBoarding()
        {
            try
            {
                App.Current.MainPage = new NavigationPage(new OnBoardingPage());
            }
            catch(Exception ex)
            {

            }
        }

        public async Task NavigationRoot()
        {
            try
            {
                await App.Current.MainPage.Navigation.PopToRootAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public async Task OnPrincipal()
        {
            try
            {
                App.Current.MainPage = new NavigationPage(new BarberPage());
            }
            catch (Exception ex)
            {

            }
        }


        //    readonly IServiceProvider _services;

        //    protected INavigation Navigation
        //    {
        //        get
        //        {
        //            INavigation? navigation = Application.Current?.MainPage?.Navigation;
        //            if (navigation is not null)
        //                return navigation;
        //            else
        //            {
        //                if (Debugger.IsAttached)
        //                    Debugger.Break();
        //                throw new Exception();
        //            }
        //        }
        //    }

        //    public NavigationService(IServiceProvider services)
        //    {
        //        _services = services;
        //    }

        //    public Task GoBack()
        //    {
        //        if(Navigation.NavigationStack.Count > 1)
        //            return Navigation.PopAsync();
        //        throw new InvalidOperationException("No pages to navigate back to!");
        //    }

        //    public async Task NavigateToPage<T>(object? parameter = null) where T : Page
        //    {
        //        var toPage = ResolvePage<T>();

        //        if (toPage is not null)
        //        {
        //            toPage.NavigatedTo += Page_NavigatedTo;
        //            var toViewModel = GetPageViewModelBase(toPage);
        //            if (toViewModel is not null)
        //                await toViewModel.OnNavigatingTo(parameter);
        //            await Navigation.PushAsync(toPage, true);
        //            toPage.NavigatedFrom += Page_NavigatedFrom;
        //        }
        //        else
        //            throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        //    }

        //    public T? ResolvePage<T>() where T : Page
        //        => _services.GetService<T>();

        //    private async void Page_NavigatedFrom(object? sender, NavigatedFromEventArgs e)
        //    {
        //        bool isForwardNavigation = Navigation.NavigationStack.Count > 1
        //            && Navigation.NavigationStack[^2] == sender;

        //        if (sender is Page thisPage)
        //        {
        //            if (!isForwardNavigation)
        //            {
        //                thisPage.NavigatedTo -= Page_NavigatedTo;
        //                thisPage.NavigatedFrom -= Page_NavigatedFrom;
        //            }

        //            await CallNavigatedFrom(thisPage, isForwardNavigation);
        //        }
        //    }

        //    private Task CallNavigatedFrom(Page p, bool isForward)
        //    {
        //        var fromViewModel = GetPageViewModelBase(p);

        //        if (fromViewModel is not null)
        //            return fromViewModel.OnNavigatedFrom(isForward);
        //        return Task.CompletedTask;
        //    }

        //    private async void Page_NavigatedTo(object? sender, NavigatedToEventArgs e)
        //        => await CallNavigatedTo(sender as Page);

        //    private Task CallNavigatedTo(Page? p)
        //    {
        //        var fromViewModel = GetPageViewModelBase(p);
        //        if (fromViewModel is not null)
        //            return fromViewModel.OnNavigatedTo();
        //        return Task.CompletedTask;
        //    }

        //    private BindableBase? GetPageViewModelBase(Page? p)
        //        => p?.BindingContext as BindableBase;

        //    public async Task NavigationRoot()
        //    {
        //        await Navigation.PopToRootAsync();
        //    }

        //    public async Task OnPrincipal()
        //    {
        //        var toPage = ResolvePage<BarberPage>();
        //        App.Current.MainPage = new NavigationPage(toPage);
        //    }

        //    public async Task OnBoarding()
        //    {
        //        //var toPage = ResolvePage<OnBoardingPage>();
        //        //toPage.BindingContext = GetPageViewModelBase(toPage);
        //        App.Current.MainPage = new NavigationPage();
        //        await NavigateToPage<OnBoardingPage>();
        //    }
    }
}
