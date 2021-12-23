using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyLoading : Rg.Plugins.Popup.Pages.PopupPage
    {
        public MyLoading()
        {
            InitializeComponent();
        }
    }
}