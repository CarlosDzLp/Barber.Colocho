using Barber.Colocho.App.Models.User;
using Barber.Colocho.App.ViewModels.Session;

namespace Barber.Colocho.App.Views.Session;

public partial class VerificationCodePage : ContentPage
{
    VerificationCodePageViewModel verificationCodePageView;
    public VerificationCodePage(AddUserModel user)
	{
		InitializeComponent();
		this.verificationCodePageView = new VerificationCodePageViewModel(user);
        this.BindingContext = verificationCodePageView;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(async () =>
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            txtuno.Focus();
        });
    }

    private void MoveFocusToNextEntry(Entry currentEntry)
    {
        if (currentEntry == txtuno)
        {
            txtdos.Text = string.Empty;
            txtdos.Focus();
        }
        else if (currentEntry == txtdos)
        {
            txttres.Text = string.Empty;
            txttres.Focus();
        }
        else if (currentEntry == txttres)
        {
            txtcuatro.Text = string.Empty;
            txtcuatro.Focus();
        }
        else if (currentEntry == txtcuatro)
        {
            txtcinco.Text = string.Empty;
            txtcinco.Focus();
        }
    }

    private void OnEntryCompleted(object sender, TextChangedEventArgs e)
    {
        try
        {
            var currentEntry = sender as Entry;
            string text = currentEntry?.Text;
            if (!string.IsNullOrWhiteSpace(text) && text.Length == 1)
            {
                if (int.TryParse(text, out int result))
                {
                    MoveFocusToNextEntry(currentEntry);
                }
            }
        }
        catch(Exception ex)
        {

        }
    }

    private void TapGestureRecognizer_ResendCode(object sender, TappedEventArgs e)
    {
        try
        {
            txtuno.Focus();
            verificationCodePageView.ResendCodeCommandExecuted();
        }
        catch (Exception ex) 
        {
        }
    }
}