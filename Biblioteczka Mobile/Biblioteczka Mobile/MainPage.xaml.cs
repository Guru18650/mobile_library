using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Biblioteczka_Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            ApiHandler.MainPage = this;
            InitializeComponent();
        }

        private void Search_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SearchPage());
        }
        private void ISBN_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ISBNSearch());
        }
        private void Add_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookEditor(new Book()));
        }

        private async void Statistics_Pressed(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistics(await ApiHandler.SearchBooks()));
        }

        private void Quit_Pressed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }
}
