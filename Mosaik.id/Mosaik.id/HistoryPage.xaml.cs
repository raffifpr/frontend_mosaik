using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Plugin.Toast;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        List<Person> temp = new List<Person>();
        public HistoryPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            HistoryList("h");
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Sejarah.ItemsSource = await App.Database.GetPeopleAsync();
            temp = await App.Database.GetPeopleAsync();
        }
        private void HistoryList(string x)
        {
            var datetapped = new TapGestureRecognizer();
            datetapped.Tapped += DateTapped;
            Label date = new Label
            {
                BackgroundColor = Color.White,
                Text = x,
                FontSize = 14,
                TextColor = Color.Black
            };
            date.GestureRecognizers.Add(datetapped);
            ListView list = new ListView
            {
                IsVisible = true,
                ItemsSource = temp,
                ItemTemplate = new DataTemplate(() =>
                {
                    return new TextCell { Text = "{Binding Link}", Detail = "{Binding AccessedTime}" };
                })
            };
            list.ItemTapped += History_ItemTapped;
   
            Frame HistoryDate = new Frame
            {
                CornerRadius = 5,
                BackgroundColor = Color.White,
                Margin = new Thickness(20, 0, 60, 0),
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    WidthRequest = 0,
                    Children =
                    {
                        date,
                        list
                    }
                }
            };
            HistoryData.Children.Insert(HistoryData.Children.Count, HistoryDate);
        }
        private void DateTapped(object sender, EventArgs e)
        {

        }
        private void History_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = "link";
            try
            {
                tappedItem = ((Person)e.Item).Link.ToString();
                Navigation.PushAsync(new BrowserPage(tappedItem));
            }
            catch (Exception ex)
            {
                if (ex is Exception)
                {
                    DisplayAlert(null, tappedItem, "OK");
                }
                else
                {
                    tappedItem = ((Person)e.Item).Link.ToString();
                    DisplayAlert(null, tappedItem, "OK");

                }
            }
        }

        async void ImageButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Remove this account from my supervision", "Do you really want to remove this account from your supervision ? Once you remove this from your supervision, you have to resend another request to be able to supervise again ", "Cancel", "Remove");
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            PopUpView.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopUpView.IsVisible = false;
            //CrossToastPopUp.Current.ShowToastSuccess("Account Removed",Plugin.Toast.Abstractions.ToastLength.Short);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            PopUpView.IsVisible = false;
        }

        private void searchhistory_Completed(object sender, EventArgs e)
        {
            var keyword = searchhistory.Text;
            var getCaf = App.Database.GetSearchResult(keyword);
            var resultCount = getCaf.Result.Count;

            if (resultCount > 0)
            {
                var result = getCaf.Result;
                Sejarah.ItemsSource = result;
            }

        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RestrictPage());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            test.IsVisible = !test.IsVisible;
            var current_state = resize.Source;
            var temp = new Image { Source = "DownArrow.png" };
            if (current_state == temp.Source)
            {
                resize.Source = "UpperArrow.png";
            }
            else
            {
                resize.Source = "DownArrow.png";
            }
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            test1.IsVisible = !test.IsVisible;
        }
        //async void OnButtonClicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(nameEntry.Text) && !string.IsNullOrWhiteSpace(ageEntry.Text))
        //    {
        //        await App.Database.SavePersonAsync(new Person
        //        {
        //            Link = nameEntry.Text,
        //            AccessedTime = ageEntry.Text
        //        });

        //        nameEntry.Text = ageEntry.Text = string.Empty;
        //        collectionView.ItemsSource = await App.Database.GetPeopleAsync();
        //    }
        //}
    }
}