using Mosaik.id.Model;
using Mosaik.id.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        //private StackLayout sideNavbar;
        private LoginResponse loginResponse;
        public string source = "";
        public HomePage(LoginResponse loginResp = null)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            // HomePage Initial Setup
            if (loginResp != null)
            {
                loginResponse = loginResp;
            }
            emailUser.Text = loginResponse.email;
            usernameUser.Text = loginResponse.username;
            if (loginResponse.accountStatus == "child")
            {
                supervisedAccount.IsVisible = false;
                //if (loginResponse.supervisedRequests.Length > 0)
                //{
                //    OpenRequestOverlayModal(loginResponse.supervisedRequests[0].username, loginResponse.supervisedRequests[0].email);
                //}
            }
            if (loginResponse.accountStatus == "supervisor")
            {
                supervisedAccount.IsVisible = true;
                //for (int i = 0; i < loginResponse.supervisorAccounts.Length; i++)
                //{
                //    AddMoreAccount(loginResponse.supervisorAccounts[i].email, loginResponse.supervisorAccounts[i].username);
                //}
            }
            removeOverlay();
            closeSideNavbar();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (height > 550)
            {
                navbarBodyHeight.Height = new GridLength(1, GridUnitType.Star);
            }
            else
            {
                navbarBodyHeight.Height = 500;
            }
            if (height < width)
            {
                scrollSupervisedAccount.VerticalOptions = LayoutOptions.StartAndExpand;
            }
            else
            {
                scrollSupervisedAccount.VerticalOptions = LayoutOptions.Start;
            }
        }

        private void logOut(object sender, EventArgs e)
        {
            loginResponse = null;
            Navigation.PopAsync();
        }

        // Side Navbar Functions
        // =====================
        private void openNavbar(object sender, EventArgs e)
        {
            openSideNavbar();
            addOverlay();
        }

        private void closeNavbar(object sender, EventArgs e)
        {
            removeOverlay();
            closeSideNavbar();
        }

        private async void addOverlay()
        {
            Overlay.IsVisible = true;
            await Overlay.FadeTo(255, 250, Easing.CubicOut);
        }

        private async void removeOverlay()
        {
            await Overlay.FadeTo(0, 250, Easing.CubicOut);
            Overlay.IsVisible = false;
        }

        private async void openSideNavbar()
        {
            await sideNavbar.TranslateTo(0, 0, 250, Easing.CubicOut);
        }

        private async void closeSideNavbar()
        {
            await sideNavbar.TranslateTo(-DeviceDisplay.MainDisplayInfo.Width * 0.6, 0, 250, Easing.CubicOut);
        }

        // Add More Account Modal Functions
        // ================================
        private void AddMoreAccount(string email, string username)
        {
            StackLayout newAccount = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromHex("#2269AA"),
                Padding = new Thickness(25, 10, 15, 10),
                Children =
                {
                    new Label
                    {
                        Text = username,
                        TextColor = Color.White,
                        FontSize = 15,
                        FontAttributes = FontAttributes.Bold,
                        Padding = new Thickness(0,0,0,-5)
                    },
                    new Label
                    {
                        Text = email,
                        TextColor = Color.White,
                        FontSize = 15,
                        Padding = new Thickness(0,-5,0,0)
                    }
                }
            };

            supervisedAccountStack.Children.Insert(supervisedAccountStack.Children.Count - 1, newAccount);
        }

        private void OpenAddMoreAccountModal(object sender, EventArgs e)
        {
            addAddMoreAccountOverlay();

            // reset pesan error jika ada sblmnya
            newEmail.Text = String.Empty;
            newEmailFrame.BorderColor = Color.FromHex("#0D477C");
            addChildEmailEntryError.IsVisible = false;

            AddMoreAccountModal.IsVisible = true;
        }

        private void CloseAddMoreAccountModal(object sender, EventArgs e)
        {
            removeAddMoreAccountOverlay();
            AddMoreAccountModal.IsVisible = false;
        }

        private async void SendRequestAddMoreAccountModal(object sender, EventArgs e)
        {
            if (newEmail.Text == null || newEmail.Text == String.Empty)
            {
                newEmailFrame.BorderColor = Color.FromHex("#E39F1B");
                addChildEmailEntryError.IsVisible = true;
                addChildEmailEntryError.Text = "Add the child's E-mail first";
            }
            else if (utils.IsValidEmail(newEmail.Text) == false)
            {
                newEmailFrame.BorderColor = Color.FromHex("#E39F1B");
                addChildEmailEntryError.IsVisible = true;
                addChildEmailEntryError.Text = "Child's E-mail is invalid";
            }
            else
            {
                //AddMoreChildResponse response = await MosaikAPIService.PostAddMoreChild(loginResponse.email, newEmail.Text);
                //if (response.status == "don't exist")
                //{
                //    newEmailFrame.BorderColor = Color.FromHex("#E39F1B");
                //    addChildEmailEntryError.IsVisible = true;
                //    addChildEmailEntryError.Text = "Child's E-mail doesn't exist";
                //} 
                //else if (response.status == "success")
                //{
                //    // TODO: Show request sudah dikirim
                //}
                AddMoreAccount(newEmail.Text, "username");
                removeAddMoreAccountOverlay();
                AddMoreAccountModal.IsVisible = false;
            }
        }

        private async void addAddMoreAccountOverlay()
        {
            AddMoreAccountOverlay.IsVisible = true;
            await AddMoreAccountOverlay.FadeTo(255, 250, Easing.CubicOut);
        }

        private async void removeAddMoreAccountOverlay()
        {
            await AddMoreAccountOverlay.FadeTo(0, 250, Easing.CubicOut);
            AddMoreAccountOverlay.IsVisible = false;
        }

        // Change Password Modal Functions
        // ===============================
        private void OpenChangePasswordModal(object sender, EventArgs e)
        {
            ResetChangePasswordForm();
            addChangePasswordOverlay();
            ChangePasswordModal.IsVisible = true;
        }

        private void CloseChangePasswordModal(object sender, EventArgs e)
        {
            removeChangePasswordOverlay();
            ChangePasswordModal.IsVisible = false;
        }

        private void ResetChangePasswordForm()
        {
            prevPasswordFrame.BorderColor = Color.FromHex("#0D477C");
            newPasswordFrame.BorderColor = Color.FromHex("#0D477C");
            RePasswordFrame.BorderColor = Color.FromHex("#0D477C");
            errorPrevPassword.IsVisible = false;
            errorNewPassword.IsVisible = false;
            errorRePassword.IsVisible = false;
            prevPassword.Text = String.Empty;
            newPassword.Text = String.Empty;
            RePassword.Text = String.Empty;
        }

        private async void ChangePassword(object sender, EventArgs e)
        {
            if (prevPassword.Text == null || prevPassword.Text == String.Empty)
            {
                prevPasswordFrame.BorderColor = Color.FromHex("#E39F1B");
                errorPrevPassword.IsVisible = true;
                errorPrevPassword.Text = "Please type the old password";
            }
            else if (newPassword.Text == null || newPassword.Text == String.Empty)
            {
                newPasswordFrame.BorderColor = Color.FromHex("#E39F1B");
                errorNewPassword.IsVisible = true;
                errorNewPassword.Text = "Please type the new password";
            }
            else if (RePassword.Text == null || RePassword.Text == String.Empty)
            {
                RePasswordFrame.BorderColor = Color.FromHex("#E39F1B");
                errorRePassword.IsVisible = true;
                errorRePassword.Text = "Please re-type the new password";
            }
            else if (RePassword.Text != newPassword.Text)
            {
                RePasswordFrame.BorderColor = Color.FromHex("#E39F1B");
                errorRePassword.IsVisible = true;
                errorRePassword.Text = "Re-type password doesn't match";
            }
            else
            {
                ChangePasswordResponse response = await MosaikAPIService.PostChangePassword(loginResponse.email, prevPassword.Text, newPassword.Text);
                if (response.status == "wrong password")
                {
                    prevPasswordFrame.BorderColor = Color.FromHex("#E39F1B");
                    errorPrevPassword.IsVisible = true;
                    errorPrevPassword.Text = "Old password wrong";
                }
                else if (response.status == "success")
                {
                    removeChangePasswordOverlay();
                    ChangePasswordModal.IsVisible = false;
                }
            }
        }

        private async void addChangePasswordOverlay()
        {
            ChangePasswordOverlay.IsVisible = true;
            await ChangePasswordOverlay.FadeTo(255, 250, Easing.CubicOut);
        }

        private async void removeChangePasswordOverlay()
        {
            await ChangePasswordOverlay.FadeTo(0, 250, Easing.CubicOut);
            ChangePasswordOverlay.IsVisible = false;
        }

        // Change Username Modal Functions
        // ===============================
        private void ResetUSernameModal()
        {
            newUsername.Text = String.Empty;
            newUsernameFrame.BorderColor = Color.FromHex("#0D477C");
            errorUsername.IsVisible = false;
        }

        private async void ChangeUsername(object sender, EventArgs e)
        {
            if (newUsername.Text == null || newUsername.Text == String.Empty)
            {
                newUsernameFrame.BorderColor = Color.FromHex("#E39F1B");
                errorUsername.IsVisible = true;
            }
            else
            {
                ChangeUsernameResponse response = await MosaikAPIService.PostChangeUsername(loginResponse.email, newUsername.Text);
                if (response.status == "success")
                {
                    usernameUser.Text = newUsername.Text;
                    loginResponse.username = newUsername.Text;
                }
            }
            usernameUser.Text = newUsername.Text;
            removeChangeUsernameOverlay();
            ChangeUsernameModal.IsVisible = false;
        }

        private void OpenChangeUsernameModal(object sender, EventArgs e)
        {
            ResetUSernameModal();
            addChangeUsernameOverlay();
            curUserText.Text = loginResponse.username;
            ChangeUsernameModal.IsVisible = true;
        }

        private void CloseChangeUsernameModal(object sender, EventArgs e)
        {
            removeChangeUsernameOverlay();
            ChangeUsernameModal.IsVisible = false;
        }

        private async void addChangeUsernameOverlay()
        {
            ChangeUsernameOverlay.IsVisible = true;
            await ChangeUsernameOverlay.FadeTo(255, 250, Easing.CubicOut);
        }

        private async void removeChangeUsernameOverlay()
        {
            await ChangeUsernameOverlay.FadeTo(0, 250, Easing.CubicOut);
            ChangeUsernameOverlay.IsVisible = false;
        }

        // Incoming Supervising Request Modal Functions
        // ============================================
        private async void AcceptRequestOverlayModal(object sender, EventArgs e)
        {
            //SupervisorLinkResponse response = await MosaikAPIService.PostSupervisorLinkAccept(loginResponse.email, reqFromEmail.Text, "accept");
            CloseRequestOverlayModal();
        }

        private async void DenyRequestOverlayModal(object sender, EventArgs e)
        {
            //SupervisorLinkResponse response = await MosaikAPIService.PostSupervisorLinkAccept(loginResponse.email, reqFromEmail.Text, "deny");
            CloseRequestOverlayModal();
        }

        private void OpenRequestOverlayModal(string supervisorUsername, string supervisorEmail)
        {
            reqFromUsername.Text = supervisorUsername;
            reqFromEmail.Text = supervisorEmail;
            addRequestOverlayModal();
            RequestModal.IsVisible = true;
        }

        private void CloseRequestOverlayModal()
        {
            removeRequestOverlayModal();
            RequestModal.IsVisible = false;
            loginResponse.supervisedRequests = loginResponse.supervisedRequests.Where((item, index) => index != 0).ToArray();
            if (loginResponse.supervisedRequests.Length > 0)
            {
                OpenRequestOverlayModal(loginResponse.supervisedRequests[0].username, loginResponse.supervisedRequests[0].email);
            }
        }

        private async void addRequestOverlayModal()
        {
            RequestOverlay.IsVisible = true;
            await RequestOverlay.FadeTo(255, 250, Easing.CubicOut);
        }

        private async void removeRequestOverlayModal()
        {
            await RequestOverlay.FadeTo(0, 250, Easing.CubicOut);
            RequestOverlay.IsVisible = false;
        }
        // Directing to another page
        // =========================
        void GoBrowsing(object sender, EventArgs e)
        {
            bool CheckURLValid(String source)
            {
                if (string.IsNullOrEmpty(source))
                    return false;
                if (!Regex.IsMatch(source, @"^https?:\/\/", RegexOptions.IgnoreCase))
                    source = "https://" + source;
                return Regex.IsMatch(source, @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", RegexOptions.IgnoreCase);
            }
            if (CheckURLValid(link.Text))
            {
                if (!Regex.IsMatch(link.Text, @"^https?:\/\/", RegexOptions.IgnoreCase))
                    link.Text = "https://" + link.Text;
                source = link.Text;
            }
            else
            {
                source = "https://www.google.com/search?q=" + link.Text; 
            }
            link.Text = "";
            Navigation.PushAsync(new BrowserPage(source));
        }

        private void GoToHistoryPage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryPage());
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Exit", "Do you want to exit?", "Yes", "No");
                if (result) await this.Navigation.PopAsync();
            });
            return true;
        }
    }
}
