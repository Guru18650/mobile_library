using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Biblioteczka_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookEditor : ContentPage
    {
        public static Book book = new Book();

        public BookEditor(Book book2)
        {
            book = book2;
            InitializeComponent();
            ISBNEntry.Text = book.ISBN == 0 ? "" : book.ISBN.ToString();
            TitleEntry.Text = book.Title;
            AuthorEntry.Text = book.Author;
            YearEntry.Text = book.PublishDate;
            LangPicker.SelectedItem = book.Language;
            LengthEntry.Text = book.Length == 0 ? "" : book.Length.ToString();
            PublisherEntry.Text = book.Publisher.ToString();
            FormatPicker.SelectedItem = book.PhysicalFormat;
        }

        private async void Button_Pressed(object sender, EventArgs e)
        {
            try
            {
                book.ISBN = long.Parse(ISBNEntry.Text);
                book.Title = TitleEntry.Text;
                book.Author = AuthorEntry.Text;
                book.Publisher = PublisherEntry.Text;
                book.Language = LangPicker.SelectedItem.ToString();
                book.Length = int.Parse(LengthEntry.Text);
                book.PublishDate = YearEntry.Text;
                book.PhysicalFormat = FormatPicker.SelectedItem.ToString();
                book.Owner = "M";

                if (await ApiHandler.AddBook(book))
                {
                    await DisplayAlert("Success", "Book has been added", "ok");
                    await Navigation.PopAsync();
                    await Navigation.PopAsync();
                    
                }
                else
                    await DisplayAlert("Error", "Something went wrong", "ok");
            } catch (Exception ex)
            {
                await DisplayAlert("Error", "Something went wrong", "ok");
            }
            
        }
    }
}