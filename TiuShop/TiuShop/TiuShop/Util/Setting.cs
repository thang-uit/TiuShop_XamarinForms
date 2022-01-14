using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace TiuShop.Util
{
    public static class Setting
    {
        // Light: false | Dark: true
        const bool theme = false;

        public static bool Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
    }
}
