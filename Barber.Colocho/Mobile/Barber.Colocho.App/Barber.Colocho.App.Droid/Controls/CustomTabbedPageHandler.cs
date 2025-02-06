using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.Core.View;
using Barber.Colocho.App.Views.Page;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Platform;
using System.Reflection;
using static Android.Views.ViewGroup;

namespace Barber.Colocho.App.Droid.Controls
{
    public class CustomTabbedPageHandler : Microsoft.Maui.Handlers.TabbedViewHandler
    {
        public CustomTabbedPageHandler()
        {
            
        }

        protected override void ConnectHandler(Android.Views.View platformView)
        {
            base.ConnectHandler(platformView);
            Mapper.AppendToMapping(nameof(CustomTabbedPageHandler), (handler, view) =>
            {
                if (view is BarberPage)
                {
                    FieldInfo tabbedPageManagerFieldInfo = typeof(TabbedPage).GetField("_tabbedPageManager", BindingFlags.NonPublic | BindingFlags.Instance);
                    object tabbedPageManager = tabbedPageManagerFieldInfo?.GetValue(view);

                    FieldInfo tabLayoutFieldInfo = tabbedPageManager?.GetType().GetField("_bottomNavigationView", BindingFlags.NonPublic | BindingFlags.Instance);
                    BottomNavigationView bottomNavigationView = tabLayoutFieldInfo?.GetValue(tabbedPageManager) as BottomNavigationView;

                    if (bottomNavigationView != null)
                    {
                        bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilitySelected;
                        var topShadow = LayoutInflater.From(Context).Inflate(Resource.Layout.view_shadow, null);

                        var layoutParams = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, 5);
                        layoutParams.AddRule(LayoutRules.Above, bottomNavigationView.Id);
                        bottomNavigationView.AddView(topShadow, 1, layoutParams);
                    }
                }
            });
        }
    }

    //public class CustomTabbedPage : TabbedPageRenderer
    //{
    //    public CustomTabbedPage(Context context) : base(context)
    //    {
    //    }

    //    protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
    //    {
    //        base.OnElementChanged(e);

    //        if (!(GetChildAt(0) is ViewGroup layout))
    //            return;

    //        if (!(layout.GetChildAt(1) is BottomNavigationView bottomNavigationView))
    //            return;

    //        bottomNavigationView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilitySelected;
    //        var topShadow = LayoutInflater.From(Context).Inflate(Resource.Layout.view_shadow, null);

    //        var layoutParams = new Android.Widget.RelativeLayout.LayoutParams(LayoutParams.MatchParent, 15);
    //        layoutParams.AddRule(LayoutRules.Above, bottomNavigationView.Id);
    //        layout.AddView(topShadow, 2, layoutParams);
    //    }
    //}
}
