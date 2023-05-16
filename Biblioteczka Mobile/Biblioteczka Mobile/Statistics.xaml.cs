using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Biblioteczka_Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Statistics : ContentPage
    {
        public Statistics(JObject stats)
        {
            InitializeComponent();
            totalBooks.Text = stats["totalBooksCount"].ToString();
            totalPages.Text = stats["totalPagesCount"].ToString();
            uniqueAuthors.Text = stats["uniquePublishersCount"].ToString();
            uniquePublishers.Text = stats["uniquePublishersCount"].ToString();
            bestPublisher.Text = stats["publisherWithMostBooks"]["Publisher"].ToString()+"; "+stats["publisherWithMostBooks"]["bookCount"].ToString();
            bestAuthor.Text = stats["authorWithMostBooks"]["Author"].ToString()+"; "+stats["authorWithMostBooks"]["bookCount"].ToString();
            polishCount.Text = stats["booksPerLanguage"][0]["bookCount"].ToString();
            englishCount.Text = stats["booksPerLanguage"][1]["bookCount"].ToString();
        }
    }
}