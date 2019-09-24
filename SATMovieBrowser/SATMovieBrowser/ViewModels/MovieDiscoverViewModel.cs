using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using SATMovieBrowser.Commands;
using SATMovieBrowser.Models;
using SATMovieBrowser.Views;
using SATMovieBrowser.WebServices;
using Xamarin.Forms;
using TMDbLib.Objects;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using System.Threading.Tasks;
using TMDbLib.Objects.Movies;
using System.Collections.ObjectModel;
using System.Linq;
using SATMovieBrowser.HelperClasses;
using SATMovieBrowser.Controlls;

namespace SATMovieBrowser.ViewModels
{
    class MovieDiscoverViewModel : BaseViewModel
    {
        private bool _SwitchFilterByMostPopularIsfocused { get; set; }
        public bool SwitchFilterByMostPopularIsfocused
        {
            get { return _SwitchFilterByMostPopularIsfocused; }
            set
            {
                _SwitchFilterByMostPopularIsfocused = value;
                OnPropertyChanged("SwitchFilterByMostPopularIsfocused");
            }
        }
        private bool _SwitchFilterByHighestRatedIsfocused { get; set; }
        public bool SwitchFilterByHighestRatedIsfocused
        {
            get { return _SwitchFilterByHighestRatedIsfocused; }
            set
            {
                _SwitchFilterByHighestRatedIsfocused = value;
                OnPropertyChanged("SwitchFilterByHighestRatedIsfocused");
            }
        }

        private bool _FilterByMostPopular_Toggled { get; set; }
        public bool FilterByMostPopular_Toggled
        {
            get { return _FilterByMostPopular_Toggled; }
            set
            {
                _FilterByMostPopular_Toggled = value;
                OnPropertyChanged("FilterByMostPopular_Toggled");

            }
        }
        private bool _FilterByHighestRated_Toggled { get; set; }
        public bool FilterByHighestRated_Toggled
        {
            get { return _FilterByHighestRated_Toggled; }
            set
            {
                _FilterByHighestRated_Toggled = value;
                OnPropertyChanged("FilterByHighestRated_Toggled");
            }
        }

        private long _gid { get; set; }
        private int _ShowFilterPanel { get; set; }

        public int ShowFilterPanel
        {
            get { return _ShowFilterPanel; }
            set { _ShowFilterPanel = value; OnPropertyChanged("ShowFilterPanel"); }
        }

        private bool _ShowSearchPanel { get; set; }

        public bool ShowSearchPanel
        {
            get { return _ShowSearchPanel; }
            set { _ShowSearchPanel = value; OnPropertyChanged("ShowSearchPanel"); }
        }


        private ObservableCollection<Result> _movieResult { get; set; }

        private MovieDiscoverModel discoverdMovies;

        public ObservableCollection<Result> MovieResult
        {
            get { return _movieResult; }
            set { _movieResult = value; OnPropertyChanged("MovieResult"); }
        }

        private string _PosterPath { get; set; }

        public string PosterPath
        {
            get { return _PosterPath; }
            set { _PosterPath = "https://image.tmdb.org/t/p/w500" + value; OnPropertyChanged("PosterPath"); }
        }

        private string _Duration { get; set; }

        public string Duration
        {
            get { return _Duration; }
            set
            {
                _Duration = value;
                OnPropertyChanged("Duration");
            }
        }


        public ICommand LoadMovieCommand { get; set; }
        public ICommand FilterTabbed => new Command(() =>
        {
            ShowFilterPanel = ShowFilterPanel == 0 ? 100 : 0;
        });

        public ICommand ApplyFilter => new Command<string>(async (string filter) =>
        {
            try
            {
                WebService webServices = new WebService();
                filter = "popularity.desc";
                if (FilterByMostPopular_Toggled) filter = "popularity.desc";
                if (FilterByHighestRated_Toggled) filter = "vote_average.desc";
                var result = await webServices.GetMovieList(null, filter, false, false, 1, this._gid);
                // var Values=
                var discoverdMovies = MovieDiscoverModel.FromJson(result);

                MovieResult = discoverdMovies.Results;
                FetchDuration(MovieResult);

            }
            catch (Exception)
            {

                
            }

        });

        public ICommand MovieSearch { get; set; }
        //public ICommand PerformSearch => new Command<string>((string query) =>

        public ICommand PerformSearch => new Command<string>(async (string query) =>
        {
            WebService MovieService = new WebService();
            var result = await MovieService.MovieSearch(query);
            // var Values=
            discoverdMovies = MovieDiscoverModel.FromJson(result);

            MovieResult = discoverdMovies.Results;
            FetchDuration(MovieResult);
        });

        public void FetchDuration(ObservableCollection<Result> result)
        {
            TMDbClient client = new TMDbClient(App.ApiKey);
            Device.BeginInvokeOnMainThread(() =>
            {

                Task.Run(() =>
                {
                    foreach (var item in result)
                    {
                        string genre = item.Genres;

                        Movie movie = client.GetMovieAsync(int.Parse(item.Id.ToString())).Result;
                        if (movie == null)
                        {
                            item.Duration = genre;
                        }
                        else
                        {
                            var totalMinutes = movie.Runtime;
                            string dr = string.Format("{0:0}" + ((totalMinutes / 60) == 0 ? "" : "h") + "{1:0}" + ((totalMinutes % 60) == 0 ? "" : "m"), totalMinutes / 60, totalMinutes % 60);
                            item.Duration = genre + " - " + (!movie.Runtime.HasValue ? "" : dr);
                        }

                        //item.Duration = "";
                    }
                    MovieResult = discoverdMovies.Results;

                });

            });
        }
        //public object SearchResults { get; private set; }

        public MovieDiscoverViewModel(long gid = 0, object MCV = null)
        {
            this._gid = gid;
            MessagingCenter.Subscribe<NavigationMessage>(this, "MovieListView:ShowHideSearchPanel", ShowHideSearchPanel);

            LoadMovieCommand = new Command(LoadMovies);
            // MovieSearch = new Command<string>(OnSearchMovie());

            LoadMovies();
        }

        private void ShowHideSearchPanel(NavigationMessage obj)
        {
            if (this._gid == (long)obj.Options)
            {
                ShowSearchPanel = !ShowSearchPanel;
            }
        }

        public void OnTapped(Result SelectedMovie)
        {
            NavigationPage MoviePage = new NavigationPage(new MovieDetailPage());
            MoviePage.BindingContext = SelectedMovie;
            App.Current.MainPage.Navigation.PushModalAsync(MoviePage);// new MovieDetailPage());
        }

        public async void LoadMovies()
        {
            WebService webServices = new WebService();
            var result = await webServices.GetMovieList(null, "popularity.desc", false, false, 1, this._gid);
            // var Values=
            var discoverdMovies = MovieDiscoverModel.FromJson(result);

            MovieResult = discoverdMovies.Results;
            FetchDuration(MovieResult);
        }
    }
}
