using Mosaik.id.Model;
using Mosaik.id.Service;
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
    public partial class SignupSupervisorPage : ContentPage
    {
        string username;
        string email; 
        string password;
        //List<string> supervisedEmail = new List<string>();

        int errorMsgIndex = -1;
        double bodyOrientationHeight = 0;
        double bodyTempHeight = 0;
        double bodyHeightLimit = 280;

        double pageMinHeight = 700;
        public SignupSupervisorPage(string _username, string _email, string _password)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            username = _username;
            email = _email;
            password = _password;
            createButton.IsEnabled = true;
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (height < pageMinHeight) { rowHeight.Height = pageMinHeight; }
            else { rowHeight.Height = height; }

            //body height config
            if (bodyOrientationHeight != 0) { bodyTempHeight -= bodyOrientationHeight; }
            if (width < height)
            {
                bodyOrientationHeight = 280;
            }
            else
            {
                bodyOrientationHeight = 150;
            }
            bodyTempHeight = bodyTempHeight + bodyOrientationHeight;
            if (bodyTempHeight <= bodyHeightLimit) { bodyHeight.Height = bodyTempHeight; }
        }

        private void TCLabelPressed(object sender, EventArgs e)
        {
            TCCheckbox.IsChecked = !TCCheckbox.IsChecked;
        }

        private async void CreateButton(object sender, EventArgs e)
        {
            createButton.IsEnabled = false;

            errorTCLabel.IsVisible = false;
            bool emailError = false;
            if (errorMsgIndex != -1)
            {
                EmailStackLayout.Children.RemoveAt(errorMsgIndex + 1);
            }
            for (int i = 0; i < EmailStackLayout.Children.Count - 2; i++)
            {
                Frame frame = (Frame)EmailStackLayout.Children[i];
                frame.BorderColor = Color.Transparent;
            }

            if (TCCheckbox.IsChecked == false)
            {
                errorTCLabel.IsVisible = true;
            }
            else
            {
                for (int i = 0; i < EmailStackLayout.Children.Count - 2; i++)
                {
                    Frame frame = (Frame)EmailStackLayout.Children[i];
                    StackLayout stackLayout = (StackLayout)frame.Content;
                    Entry entry = (Entry)stackLayout.Children[1];
                    if (entry.Text == null || entry.Text == String.Empty || utils.IsValidEmail(entry.Text) == false)
                    {
                        Label errorMsg = new Label
                        {
                            Text = "This email is invalid",
                            Padding = new Thickness(0, -5, 0, 0),
                            TextColor = Color.Red,
                            FontAttributes = FontAttributes.Italic
                        };
                        frame.BorderColor = Color.Red;
                        emailError = true;
                        EmailStackLayout.Children.Insert(i + 1, errorMsg);
                        errorMsgIndex = i;
                        break;
                    }
                }
                if (emailError == false)
                {
                    string[] supervisedEmail = new string[EmailStackLayout.Children.Count - 2];
                    for (int i = 0; i < EmailStackLayout.Children.Count - 2; i++)
                    {
                        //supervisedEmail[i] = "tes" + i;
                        supervisedEmail[i] = ((Entry)((StackLayout)((Frame)EmailStackLayout.Children[i]).Content).Children[1]).Text;
                    }
                    //String response = await MosaikAPIService.PostRegisterSupervisor(username, email, password, supervisedEmail);
                    //testing.Text = response;
                    RegisterSupervisorResponse response = await MosaikAPIService.PostRegisterSupervisor(username, email, password, supervisedEmail.ToArray());
                    if (response.status.StartsWith("index "))
                    {
                        //testing.Text = response.status;
                        string[] strlist = response.status.Split(' ');
                        testing.Text = strlist[1];
                        //((Frame)EmailStackLayout.Children[indexEmailError]).BorderColor = Color.Red;
                    }
                    else if (response.status == "success")
                    {
                        await Navigation.PopAsync();
                        await Navigation.PopAsync();
                    }
                    //await Navigation.PopAsync();
                    //await Navigation.PopAsync();
                }
            }
            createButton.IsEnabled = true;
        }

        private void LoginClicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
            Navigation.PopAsync();
        }

        private void AddMoreAccount(object sender, EventArgs e)
        {
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += RemoveEntryFrame;
            Image deleteIcon = new Image { Source = "trashIcon" };
            deleteIcon.GestureRecognizers.Add(tapGestureRecognizer);

            Frame emailEntryFrame = new Frame
            {
                CornerRadius = 50,
                BackgroundColor = Color.White,
                Padding = new Thickness(20, 12, 20, 12),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        new Image { Source = "emailIcon" },
                        new Entry {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.White,
                            Placeholder = "childEmail@example.com",
                            Keyboard = Keyboard.Email,
                            FontSize = 15,
                            TextColor = Color.Black,
                            PlaceholderColor = Color.FromRgb(111,111,111)
                        },
                        deleteIcon
                    }
                }
            };
            // add entry email sbg children ke EmailStackLayout
            SkipLabel.IsVisible = false;
            EmailStackLayout.Children.Insert(EmailStackLayout.Children.Count - 2, emailEntryFrame);
            // increase body height if possible
            bodyTempHeight += emailEntryFrame.Height;
            if (bodyTempHeight <= bodyHeightLimit)
            {
                bodyHeight.Height = bodyTempHeight;
                //pageMinHeight += emailEntryFrame.Height;
            }
        }

        private void RemoveEntryFrame(object sender, EventArgs e)
        {
            // find frame to delete
            var img = (Image)sender;
            var frame = (Frame)img.Parent.Parent;

            // decrease body height if needed
            bodyTempHeight -= frame.Height;
            if (bodyTempHeight <= bodyHeightLimit)
            {
                bodyHeight.Height = bodyTempHeight;
                //pageMinHeight -= frame.Height;
            }

            var stacklayout = (StackLayout)frame.Parent;
            stacklayout.Children.Remove(frame);
            if (stacklayout.Children.Count == 2)
            {
                SkipLabel.IsVisible = true;
            }
        }
    }
}