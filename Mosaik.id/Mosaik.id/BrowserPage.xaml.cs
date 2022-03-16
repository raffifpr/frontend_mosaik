using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowserPage : ContentPage
    {
        public BrowserPage(string sauce)
        {
            InitializeComponent();
            Browser.Source = sauce;
        }
        private void PreviousPage(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            {

            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }
        private async void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Loading.IsVisible = true;
            url.Text = e.Url;
            await App.Database.SavePersonAsync(new Person
            {
                Link = url.Text,
                AccessedTime = DateTime.Now.ToString()
            });
        }
        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Loading.IsVisible = false;
        }

        private void url_Completed(object sender, EventArgs e)
        {
            bool CheckURLValid(String source)
            {
                if (!Regex.IsMatch(source, @"^https?:\/\/", RegexOptions.IgnoreCase))
                    source = "https://" + source;
                return Regex.IsMatch(source, @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]+\.[^\s]{2,}|www\.[a-zA-Z0-9]+\.[^\s]{2,})", RegexOptions.IgnoreCase);
            }
            if (CheckURLValid(url.Text))
            {
                if (!Regex.IsMatch(url.Text, @"^https?:\/\/", RegexOptions.IgnoreCase))
                    url.Text = "https://" + url.Text;
                Browser.Source = url.Text;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
            }
            else
            {
                Browser.Source = "https://www.google.com/search?q=" + url.Text;
            }
        }
    }
}