using Mosaik.id.Model;
using Mosaik.id.Service;
using System;
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

        private async void ContinueClicked(object sender, EventArgs e)
        {
            continueButton.IsEnabled = false;
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
                //await Task.Delay(1000);
                LoginResponse response = await MosaikAPIService.PostLogin(emailEntry.Text, passwordEntry.Text);
                if (response.status == "Wrong")
                {
                    emailFrame.BorderColor = Color.Red;
                    errorEmailLabel.Text = "This account doesn't exist or Wrong Password";
                    errorEmailLabel.IsVisible = true;
                }
                else if (response.status == "Failed")
                {
                    emailFrame.BorderColor = Color.Red;
                    errorEmailLabel.Text = "There are currently some problem in the server right now";
                    errorEmailLabel.IsVisible = true;
                }
                else
                {
                    await Navigation.PushAsync(new HomePage(response));
                }
                
            }
            continueButton.IsEnabled = true;
        }
    }
}