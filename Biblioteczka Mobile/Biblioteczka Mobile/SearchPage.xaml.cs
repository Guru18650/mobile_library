using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Biblioteczka_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        public SearchPage()
        {
            InitializeComponent();
            
        }

        private async void OnBookTappedCommand(object sender, ItemTappedEventArgs e)
        {
            Book selectedBook = e.Item as Book;

            if (selectedBook != null)
            {
                await ApiHandler.ShowDetails(selectedBook.ISBN);
            }
        }

        private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var res = await ApiHandler.SearchBooks(searchBar.Text);
            booksListView.ItemsSource = res;
            if (res != null)
                count.Text = $"Result count: {res.Count()}";
            else
                count.Text = "Result count: 0";
            res = await ApiHandler.SearchBooks(searchBar.Text);
            booksListView.ItemsSource = res;
            if (res != null)
                count.Text = $"Result count: {res.Count()}";
            else
                count.Text = "Result count: 0";
        }
    }
}