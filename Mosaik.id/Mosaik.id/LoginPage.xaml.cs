using System;
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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (height < 700) { rowHeight.Height = 600; }
            else { rowHeight.Height = height; }
        }

        private void SignupClicked(object sender, EventArgs e){
            Navigation.PushAsync(new RegisterPage());
        }

        private void ContinueClicked(object sender, EventArgs e)
        {
            emailFrame.BorderColor = Color.Transparent;
            passwordFrame.BorderColor = Color.Transparent;
            errorEmailLabel.IsVisible = false;
            errorPasswordLabel.IsVisible = false;

            if (emailEntry.Text == null || emailEntry.Text == String.Empty)
            {
                emailFrame.BorderColor = Color.Red;
                errorEmailLabel.Text = "Please input your account email first";
                errorEmailLabel.IsVisible = true;
            }
            else if (utils.IsValidEmail(emailEntry.Text) == false)
            {
                emailFrame.BorderColor = Color.Red;
                errorEmailLabel.Text = "Email is Invalid";
                errorEmailLabel.IsVisible = true;
            }
            else if (passwordEntry.Text == null || passwordEntry.Text == String.Empty)
            {
                passwordFrame.BorderColor = Color.Red;
                errorPasswordLabel.IsVisible = true;
            }
            else
            {
                //TODO: navigate ke homePage
                Navigation.PushAsync(new HomePage());
                //tampilkan modal kalo error
            }
        }
    }
}