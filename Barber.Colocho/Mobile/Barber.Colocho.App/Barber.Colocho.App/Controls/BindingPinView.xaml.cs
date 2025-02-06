namespace Barber.Colocho.App.Controls;

public partial class BindingPinView : Grid
{
    private string _display;
    private string _url;

    public BindingPinView(string display, string urlImage)
    {
        InitializeComponent();
        _display = display;
        _url = urlImage;
        BindingContext = this;
    }

    public string Display
    {
        get { return _display; }
    }

    public string Image
    {
        get { return _url; }
    }
}