using SATMovieBrowser.Controlls;
using SATMovieBrowser.HelperClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SATMovieBrowser.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedHomePage : TabbedPage
    {

        List<ContentView> contentViews { get; set; }
        public TabbedHomePage()
        {
            ContentPage mPage = new ContentPage() { Title = "ALL" };
            ContentView content = new MovieListView(0);
            content.ClassId = "0";
            mPage.Content = content;
            mPage.ClassId = "0";
            mPage.Title = "All";

            Children.Add(mPage);

            var GList = App.MovieGenre.ToList();
            foreach (var item in GList)
            {
                // Task.Run(()=>{ 
                mPage = new ContentPage() { Title = item.Value };
                content = new MovieListView(item.Key);
                content.ClassId = item.Key.ToString();
                mPage.Content = content;
                mPage.ClassId = item.Key.ToString();
                mPage.Title = item.Value.ToString();

                Children.Add(mPage);
                //});
            }

            InitializeComponent();

        }
        string currentPageName = "";
        long currentPageID = 0;
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            currentPageName = CurrentPage.Title;
            currentPageID = long.Parse(CurrentPage.ClassId);
            App.CurrentPage = currentPageID;
        }
        private void SearchMovieTapped(object sender, EventArgs e)
        {
            MessagingCenter.Send(new NavigationMessage(currentPageID), "MovieListView:ShowHideSearchPanel");
        }
    }
}