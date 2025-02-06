using Barber.Colocho.Enums;
using Mopups.Pages;
using Mopups.Services;

namespace Barber.Colocho.App.Views.Popups;

public partial class DialogPopup : PopupPage
{
    public event EventHandler<bool> Confirm;
    private bool IsConfirm { get; set; }
    public DialogPopup(DialogPopupEnum confirm, string message)
	{
		InitializeComponent();
        lblsubtitle.Text = message;
        if (confirm == DialogPopupEnum.Success)
        {
            lbltitle.Text = "Genial!!";
            skiaLottieView.Source = "success_dialog.svg";
            btnerror.IsVisible = false;
            btnsuccess.IsVisible = true;
        }
        else if (confirm == DialogPopupEnum.Error)
        {
            btnsuccess.IsVisible = false;
            btnerror.IsVisible = true;
            lbltitle.Text = "Error";
            skiaLottieView.Source = "error_dialog.svg";
        }
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Confirm?.Invoke(this, true);
        MopupService.Instance.PopAllAsync(true);
    }

    protected override void OnDisappearing()
    {
        Confirm?.Invoke(this, false);
        base.OnDisappearing();
    }

    protected override bool OnBackButtonPressed()
    {
        return false;
    }
}