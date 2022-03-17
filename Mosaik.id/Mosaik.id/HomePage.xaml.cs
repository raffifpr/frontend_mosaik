using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Mosaik.id
{
    public partial class HomePage : ContentPage
    {
        public string sauce = "";
        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            
            var link = new Entry { };
            var url = new Entry { };
        }
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
                sauce = link.Text;
            }
            else
            {
                sauce = "https://www.google.com/search?q=" + link.Text;
            }
            link.Text = "";
            Navigation.PushAsync(new BrowserPage(sauce));
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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
                if (width > height)
                {
                //Landscape
                StackLv2.Margin = new Thickness(30, -620, 30, 0);
                StackLv3.Margin = new Thickness(0, -50, 0, 0);
                StackLv5_2.IsVisible = true;
                StackLv5.IsVisible = false;
                History_Button.Margin = new Thickness(0, 0, 0, 0);
                Title.FontSize = 34;
                Title.Padding = new Thickness(0, 44, 0, 0);
                SubTitle.FontSize = 26;
                SubTitle.Padding = new Thickness(27, 0, 0, 0);
                SubTitile2.FontSize = 18;
                FrameLv1.WidthRequest = 600;
                FrameLv1.MinimumWidthRequest = 600;
                FrameLv1.HeightRequest = 20;
                FrameLv1.MinimumHeightRequest = 20;

                link.WidthRequest = 600;
                link.Margin = new Thickness(0, -25, 0, -25);
            }
                else
                {
                //Portrait
                StackLv2.Margin = new Thickness(30, 36, 30, 0);
                StackLv3.Margin = new Thickness(0, 0, 0, 0);
                StackLv5_2.IsVisible = false;
                StackLv5.IsVisible = true;
                History_Button.Margin = new Thickness(0, 0, 0, 50);
                Title.FontSize = 48;
                Title.Padding = new Thickness(0, 44, 0, 0);
                SubTitle.FontSize = 36;
                SubTitle.Padding = new Thickness(27, 71, 0, 0);
                SubTitile2.FontSize = 22;
                FrameLv1.WidthRequest = 313;
                FrameLv1.MinimumWidthRequest = 313;
                FrameLv1.HeightRequest = 62;
                FrameLv1.MinimumHeightRequest = 40;

                link.WidthRequest = 250;
                link.Margin = new Thickness(0, 0, 0, 0);

                }
            
        }
    }

}
