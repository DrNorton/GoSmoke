using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Cirrious.MvvmCross.WindowsCommon.Views;
using GoSmokeMobileUniversal.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GoSmokeMobileUniversal.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EnterPhoneAuthView : MvxWindowsPage
    {
        public EnterPhoneAuthView()
        {
            this.InitializeComponent();
          
        }

        private void Vk_OnNavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            if (args.Uri.ToString().ToLower().Contains("expires_in"))
            {
                var url = new Windows.Foundation.WwwFormUrlDecoder(args.Uri.ToString());
                var token=url[0].Value;
                Debug.WriteLine(url[0]);
               
                var context = (this.DataContext as EnterPhoneAuthViewModel);
                context.OnReceiveToken(token);

            }
            
        }
    }
}
