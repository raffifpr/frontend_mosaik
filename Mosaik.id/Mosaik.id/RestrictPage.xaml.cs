using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosaik.id.Model;
using Mosaik.id.Service;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RestrictPage : ContentPage
    {
        public string email;
        public string child;
        public LoginResponse lr;

        public RestrictPage( LoginResponse loginResponse, string target)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            email = loginResponse.email;
            child = target;
            anak.Text = child;
            lr = loginResponse;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            RestrictedLinkDataResponse response = await MosaikAPIService.PostRestrictedLinkData(child);
            for (int i = 0; i < response.total; i++)
            {
                var checkboxtapped = new TapGestureRecognizer();
                checkboxtapped.Tapped += CheckBoxTapped;

                CheckBox check = new CheckBox { IsChecked = false, Color = Color.Black, };
                check.GestureRecognizers.Add(checkboxtapped);

                Image bell = new Image { };
                if (response.linkAndNotif.notifs[i])
                {
                    bell.Source = "Bell";
                }
                else { bell.Source = "No_Bell"; }

                var belltapped = new TapGestureRecognizer();
                belltapped.Tapped += BellTapped;
                bell.GestureRecognizers.Add(belltapped);
                Frame RestrictedLinkFrame = new Frame
                {
                    CornerRadius = 5,
                    BackgroundColor = Color.White,
                    Margin = new Thickness(20, 0, 0, 0),
                    Content = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                    {
                        check,
                        new Label {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.White,
                            Text = response.linkAndNotif.links[i],
                            FontSize = 15,
                            TextColor = Color.Black,
                        },
                        bell,
                    }
                    }
                };
                RestrictList.Children.Insert(RestrictList.Children.Count, RestrictedLinkFrame);
            }

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (RestrictToAll.IsChecked)
            {
                SupervisedAccount[] x = lr.supervisorAccounts;
                for (int i = 0; i < x.Length; i++)
                {
                    AddNewRestrictedLinkResponse result = await MosaikAPIService.PostAddNewRestrictedLink(x[i].email, restictedlink.Text);
                }
            }
            else
            {
                AddNewRestrictedLinkResponse result = await MosaikAPIService.PostAddNewRestrictedLink(child, restictedlink.Text);
            }
            var checkboxtapped = new TapGestureRecognizer();
            checkboxtapped.Tapped += CheckBoxTapped;

            CheckBox check = new CheckBox { IsChecked = false, Color = Color.Black };
            check.GestureRecognizers.Add(checkboxtapped);

            Image bell = new Image { Source = "Bell" };
            var belltapped = new TapGestureRecognizer();
            belltapped.Tapped += BellTapped;
            bell.GestureRecognizers.Add(belltapped);
            Frame RestrictedLinkFrame = new Frame
            {
                CornerRadius = 5,
                BackgroundColor = Color.White,
                Margin = new Thickness(20, 0, 0, 0),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Children =
                    {
                        check,
                        new Label {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.White,
                            Text = restictedlink.Text,
                            FontSize = 15,
                            TextColor = Color.Black,
                        },
                        bell,
                    }
                }
            };
            RestrictList.Children.Insert(RestrictList.Children.Count, RestrictedLinkFrame);
            restictedlink.Text = "";
            PopUpView.IsVisible = false;
        }
        private void CheckBoxTapped(object sender, EventArgs e)
        {

        }

        private void BellTapped(object sender, EventArgs e)
        {
            var bell = new Image { Source = "Bell" };
            var nobell = new Image { Source = "No_Bell" };
            var current_bell = (Image)sender;
            if (current_bell.Source == bell.Source)
            {

                current_bell.Source = "No_Bell";
            }
            else
            {
                current_bell.Source = "Bell";
            }

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            restictedlink.Text = "";
            PopUpView.IsVisible = false;
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            PopUpView.IsVisible = true;
        }
        public class RestictedLink
        {
            public string Link { get; set; }
        }

        ObservableCollection<RestictedLink> restrict = new ObservableCollection<RestictedLink>();
        public ObservableCollection<RestictedLink> Restricts { get { return restrict; } }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            RestrictToAll.IsChecked = !RestrictToAll.IsChecked;
        }
    }
}