using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using System.Collections.ObjectModel;
using Mosaik.id.Model;
using Mosaik.id.Service;
//using Plugin.Toast;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class HistoryPage : ContentPage
    {
        public string email;
        public string curr_email;
        public LoginResponse lr;
        private ObservableCollection<GroupedPerson> grouped { get; set; }

        List<Person> temp = new List<Person>();
        public SupervisedAccount[] supervisedAccounts;
        public int curr_child = 0;

        public HistoryPage(LoginResponse loginResponse, string target)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            if (loginResponse.accountStatus == "child")
            {
                blokAnak.IsVisible = false;
                toRestrict.IsVisible = false;
            }
            else
            {
                blokAnak.IsVisible = true;
                toRestrict.IsVisible = true;
                supervisedAccounts = loginResponse.supervisorAccounts;
            }
            //HistoryList("h");
            title.Text = target;
            email = loginResponse.email;
            curr_email = target;
            lr = loginResponse;
            //HistoryList("h");
            grouped = new ObservableCollection<GroupedPerson>();
            //var veggieGroup = new GroupedPerson() { Date = "vegetables"};
            //var fruitGroup = new GroupedPerson() { Date = "fruit"};
            //veggieGroup.Add(new Person() { Link = "celery", AccessedTime = "try ants on a log" });
            //veggieGroup.Add(new Person() { Link = "tomato", AccessedTime = "pairs well with basil" });
            //veggieGroup.Add(new Person() { Link = "zucchini", AccessedTime = "zucchini bread > bannana bread" });
            //veggieGroup.Add(new Person() { Link = "peas", AccessedTime = "like peas in a pod" });
            //fruitGroup.Add(new Person() { Link = "banana", AccessedTime = "available in chip form factor" });
            //fruitGroup.Add(new Person() { Link = "strawberry", AccessedTime = "spring plant" });
            //fruitGroup.Add(new Person() { Link = "cherry", AccessedTime = "topper for icecream" });
            //grouped.Add(veggieGroup); grouped.Add(fruitGroup);
            lstView.ItemsSource = grouped;
            Sejarah.ItemsSource = temp;

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //HistoryDataOnDateResponse result = await MosaikAPIService.PostHistoryDataOnDate("raffifpr@gmail.com", "4/27/2022");
            //Sejarah.ItemsSource = (System.Collections.IEnumerable)result.data;
            //List<Person> data = new List<Person>();
            //for (int i = 0; i < result.total; i++)
            //{
            //    data.Add(new Person { Link = result.data.links[i], AccessedTime = result.data.times[i] });
            //}

            //Sejarah.ItemsSource = data;
            DateOfHistoryDataResponse dateOfHistoryDataResponse = await MosaikAPIService.PostDateOfHistoryData(curr_email);
            for (int i = 0; i < dateOfHistoryDataResponse.total; i++)
            {
                var historydate = dateOfHistoryDataResponse.dateList[i];
                var date = new GroupedPerson() { Date = historydate };
                HistoryDataOnDateResponse check = await MosaikAPIService.PostHistoryDataOnDate(curr_email, historydate);
                for (int j = 0; j < check.total; j++)
                {
                    date.Add(new Person() { Link = check.data.links[j], AccessedTime = check.data.times[j] });
                    temp.Add(new Person() { Link = check.data.links[j], AccessedTime = check.data.times[j] });
                }
                grouped.Add(date);
            }
        }
        private async void Restart(string x)
        {
            grouped = new ObservableCollection<GroupedPerson>();
            temp = new List<Person>();
            DateOfHistoryDataResponse dateOfHistoryDataResponse = await MosaikAPIService.PostDateOfHistoryData(x);
            for (int i = 0; i < dateOfHistoryDataResponse.total; i++)
            {
                var historydate = dateOfHistoryDataResponse.dateList[i];
                var date = new GroupedPerson() { Date = historydate };
                HistoryDataOnDateResponse check = await MosaikAPIService.PostHistoryDataOnDate(x, historydate);
                for (int j = 0; j < check.total; j++)
                {
                    date.Add(new Person() { Link = check.data.links[j], AccessedTime = check.data.times[j] });
                    temp.Add(new Person() { Link = check.data.links[j], AccessedTime = check.data.times[j] });
                }
                grouped.Add(date);
            }
            lstView.ItemsSource = grouped;
            Sejarah.ItemsSource = temp;
        }
        //private void HistoryList(string x)
        //{

        //    ListView list = new ListView
        //    {
        //        IsVisible = false,
        //        ItemsSource = temp,
        //        ItemTemplate = new DataTemplate(() =>
        //        {
        //            return new TextCell { Text = "{Binding Link}", Detail = "{Binding AccessedTime}" };
        //        })
        //    };
        //    list.ItemTapped += History_ItemTapped;
        //    //var datetapped = new TapGestureRecognizer();
        //    //datetapped.Tapped += DateTapped(list);
        //    Label date = new Label
        //    {
        //        BackgroundColor = Color.White,
        //        Text = x,
        //        FontSize = 14,
        //        TextColor = Color.Black
        //    };
        //    if (date.GestureRecognizers.Count % 2 == 1) 
        //    {
        //        list.IsVisible = !list.IsVisible;

        //    };

        //    Frame HistoryDate = new Frame
        //    {
        //        CornerRadius = 5,
        //        BackgroundColor = Color.White,
        //        Margin = new Thickness(20, 0, 60, 0),
        //        Content = new StackLayout
        //        {
        //            Orientation = StackOrientation.Horizontal,
        //            WidthRequest = 0,
        //            Children =
        //            {
        //                date,
        //                list
        //            }
        //        }
        //    };
        //    HistoryData.Children.Insert(HistoryData.Children.Count, HistoryDate);
        //}

        //private EventHandler DateTapped(ListView list)
        //{
        //    list.IsVisible = !list.IsVisible;
        //}

        private void History_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var tappedItem = "link";
            try
            {
                tappedItem = ((Person)e.Item).Link.ToString();
                Navigation.PushAsync(new BrowserPage(tappedItem, lr));
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
            if (curr_email == email) { }
            else
            { PopUpView.IsVisible = true; }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            PopUpView.IsVisible = false;
            RemoveChildResponse response =  await MosaikAPIService.PostRemoveChild(email, curr_email);
            //CrossToastPopUp.Current.ShowToastSuccess("Account Removed",Plugin.Toast.Abstractions.ToastLength.Short);
        
            if (response.status == "success")
            {
                Navigation.PushAsync(new HomePage(lr));
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            PopUpView.IsVisible = false;
        }

        private void searchhistory_Completed(object sender, EventArgs e)
        {
            var keyword = searchhistory.Text;
            //var getCaf = App.Database.GetSearchResult(keyword);
            //var resultCount = getCaf.Result.Count;

            //if (resultCount > 0)
            //{
            //    var result = getCaf.Result;
            //    Sejarah.ItemsSource = result;
            //}
            if (keyword != "")
            {

                lstView.IsVisible = false;
                Sejarah.IsVisible = true;
                Sejarah.ItemsSource = temp.Where(x => x.Link.Contains(keyword.ToLower()));
            }
            else
            {
                lstView.IsVisible = true;
                Sejarah.IsVisible = false;
            }


        }

        private void ImageButton_Clicked_2(object sender, EventArgs e)
        {
            if (curr_email == email) { }
            else
            {
                Navigation.PushAsync(new RestrictPage(lr, curr_email));
            }
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            if (lr.accountStatus == "child")
            {

            }else 
            {
                if (curr_child < lr.supervisorAccounts.Length)
                {
                    user.Text = lr.supervisorAccounts[curr_child].email;
                    title.Text = lr.supervisorAccounts[curr_child].email;
                    curr_email = lr.supervisorAccounts[curr_child].email;
                    curr_child++;
                    Restart(curr_email);
                }else
                {
                    curr_child = 0;
                    user.Text = email;
                    title.Text = "Your search history";
                    curr_email = email;
                    Restart(curr_email);
                }
            }
        }

        //private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        //{
        //    test.IsVisible = !test.IsVisible;
        //    var current_state = resize.Source;
        //    var temp = new Image { Source = "DownArrow.png" };
        //    if (current_state == temp.Source)
        //    {
        //        resize.Source = "UpperArrow.png";
        //    }
        //    else
        //    {
        //        resize.Source = "DownArrow.png";
        //    }
        //}

        //private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        //{
        //    test1.IsVisible = !test.IsVisible;
        //}
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