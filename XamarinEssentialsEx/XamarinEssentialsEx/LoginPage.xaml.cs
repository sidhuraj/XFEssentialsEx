using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace XamarinEssentialsEx
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var userName = Preferences.Get("UserName", "");
            var password = Preferences.Get("Password", "");
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                Navigation.PushAsync(new WelcomePage(userName));
            }

            btnLogin.Clicked += BtnLogin_Clicked;
        }

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (chkRememberMe.IsChecked)
            {
                Preferences.Set("UserName", etUserName.Text);
                
                Preferences.Set("Password", etPassword.Text);

            }
            Navigation.PushAsync(new WelcomePage(etPassword.Text));

        }
    }
}