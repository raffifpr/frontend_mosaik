using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MyProject
{
    public partial class BrowserMosaik : ContentPage
    {

        void Button_Clicked(object sender, EventArgs e)
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
                Browser.Source = link.Text;
                Header.IsVisible = false;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
            }
            else
            {
                Browser.Source = "https://www.google.com/search?q=" + link.Text;
                Header.IsVisible = false;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
            }
            
        }
        
        public BrowserMosaik()
        {
            InitializeComponent();
            var link = new Entry {};
            var url = new Entry { };

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            {
                Header.IsVisible = true;
                Browser.IsVisible = false;
                SearchBar.IsVisible = false;
            }
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
                Browser.GoForward();
        }

        private void Browser_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Loading.IsVisible = true;
            url.Text = e.Url;
        }

        private void Browser_Navigated(object sender, WebNavigatedEventArgs e)
        {
            Loading.IsVisible=false;
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
                Header.IsVisible = false;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
            }
            else
            {
                Browser.Source = "https://www.google.com/search?q=" + url.Text;
                Header.IsVisible = false;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
            }
        }
    }

}
