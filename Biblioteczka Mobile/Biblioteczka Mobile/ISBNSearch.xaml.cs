using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Biblioteczka_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ISBNSearch : ContentPage
    {
        public ISBNSearch()
        {
            InitializeComponent();
        }

        private async void ScanF(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            await Navigation.PushAsync(scanPage);
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    isbnEntry.Text = result.ToString();
                });
            };
        }

        private async void isbnEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isbnEntry.Text.Length == 13)
            {
                SBtn.IsEnabled = false;
                SBtn.Text = "Searching...";
                await ApiHandler.ShowDetails(long.Parse(isbnEntry.Text), this);
                SBtn.IsEnabled = true;
                SBtn.Text = "Search";
            }

        }

        private async void SBtn_Pressed(object sender, EventArgs e)
        {
            if (isbnEntry.Text.Length == 13 || isbnEntry.Text.Length == 10)
            {
                SBtn.IsEnabled = false;
                SBtn.Text = "Searching...";
                await ApiHandler.ShowDetails(long.Parse(isbnEntry.Text), this);
                SBtn.IsEnabled = true;
                SBtn.Text = "Search";
            }
        }
    }
    }
