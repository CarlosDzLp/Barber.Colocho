using Mopups.Pages;

namespace Barber.Colocho.App.Views.Popups;

public partial class PermissionPopup : PopupPage
{
	public event EventHandler<bool> OnPermissionPopup;
	public PermissionPopup(string message,string img)
	{
		InitializeComponent();
		var device = DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density;
		var width = device - 80;
		stackView.WidthRequest = width;
		imgpermission.Source = img;
		txtmessage.Text = message;
	}

    private void Button_Clicked(object sender, EventArgs e)
    {
		OnPermissionPopup?.Invoke(this, true);
    }
}