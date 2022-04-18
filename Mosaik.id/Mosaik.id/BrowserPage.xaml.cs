using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserPage : ContentPage
    {
        public BrowserPage(string sauce)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Browser.Source = sauce;
        }
        bool CheckURLValid(String source)
        {
            if (!Regex.IsMatch(source, @"^https?:\/\/", RegexOptions.IgnoreCase))
                source = "https://" + source;
            return Regex.IsMatch(source, @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", RegexOptions.IgnoreCase);
        }
        private void PreviousPage(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            {
                Navigation.PushAsync(new HomePage());
            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }
        private async void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            var X = e.Url;
            Loading.IsVisible = true;
            url.Text = X;
            await App.Database.SavePersonAsync(new Person
            {
                Link = url.Text,
                AccessedTime = DateTime.Now.ToString()
            });
            CekDuluYa(X);
        }
        private void CekDuluYa(string x)
        {
            if (Regex.IsMatch(x, @"xxx", RegexOptions.IgnoreCase))
            {
                Browser.IsVisible = false;
                EhApaTuh.IsVisible = true;
            }
            else
            {
                Browser.IsVisible = true;
                EhApaTuh.IsVisible = false; 
            }
        }
        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Loading.IsVisible = false;
        }

        private void url_Completed(object sender, EventArgs e)
        {

            if (CheckURLValid(url.Text))
            {
                if (!Regex.IsMatch(url.Text, @"^https?:\/\/", RegexOptions.IgnoreCase))
                    url.Text = "https://" + url.Text;
                Browser.Source = url.Text;
            }
            else
            {
                Browser.Source = "https://www.google.com/search?q=" + url.Text;
            }
        }
        protected override bool OnBackButtonPressed()
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            {
                return base.OnBackButtonPressed();
            }
            return true;
        }

        private void HomeButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}