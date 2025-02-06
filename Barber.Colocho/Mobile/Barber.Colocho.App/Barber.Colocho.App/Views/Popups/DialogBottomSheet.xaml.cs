using Barber.Colocho.Enums;
using Mopups.Pages;
using Mopups.Services;
using SkiaSharp.Extended.UI.Controls;

namespace Barber.Colocho.App.Views.Popups;

public partial class DialogBottomSheet : PopupPage
{
    public event EventHandler<bool> Confirm;
    private bool IsConfirm { get; set; }
    public DialogBottomSheet(ConfirmPopupEnum confirm, string message)
    {
        InitializeComponent();
        lbltitle.Text = message;
        if (confirm == ConfirmPopupEnum.Delete)
        {
            skiaLottieView.Source = new SKFileLottieImageSource()
            {
                File = "trash_icon.json"
            };
        }
        else if (confirm == ConfirmPopupEnum.Save)
        {
            skiaLottieView.Source = new SKFileLottieImageSource()
            {
                File = "saved_icon.json"
            };
        }
        else if (confirm == ConfirmPopupEnum.Danger)
        {
            skiaLottieView.Source = new SKFileLottieImageSource()
            {
                File = "danger_icon.json"
            };
        }
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

    private void Button_Clicked(object sender, EventArgs e)
    {
        Confirm?.Invoke(this, false);
        MopupService.Instance.PopAllAsync(true);
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Confirm?.Invoke(this, true);
        MopupService.Instance.PopAllAsync(true);
    }
}