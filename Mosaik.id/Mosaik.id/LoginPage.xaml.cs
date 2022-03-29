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
            usernameFrame.BorderColor = Color.Transparent;
            passwordFrame.BorderColor = Color.Transparent;
            errorUsernameLabel.IsVisible = false;
            errorPasswordLabel.IsVisible = false;

            if (usernameEntry.Text == null || usernameEntry.Text == String.Empty)
            {
                usernameFrame.BorderColor = Color.Red;
                errorUsernameLabel.IsVisible = true;
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