using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

namespace Mosaik.id
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Sejarah.ItemsSource = await App.Database.GetPeopleAsync();
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