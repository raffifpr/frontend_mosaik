using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void Login_Button_Clicked(object sender, EventArgs e)
        {
            if (username.Text == null || username.Text == "")
            {
                DisplayAlert(null, "Please Enter Your Username", "OK");
            }
            else if (password.Text == null || password.Text == "")
            {
                DisplayAlert(null, "Please Enter Your Password", "OK");
            }
            else if (username.Text.Equals("Admin") && password.Text.Equals("1234"))
            {
                DisplayAlert("ToDo!", "Redirect ke Browser", "OK");
            }
            else
            {
                DisplayAlert(null, "Username doesn't exist or wrong password", "OK");
            }
        }
        private void SignUp_Label_Clicked(object sender, EventArgs e)
        {
            //DisplayAlert("ToDo!", "Redirect ke Sign Up", "OK");
            Navigation.PushAsync(new RegisterPage());
        }

        private void ShowPassword(object sender, EventArgs e)
        {
            password.IsPassword = !password.IsPassword;
            if (showPasswordBtn.Text.Equals("Show"))
            {
                showPasswordBtn.Text = "Hide";
            }
            else
            {
                showPasswordBtn.Text = "Show";
            }
        }
    }
}