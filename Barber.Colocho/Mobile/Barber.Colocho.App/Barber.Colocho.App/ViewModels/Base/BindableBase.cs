using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Barber.Colocho.App.ViewModels.Base
{
    public class BindableBase : INotifyPropertyChanged
    {
        #region Properties
        private bool _isBussy;
        public bool IsBussy
        {
            get => _isBussy;
            set { SetProperty(ref _isBussy, value); }
        }
        #endregion

        #region NotifyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(field, value)) { return false; }

            field = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        #endregion
    }
}
