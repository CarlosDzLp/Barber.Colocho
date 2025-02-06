namespace Barber.Colocho.App.Controls;

public partial class ToolBarView : ContentView
{
    public static readonly BindableProperty TitleProperty =
           BindableProperty.Create(nameof(Title), typeof(string), typeof(ToolBarView), string.Empty,
               propertyChanged: (bindable, oldValue, newValue) =>
               {
                   ((ToolBarView)bindable).lbltitle.Text = (string)newValue;
               });

    public static readonly BindableProperty IsVisibleTitleProperty =
           BindableProperty.Create(nameof(IsVisibleTitle), typeof(bool), typeof(ToolBarView), true,
               propertyChanged: (bindable, oldValue, newValue) =>
               {
                   ((ToolBarView)bindable).lbltitle.IsVisible = (bool)newValue;
               });

    public static readonly BindableProperty TitleSizeProperty =
       BindableProperty.Create(nameof(TitleSize), typeof(double), typeof(ToolBarView), (double)16,
           propertyChanged: (bindable, oldValue, newValue) =>
           {
               ((ToolBarView)bindable).lbltitle.FontSize = (double)newValue;
           });
    public static readonly BindableProperty TitleColorProperty =
           BindableProperty.Create(nameof(TitleColor), typeof(Color), typeof(ToolBarView), Color.Parse("#FFFFFF"),
               propertyChanged: (bindable, oldValue, newValue) =>
               {
                   ((ToolBarView)bindable).lbltitle.TextColor = (Color)newValue;
               });
    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public bool IsVisibleTitle
    {
        get { return (bool)GetValue(IsVisibleTitleProperty); }
        set { SetValue(IsVisibleTitleProperty, value); }
    }

    public double TitleSize
    {
        get { return (double)GetValue(TitleSizeProperty); }
        set { SetValue(TitleSizeProperty, value); }
    }
    public Color TitleColor
    {
        get { return (Color)GetValue(TitleColorProperty); }
        set { SetValue(TitleColorProperty, value); }
    }
    public ToolBarView()
	{
		InitializeComponent();
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        try
        {
            var listPage = Navigation.NavigationStack.ToList();
            int countPage = listPage.Count;
            var page = Navigation.NavigationStack[countPage - 1];
            Navigation.RemovePage(page);
        }
        catch(Exception ex)
        {

        }
    }
}