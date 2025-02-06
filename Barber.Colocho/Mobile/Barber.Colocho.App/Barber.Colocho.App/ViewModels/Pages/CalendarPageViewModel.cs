using Barber.Colocho.App.Services.Client;
using Barber.Colocho.App.Services.Dialog;
using Barber.Colocho.App.Services.Message;
using Barber.Colocho.App.ViewModels.Base;
using System.Globalization;

namespace Barber.Colocho.App.ViewModels.Pages
{
    public class CalendarPageViewModel : BindableBase
    {
        //private readonly IMessage message = new Message();
        private readonly IDialogService dialogService = new DialogService();
        private readonly IServiceClient serviceClient = new ServiceClient();
        
        #region Properties
        public CultureInfo Culture { get; set; }
        public DateTime SelectedDate { get; set; }
        #endregion

        #region Constructor
        public CalendarPageViewModel()
        {
            Culture = new CultureInfo("en-MX");
            SelectedDate = DateTime.Now;
        }
        #endregion
    }
}
