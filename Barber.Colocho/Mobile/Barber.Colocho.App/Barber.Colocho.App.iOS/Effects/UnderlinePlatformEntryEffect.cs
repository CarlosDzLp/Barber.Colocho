using Microsoft.Maui.Controls.Platform;
using UIKit;

namespace Barber.Colocho.App.iOS.Effects
{
    public class UnderlinePlatformEntryEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            if (Control is UITextField)
            {
                var controls = Control as UITextField;
                controls.BackgroundColor = UIKit.UIColor.Clear;
                controls.Layer.BorderWidth = 0;
                controls.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }

        protected override void OnDetached()
        {
            if (Control is UITextField)
            {
                var controls = Control as UITextField;
                controls.BackgroundColor = UIKit.UIColor.Clear;
                controls.Layer.BorderWidth = 0;
                controls.BorderStyle = UIKit.UITextBorderStyle.None;
            }
        }
    }
}
