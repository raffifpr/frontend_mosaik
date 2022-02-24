using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using System.Collections.ObjectModel;

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
            //this.BindingContext = new MyViewModel();
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
            HistoryList.ItemsSource = history;

            // ObservableCollection allows items to be added after ItemsSource
            // is set and the UI will react to changes
            history.Add(new History { Link = url.Text });
            history.Add(new History { AccessedTime = DateTime.Now.ToString() });
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
        public class History
        {
            public string AccessedTime { get; set; }
            public string Link { get; set; }
        }

        ObservableCollection<History> history = new ObservableCollection<History>();
        public ObservableCollection<History> Historys { get { return history; } }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            Header.IsVisible = false;
            Browser.IsVisible = false;
            SearchBar.IsVisible = false;
            Histori.IsVisible=true;
            HistoryList.IsVisible=true;
        }

        private void HistoryList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = "link";
            try
            {
                tappedItem = ((History)e.Item).Link.ToString();
                Header.IsVisible = false;
                Browser.IsVisible = true;
                SearchBar.IsVisible = true;
                Histori.IsVisible = false;
                Browser.Source = tappedItem;
            }
            catch (Exception ex)
            {
                if (ex is Exception) {
                    
                } 
                else {
                    Header.IsVisible = false;
                    Browser.IsVisible = true;
                    SearchBar.IsVisible = true;
                    Histori.IsVisible = false;
                    tappedItem = ((History)e.Item).Link.ToString(); 
                    Browser.Source = tappedItem; 
                }
            }

        }
        private void Button_Clicked_4(object sender, EventArgs e)
        {
            Header.IsVisible = true;
            Browser.IsVisible = false;
            SearchBar.IsVisible = false;
            Histori.IsVisible = false;
        }
        //public class MyViewModel
        //{
        //    public ICommand NaviCommand { protected set; get; }
        //    public ObservableCollection<History> history { get; set; }
        //    public MyViewModel()
        //    {
        //        history = new ObservableCollection<History>();
        //        NaviCommand = new Command<History>((key) =>
        //        {
        //            History person = key as History;

        //        });
        //        history.Add(new History() { Link = "test1"});
        //    }
        //}
        //public class History
        //{
        //    public string Link { get; set; }
        //}

    }

}
