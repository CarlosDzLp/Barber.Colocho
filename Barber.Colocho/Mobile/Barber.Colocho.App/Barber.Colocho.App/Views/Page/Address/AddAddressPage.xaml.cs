using Barber.Colocho.App.Models.Base;
using Barber.Colocho.App.ViewModels.Pages.Address;

namespace Barber.Colocho.App.Views.Page.Address;

public partial class AddAddressPage : ContentPage
{
	AddAddressPageViewModel addAddressPageViewModel;
    public AddAddressPage()
	{
		InitializeComponent();
		this.BindingContext = addAddressPageViewModel = new AddAddressPageViewModel();
	}

    private void Entry_Unfocused(object sender, FocusEventArgs e)
    {
		try
		{
			var ent = sender as Entry;
			if (ent != null) 
			{
				if (!string.IsNullOrWhiteSpace(ent.Text))
				{
					if(ent.Text.Length >=4 &&  ent.Text.Length <=5)
					{
                        addAddressPageViewModel.SearchCodePostal(ent.Text);
                    }
				}
			}
		}
		catch(Exception ex)
		{

		}
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		try
		{
			var picker = sender as Picker;
			if(picker != null)
			{
				if(picker.SelectedItem != null)
				{
					var item = picker.SelectedItem as GenericModel;
					addAddressPageViewModel.IdColony = item.Id;
				}
			}
		}
		catch(Exception ex)
		{

		}
    }

    protected override bool OnBackButtonPressed()
    {
        return addAddressPageViewModel.CloseBottomSheet();
    }
}