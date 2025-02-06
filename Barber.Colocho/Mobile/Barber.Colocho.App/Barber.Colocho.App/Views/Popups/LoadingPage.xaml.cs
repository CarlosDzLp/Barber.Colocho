

using Mopups.Pages;

namespace Barber.Colocho.App.Views.Popups;

public partial class LoadingPage : PopupPage
{
    public LoadingPage(string message = null)
	{
		InitializeComponent();
        lblloading.Text = (string.IsNullOrWhiteSpace(message)) ? "Cargando..." : message;
    }
}