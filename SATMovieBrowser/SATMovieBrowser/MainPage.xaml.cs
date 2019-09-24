using SATMovieBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SATMovieBrowser.WebServices;
using SATMovieBrowser.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SATMovieBrowser.HelperClasses;

namespace SATMovieBrowser
{
    public partial class MainPage : ContentPage
    {
        MovieDiscoverViewModel VM = new MovieDiscoverViewModel();
        public MainPage()
        {
            this.BindingContext = VM;// new MovieDiscoverViewModel();
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
        }

        private  void MyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Result tappedMovie =(Result)((ListView)sender).SelectedItem;
            VM.OnTapped(tappedMovie);

        }

        //private async void BtnLoadMovies_Clicked(object sender, EventArgs e)
        //{
        //    WebService webServices = new WebService();
        //    var result = await webServices.GetMovieList();
        //    // var Values=
        //    var discoverMovies = MovieDiscoverModel.FromJson(result);
        //    //var data = Utils.DeserializeFromJson<List<MovieDiscoverModel>>(result);

        //    // ObservableCollection<MovieDiscoverModel> movieDiscovers = new ObservableCollection<MovieDiscoverModel>();



        //}
    }
}
