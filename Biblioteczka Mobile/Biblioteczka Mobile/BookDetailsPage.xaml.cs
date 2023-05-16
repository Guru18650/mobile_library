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
    public partial class BookDetailsPage : ContentPage
    {
        public static Book Gbook;
        

        public BookDetailsPage(Book book, bool DB = true)
        {
            InitializeComponent();
            Gbook = book;
            titleLabel.Text = book.Title;
            authorLabel.Text = book.Author;
            publisherLabel.Text = book.Publisher;
            languageLabel.Text = book.Language;
            lengthLabel.Text = book.Length.ToString();
            publishDateLabel.Text = book.PublishDate;
            physicalFormatLabel.Text = book.PhysicalFormat;
            DBStat.Text = DB ? "Found" : "Not Found";
            // Set the cover image
            bookCoverImage.Source = GetCoverImageUrl(book.Cover.ToString());

        }

        private string GetCoverImageUrl(string id)
        {
            return $"http://covers.openlibrary.org/b/id/{id}-L.jpg";
        }

        private void editButton_Pressed(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BookEditor(Gbook));
        }
    }
}