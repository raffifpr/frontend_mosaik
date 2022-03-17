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

        private void UserCreateButton(object sender, EventArgs e)
        {
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
                errorUserUsernameLabel.IsVisible = true;
            }
            else if (UserEmailEntry.Text == null || UserEmailEntry.Text == String.Empty)
            {
                UserEmailFrame.BorderColor = Color.Red;
                errorUserEmailLabel.IsVisible = true;
                errorUserEmailLabel.Text = "Input your email";
            }
            else if (utils.IsValidEmail(UserEmailEntry.Text) == false)
            {
                UserEmailFrame.BorderColor = Color.Red;
                errorUserEmailLabel.IsVisible = true;
                errorUserEmailLabel.Text = "this email is invalid";
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
                //TODO: bikin akun
                //Tampilin modal berhasil/gagal
                Navigation.PopAsync();
            }
        }

        private void SupervisorCreateButton(object sender, EventArgs e)
        {
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
                errorSupervisorEmailLabel.Text = "Input your email";
            }
            else if (utils.IsValidEmail(SupervisorEmailEntry.Text) == false)
            {
                SupervisorEmailFrame.BorderColor = Color.Red;
                errorSupervisorEmailLabel.IsVisible = true;
                errorSupervisorEmailLabel.Text = "this email is invalid";
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
                Navigation.PushAsync(new SignupSupervisorPage());
            }
        }
    }
}