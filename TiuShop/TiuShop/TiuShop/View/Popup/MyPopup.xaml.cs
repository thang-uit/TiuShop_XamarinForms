using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TiuShop.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPopup : Rg.Plugins.Popup.Pages.PopupPage
    {

        //private SetActionListener setActionListener;

        //public interface SetActionListener
        //{
        //    void setNegativeButton();
        //    void setPositiveButton();
        //}

        public MyPopup(string title, string content, string buttonAction)
        {
            InitializeComponent();

            this.lblTitle.Text = title;
            this.lblContent.Text = content;
            this.btnAction.Text = buttonAction;
        }

        private void btnAction_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}