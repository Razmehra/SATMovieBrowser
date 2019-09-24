//namespace SATMovieBrowser.Models
//{
//    using System;
//    using System.Collections.Generic;

//    using System.Globalization;
//    using Newtonsoft.Json;
//    using Newtonsoft.Json.Converters;

//    public partial class MovieDetailWithVidAndImgModel
//    {
//        [JsonProperty("adult")]
//        public bool Adult { get; set; }

//        [JsonProperty("backdrop_path")]
//        public string BackdropPath { get; set; }

//        [JsonProperty("belongs_to_collection")]
//        public object BelongsToCollection { get; set; }

//        [JsonProperty("budget")]
//        public long Budget { get; set; }

//        [JsonProperty("genres")]
//        public List<Genre> Genres { get; set; }

//        [JsonProperty("homepage")]
//        public Uri Homepage { get; set; }

//        [JsonProperty("id")]
//        public long Id { get; set; }

//        [JsonProperty("imdb_id")]
//        public string ImdbId { get; set; }

//        [JsonProperty("original_language")]
//        public string OriginalLanguage { get; set; }

//        [JsonProperty("original_title")]
//        public string OriginalTitle { get; set; }

//        [JsonProperty("overview")]
//        public string Overview { get; set; }

//        [JsonProperty("popularity")]
//        public double Popularity { get; set; }

//        [JsonProperty("poster_path")]
//        public string PosterPath { get; set; }

//        [JsonProperty("production_companies")]
//        public List<ProductionCompany> ProductionCompanies { get; set; }

//        [JsonProperty("production_countries")]
//        public List<ProductionCountry> ProductionCountries { get; set; }

//        [JsonProperty("release_date")]
//        public DateTimeOffset ReleaseDate { get; set; }

//        [JsonProperty("revenue")]
//        public long Revenue { get; set; }

//        [JsonProperty("runtime")]
//        public long Runtime { get; set; }

//        [JsonProperty("spoken_languages")]
//        public List<SpokenLanguage> SpokenLanguages { get; set; }

//        [JsonProperty("status")]
//        public string Status { get; set; }

//        [JsonProperty("tagline")]
//        public string Tagline { get; set; }

//        [JsonProperty("title")]
//        public string Title { get; set; }

//        [JsonProperty("video")]
//        public bool Video { get; set; }

//        [JsonProperty("vote_average")]
//        public double VoteAverage { get; set; }

//        [JsonProperty("vote_count")]
//        public long VoteCount { get; set; }

//        [JsonProperty("videos")]
//        public Videos Videos { get; set; }

//        [JsonProperty("images")]
//        public Images Images { get; set; }
//    }

//    public partial class Genre
//    {
//        [JsonProperty("id")]
//        public long Id { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }
//    }

//    public partial class Images
//    {
//        [JsonProperty("backdrops")]
//        public List<Backdrop> Backdrops { get; set; }

//        [JsonProperty("posters")]
//        public List<Backdrop> Posters { get; set; }
//    }

//    public partial class Backdrop
//    {
//        [JsonProperty("aspect_ratio")]
//        public double AspectRatio { get; set; }

//        [JsonProperty("file_path")]
//        public string FilePath { get; set; }

//        [JsonProperty("height")]
//        public long Height { get; set; }

//        [JsonProperty("iso_639_1")]
//        public string Iso639_1 { get; set; }

//        [JsonProperty("vote_average")]
//        public double VoteAverage { get; set; }

//        [JsonProperty("vote_count")]
//        public long VoteCount { get; set; }

//        [JsonProperty("width")]
//        public long Width { get; set; }
//    }

//    public partial class ProductionCompany
//    {
//        [JsonProperty("id")]
//        public long Id { get; set; }

//        [JsonProperty("logo_path")]
//        public string LogoPath { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("origin_country")]
//        public string OriginCountry { get; set; }
//    }

//    public partial class ProductionCountry
//    {
//        [JsonProperty("iso_3166_1")]
//        public string Iso3166_1 { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }
//    }

//    public partial class SpokenLanguage
//    {
//        [JsonProperty("iso_639_1")]
//        public string Iso639_1 { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }
//    }

//    public partial class Videos
//    {
//        [JsonProperty("results")]
//        public List<Result> Results { get; set; }
//    }

//    public partial class Result
//    {
//        [JsonProperty("id")]
//        public string Id { get; set; }

//        [JsonProperty("iso_639_1")]
//        public string Iso639_1 { get; set; }

//        [JsonProperty("iso_3166_1")]
//        public string Iso3166_1 { get; set; }

//        [JsonProperty("key")]
//        public string Key { get; set; }

//        [JsonProperty("name")]
//        public string Name { get; set; }

//        [JsonProperty("site")]
//        public string Site { get; set; }

//        [JsonProperty("size")]
//        public long Size { get; set; }

//        [JsonProperty("type")]
//        public string Type { get; set; }
//    }

//    public partial class MovieDetailWithVidAndImgModel
//    {
//        public static MovieDetailWithVidAndImgModel FromJson(string json) => JsonConvert.DeserializeObject<MovieDetailWithVidAndImgModel>(json, SATMovieBrowser.Models.Converter.Settings);
//    }

//    public static class Serialize
//    {
//        public static string ToJson(this MovieDetailWithVidAndImgModel self) => JsonConvert.SerializeObject(self, SATMovieBrowser.Models.Converter.Settings);
//    }

//    internal static class Converter
//    {
//        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
//        {
//            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
//            DateParseHandling = DateParseHandling.None,
//            Converters =
//            {
//                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
//            },
//        };
//    }
//}
