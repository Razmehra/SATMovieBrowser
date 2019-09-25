﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using SATMovieBrowser;
//
//    var movieDetail = MovieDetail.FromJson(jsonString);

namespace SATMovieBrowser.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;
    using SATMovieBrowser.ViewModels;
    using System.ComponentModel;

    public partial class MovieDetail: INotifyPropertyChanged
    {
        [J("adult")] public bool Adult { get; set; }
        [J("backdrop_path")] public string BackdropPath { get; set; }
        [J("belongs_to_collection")] public BelongsToCollection BelongsToCollection { get; set; }
        [J("budget")] public long Budget { get; set; }
        [J("genres")] public List<Genre> Genres { get; set; }
        [J("homepage")] public Uri Homepage { get; set; }
        [J("id")] public long Id { get; set; }
        [J("imdb_id")] public string ImdbId { get; set; }
        [J("original_language")] public string OriginalLanguage { get; set; }
        [J("original_title")] public string OriginalTitle { get; set; }
        [J("overview")] public string Overview { get; set; }
        [J("popularity")] public double Popularity { get; set; }
        [J("poster_path")] public string PosterPath { get; set; }
        [J("production_companies")] public List<ProductionCompany> ProductionCompanies { get; set; }
        [J("production_countries")] public List<ProductionCountry> ProductionCountries { get; set; }
        [J("release_date")] public DateTimeOffset ReleaseDate { get; set; }
        [J("revenue")] public long Revenue { get; set; }
        private long _runtime { get; set; }
        [J("runtime")] public long Runtime {
            get { return _runtime; }

            set { _runtime = value;  }
            //Duration = GetDuration(value)
        }
        public string Duration { get; set; }
        [J("spoken_languages")] public List<SpokenLanguage> SpokenLanguages { get; set; }
        
        [J("status")] public string Status { get; set; }
        [J("tagline")] public string Tagline { get; set; }
        [J("title")] public string Title { get; set; }
        [J("video")] public bool Video { get; set; }
        [J("vote_average")] public double VoteAverage { get; set; }
        [J("vote_count")] public long VoteCount { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public string GetDuration(long runtime)
        {
            var totalMinutes = runtime;
           return ("{0:00}h-{1:00}m", totalMinutes / 60, totalMinutes % 60).ToString();
        }
    }

    public partial class BelongsToCollection
    {
        [J("id")] public long Id { get; set; }
        [J("name")] public string Name { get; set; }
        [J("poster_path")] public string PosterPath { get; set; }
        [J("backdrop_path")] public string BackdropPath { get; set; }
    }

    public partial class Genre
    {
        [J("id")] public long Id { get; set; }
        [J("name")] public string Name { get; set; }
    }

    public partial class ProductionCompany
    {
        [J("id")] public long Id { get; set; }
        [J("logo_path")] public string LogoPath { get; set; }
        [J("name")] public string Name { get; set; }
        [J("origin_country")] public string OriginCountry { get; set; }
    }

    public partial class ProductionCountry
    {
        [J("iso_3166_1")] public string Iso3166_1 { get; set; }
        [J("name")] public string Name { get; set; }
    }

    public partial class SpokenLanguage
    {
        [J("iso_639_1")] public string Iso639_1 { get; set; }
        [J("name")] public string Name { get; set; }
    }

    public partial class MovieDetail
    {
        public static MovieDetail FromJson(string json) => JsonConvert.DeserializeObject<MovieDetail>(json, MovieDetailConverter.Settings);
    }

    public static class MovieDetailSerialize
    {
        public static string ToJson(this MovieDetail self) => JsonConvert.SerializeObject(self,MovieDetailConverter.Settings);
    }

    internal static class MovieDetailConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

}
