using Barber.Colocho.App.ViewModels.Session;

namespace Barber.Colocho.App.Views.Session;

public partial class ForgotPasswordPage : ContentPage
{
    ForgotPasswordPageViewModel forgotPasswordPageView;
    public ForgotPasswordPage()
	{
		InitializeComponent();
		this.BindingContext = forgotPasswordPageView = new ForgotPasswordPageViewModel();
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
        catch (Exception ex)
        {

        }
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
}