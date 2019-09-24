using SATMovieBrowser.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SATMovieBrowser.Convertors
{
    public class MovieLenghtConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string jsonString = App.MovieService.GetMovieDetail((long)value).ToString();
            var movieDetail = MovieDetail.FromJson(jsonString);
            long MovieTotalMin = movieDetail.Runtime;
            long TotalHoursMin = MovieTotalMin / 60;
            long TotalHours = int.Parse(TotalHoursMin.ToString());
            long TotalMin = (TotalHoursMin - TotalHours) * 60;

            return $"{TotalHours}h-{TotalMin}m";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
