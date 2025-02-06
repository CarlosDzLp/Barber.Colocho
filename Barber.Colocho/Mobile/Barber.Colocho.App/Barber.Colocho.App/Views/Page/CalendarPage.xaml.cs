using Barber.Colocho.App.ViewModels.Pages;

namespace Barber.Colocho.App.Views.Page;

public partial class CalendarPage : ContentPage
{
	CalendarPageViewModel pageViewModel;
    public CalendarPage()
	{
		InitializeComponent();
		this.BindingContext = pageViewModel = new CalendarPageViewModel();
	}
}