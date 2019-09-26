using SATMovieBrowser.Views;
using SATMovieBrowser.WebServices;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SATMovieBrowser
{
    public partial class App : Application
    {
        public static long CurrentPage { get; set; }
        public static string ApiKey { get; set; }
        public static string ApiKey_v4Token { get; set; }
        public static string BaseUrl { get; set; }
        public static string BaseUrl_Image { get; set; }
        public static Dictionary<long, string> MovieGenre { get; set; }
        public static WebService MovieService { get; set; }
        public App()
        {
            ApiKey = "063f08b3f72be1c6376635991af1ece7";
            ApiKey_v4Token = "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwNjNmMDhiM2Y3MmJlMWM2Mzc2NjM1OTkxYWYxZWNlNyIsInN1YiI6IjVkNmM5ZjZiNjU2ODZlNWI5Yjg4ZjEzMCIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.hVFPv62QTwp3p1-LmDrkHjiR5cgpV06xEenFx__-tf8";
            BaseUrl = "https://api.themoviedb.org/";
            BaseUrl_Image = "https://image.tmdb.org/"; //"https://image.tmdb.org/t/p/w500/";
            MovieService = new WebService();
            MovieGenre = new Dictionary<long, string>
            {
                { 28, "Action" },
                { 12, "Adventure" },
                { 16, "Animation" },
                { 35, "Comedy" },
                { 80, "Crime" },
                { 99, "Documentary" },
                { 18, "Drama" },
                { 10751, "Family" },
                { 14, "Fantasy" },
                { 36, "History" },
                { 27, "Horror" },
                { 10402, "Music" },
                { 9648, "Mystery" },
                { 10749, "Romance" },
                { 878, "Science Fiction" },
                { 10770, "TV Movie" },
                { 53, "Thriller" },
                { 10752, "War" },
                { 37, "Western" }
            };

            InitializeComponent();

            MainPage =  new MDHomePage();
           // MainPage = new NavigationPage( new SATMovieBrowser.TestPage());
            // MainPage = new NavigationPage(new SATMovieBrowser.Views.MDHomePage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
