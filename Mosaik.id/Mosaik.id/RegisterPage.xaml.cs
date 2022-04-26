using Mosaik.id.Model;
using Mosaik.id.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            supervisorButton.IsEnabled = true;
            userButton.IsEnabled = true;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (height < 740) { rowHeight.Height = 740; }
            else { rowHeight.Height = height; }

            if (width < height) { BodyStackLayout.Padding = new Thickness(0, 20, 0, 0); }
            else { BodyStackLayout.Padding = new Thickness(0, 0, 0, 0); }
        }

        private void UserAccountClicked(object sender, EventArgs e)
        {
            UserAccountButton.BackgroundColor = Color.FromRgba(227, 159, 27, 255);
            UserAccountButton.FontAttributes = FontAttributes.Bold;
            UserAccountTab.IsVisible = true;
            SupervisorAccountButton.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
            SupervisorAccountButton.FontAttributes = FontAttributes.None;
            SupervisorAccountTab.IsVisible = false;
        }

        private void SupervisorAccountClicked(object sender, EventArgs e)
        {
            SupervisorAccountButton.BackgroundColor = Color.FromRgba(227, 159, 27, 255);
            SupervisorAccountButton.FontAttributes = FontAttributes.Bold;
            SupervisorAccountTab.IsVisible = true;
            UserAccountButton.BackgroundColor = Color.FromRgba(0, 0, 0, 0);
            UserAccountButton.FontAttributes = FontAttributes.None;
            UserAccountTab.IsVisible = false;
        }

        private void TCLabelPressed(object sender, EventArgs e)
        {
            TCCheckbox.IsChecked = !TCCheckbox.IsChecked;
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void UserCreateButton(object sender, EventArgs e)
        {
            userButton.IsEnabled = false;

            UserUsernameFrame.BorderColor = Color.Transparent;
            UserEmailFrame.BorderColor = Color.Transparent;
            UserPasswordFrame.BorderColor = Color.Transparent;
            UserRePasswordFrame.BorderColor = Color.Transparent;

            errorUserUsernameLabel.IsVisible = false;
            errorUserEmailLabel.IsVisible = false;
            errorUserPasswordLabel.IsVisible = false;
            errorUserRePasswordLabel.IsVisible = false;
            errorTCLabel.IsVisible = false;

            if (UserUsernameEntry.Text == null || UserUsernameEntry.Text == String.Empty)
            {
                UserUsernameFrame.BorderColor = Color.Red;
                errorUserUsernameLabel.Text = "Please input your username";
                errorUserUsernameLabel.IsVisible = true;
            }
            else if (UserEmailEntry.Text == null || UserEmailEntry.Text == String.Empty)
            {
                UserEmailFrame.BorderColor = Color.Red;
                errorUserEmailLabel.IsVisible = true;
                errorUserEmailLabel.Text = "Please input your email";
            }
            else if (utils.IsValidEmail(UserEmailEntry.Text) == false)
            {
                UserEmailFrame.BorderColor = Color.Red;
                errorUserEmailLabel.IsVisible = true;
                errorUserEmailLabel.Text = "This email is invalid";
            }
            else if (UserPasswordEntry.Text == null || UserPasswordEntry.Text == String.Empty)
            {
                UserPasswordFrame.BorderColor = Color.Red;
                errorUserPasswordLabel.IsVisible = true;
            }
            else if (UserRePasswordEntry.Text != UserPasswordEntry.Text)
            {
                UserRePasswordFrame.BorderColor = Color.Red;
                errorUserRePasswordLabel.IsVisible = true;
            }
            else if (TCCheckbox.IsChecked == false)
            {
                errorTCLabel.IsVisible = true;
            }
            else
            {
                RegisterResponse response = await MosaikAPIService.PostRegisterChild(UserUsernameEntry.Text, UserEmailEntry.Text, UserPasswordEntry.Text);
                if (response.status == "failed")
                {
                    UserEmailFrame.BorderColor = Color.Red;
                    errorUserEmailLabel.IsVisible = true;
                    errorUserEmailLabel.Text = "This email already exist";
                }
                else if (response.status == "success")
                {
                    await Navigation.PopAsync();
                }
                //await Navigation.PopAsync();
            }
            userButton.IsEnabled = true;
        }

        private async void SupervisorCreateButton(object sender, EventArgs e)
        {
            supervisorButton.IsEnabled = false;

            SupervisorUsernameFrame.BorderColor = Color.Transparent;
            SupervisorEmailFrame.BorderColor = Color.Transparent;
            SupervisorPasswordFrame.BorderColor = Color.Transparent;
            SupervisorRePasswordFrame.BorderColor = Color.Transparent;

            errorSupervisorUsernameLabel.IsVisible = false;
            errorSupervisorEmailLabel.IsVisible = false;
            errorSupervisorPasswordLabel.IsVisible = false;
            errorSupervisorRePasswordLabel.IsVisible = false;

            if (SupervisorUsernameEntry.Text == null || SupervisorUsernameEntry.Text == String.Empty)
            {
                SupervisorUsernameFrame.BorderColor = Color.Red;
                errorSupervisorUsernameLabel.IsVisible = true;
            }
            else if (SupervisorEmailEntry.Text == null || SupervisorEmailEntry.Text == String.Empty)
            {
                SupervisorEmailFrame.BorderColor = Color.Red;
                errorSupervisorEmailLabel.IsVisible = true;
                errorSupervisorEmailLabel.Text = "Please input your email";
            }
            else if (utils.IsValidEmail(SupervisorEmailEntry.Text) == false)
            {
                SupervisorEmailFrame.BorderColor = Color.Red;
                errorSupervisorEmailLabel.IsVisible = true;
                errorSupervisorEmailLabel.Text = "This email is invalid";
            }
            else if (SupervisorPasswordEntry.Text == null || SupervisorPasswordEntry.Text == String.Empty)
            {
                SupervisorPasswordFrame.BorderColor = Color.Red;
                errorSupervisorPasswordLabel.IsVisible = true;
            }
            else if (SupervisorRePasswordEntry.Text != SupervisorPasswordEntry.Text)
            {
                SupervisorRePasswordFrame.BorderColor = Color.Red;
                errorSupervisorRePasswordLabel.IsVisible = true;
            }
            else
            {
                EmailCheckResponse response = await MosaikAPIService.GetCheckEmail(SupervisorEmailEntry.Text);
                if (response.status == "exist")
                {
                    SupervisorEmailFrame.BorderColor = Color.Red;
                    errorSupervisorEmailLabel.IsVisible = true;
                    errorSupervisorEmailLabel.Text = "This email already exist";
                }
                else
                {
                    await Navigation.PushAsync(new SignupSupervisorPage(SupervisorUsernameEntry.Text,
                        SupervisorEmailEntry.Text, SupervisorPasswordEntry.Text));
                }
                //await Navigation.PushAsync(new SignupSupervisorPage(SupervisorUsernameEntry.Text,
                //    SupervisorEmailEntry.Text, SupervisorPasswordEntry.Text));
            }
            supervisorButton.IsEnabled = true;
        }
    }
}