﻿using System;
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
        private string _searchQuery = "";
        private string FilterType = "popularity.desc";
        private bool _inicallbackFromPagination = false;
        private double _currentPage { get; set; }
        public double CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = (value == 0 ? 1 : value); OnPropertyChanged("CurrentPage"); }
        }


        private double _minPageCount = 0;// { get; set; }
        public double MinPageCount
        {
            get { return _minPageCount; }
            set { _minPageCount = (value == 0 ? 1 : value); OnPropertyChanged("MinPageCount"); }
        }

        private double _maxPageCount = 10;// { get; set; }
        public double MaxPageCount
        {
            get { return _maxPageCount; }
            set { _maxPageCount = (value == 0 ? 10 : value); OnPropertyChanged("MaxPageCount"); }
        }


        private string _pagesinfo { get; set; }
        public string PagesInfo
        {
            get { return _pagesinfo; }
            set
            {
                _pagesinfo = value;
                OnPropertyChanged("PagesInfo");
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
        public ICommand FilterTapped => new Command(() =>
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
                FilterType = filter;
                var result = await webServices.GetMovieList(null, FilterType, false, false, 1, this._gid);
                // var Values=
                var discoverdMovies = MovieDiscoverModel.FromJson(result);

                MovieResult = discoverdMovies.Results;
                await RefreshPagesInformation();
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
            _searchQuery = query;
            WebService MovieService = new WebService();
            var result = await MovieService.MovieSearch(query);
            // var Values=
            if (result.Contains("429")) await App.Current.MainPage.DisplayAlert("Http Error", "Too many request..", "Ok");
            if (result.Contains("False")) return;
            discoverdMovies = MovieDiscoverModel.FromJson(result);
            string genre = App.MovieGenre.Where(w => w.Key == _gid).FirstOrDefault().Value;
            //List<Class1> myList;
            //ObservableCollection<Class1> myOC = new ObservableCollection<Class1>(myList);
            ObservableCollection<Result> rs = new ObservableCollection<Result>(discoverdMovies.Results.Where(w => w.Genres.Contains(genre)));

            MovieResult = rs;// discoverdMovies.Results;
            await RefreshPagesInformation();
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

                    if (ShowSearchPanel)
                    {
                       // return;
                        // MovieResult = Results;
                        string genre = App.MovieGenre.Where(w => w.Key == _gid).FirstOrDefault().Value;
                        //List<Class1> myList;
                        //ObservableCollection<Class1> myOC = new ObservableCollection<Class1>(myList);
                        ObservableCollection<Result> rs = new ObservableCollection<Result>(discoverdMovies.Results.Where(w => w.Genres.Contains(genre)));

                        MovieResult = rs;// discoverdMovies.Results;
                                         // await RefreshPagesInformation();

                    }
                    else
                    {
                        MovieResult = discoverdMovies.Results;
                    }

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
            var result = await webServices.GetMovieList(null, FilterType, false, false, 1, this._gid);
            // var Values=
            var discoverdMovies = MovieDiscoverModel.FromJson(result);

            MovieResult = discoverdMovies.Results;
            // await Task.Run(async () => { await RefreshPagesInformation(); });

            // await RefreshPagesInformation();
            MinPageCount = discoverdMovies.Page > 0 ? 1 : 0;
            MaxPageCount = discoverdMovies.TotalPages;
            CurrentPage = MinPageCount;
            PagesInfo = $"{MinPageCount}/{MaxPageCount}";
            FetchDuration(MovieResult);
        }

        private async Task RefreshPagesInformation()
        {
            MinPageCount = discoverdMovies.Page > 0 ? 1 : 0;
            MaxPageCount = discoverdMovies.TotalPages;
            CurrentPage = MinPageCount;
            PagesInfo = $"{MinPageCount}/{MaxPageCount}";
        }
        public async void UpdatePageInfo()
        {
            try
            {
                PagesInfo = $"{CurrentPage}/{MaxPageCount}";
                if (CurrentPage > 1 || _inicallbackFromPagination)
                {
                    if (ShowSearchPanel)
                    {
                        WebService MovieService = new WebService();
                        var result = await MovieService.MovieSearch(_searchQuery, long.Parse(CurrentPage.ToString()));
                        // var Values=
                        //if (result.Contains("429")) await App.Current.MainPage.DisplayAlert("Http Error", "Too many request..", "Ok");
                        if (result == "False") return;
                        discoverdMovies = MovieDiscoverModel.FromJson(result);
                        string genre = App.MovieGenre.Where(w => w.Key == _gid).FirstOrDefault().Value;
                        //List<Class1> myList;
                        //ObservableCollection<Class1> myOC = new ObservableCollection<Class1>(myList);
                        ObservableCollection<Result> rs = new ObservableCollection<Result>(discoverdMovies.Results.Where(w => w.Genres.Contains(genre)));

                        MovieResult = rs;// discoverdMovies.Results;
                                         // await RefreshPagesInformation();

                    }
                    else
                    {
                        _inicallbackFromPagination = true;
                        //https://api.themoviedb.org/3/discover/movie?api_key=063f08b3f72be1c6376635991af1ece7&language=en-US&sort_by=popularity.desc&include_adult=False&include_video=False&page=2&with_genres=28
                        WebService webServices = new WebService();
                        var result = await webServices.GetMovieList(null, FilterType, false, false, long.Parse(CurrentPage.ToString()), this._gid);
                        // var Values=
                        var discoverdMovies = MovieDiscoverModel.FromJson(result);

                        MovieResult = discoverdMovies.Results;

                    }
                    FetchDuration(MovieResult);
                }
            }
            catch (Exception)
            {


            }
        }

    }
}