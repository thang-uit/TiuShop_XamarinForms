using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TiuShop.Util
{
    public class TheTheme
    {
        public static void SetTheme()
        {
            switch (Setting.Theme)
            {
                case true:
                    {
                        App.Current.UserAppTheme = OSAppTheme.Dark;
                        break;
                    }
                case false:
                    {
                        App.Current.UserAppTheme = OSAppTheme.Light;
                        break;
                    }
                default:
                    {
                        App.Current.UserAppTheme = OSAppTheme.Unspecified;
                        break;
                    }
            }
        }
    }
}
