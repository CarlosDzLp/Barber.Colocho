using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Platform;

namespace Barber.Colocho.App.Droid.Effects
{
    public class UnderlinePlatformEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is AppCompatEditText)
            {
                var control = Control as AppCompatEditText;
                control.Background = null;
                control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }

            if(Control is MauiPickerBase)
            {
                var control = Control as AppCompatEditText;
                control.Background = null;
                control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }
        }

        protected override void OnDetached()
        {
            if (Control is AppCompatEditText)
            {
                var control = Control as AppCompatEditText;
                control.Background = null;
                control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }

            if (Control is MauiPickerBase)
            {
                var control = Control as AppCompatEditText;
                control.Background = null;
                control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                control.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            }
        }
    }
}
