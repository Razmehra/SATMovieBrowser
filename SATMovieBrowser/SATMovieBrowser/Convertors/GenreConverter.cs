using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using System.Linq;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace SATMovieBrowser.Convertors
{
    public class GenreConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<long> genreArray = (List<long>)value;
            string x1 = "";
            foreach (var genre in genreArray)
            {
                // App.MovieGenre.ContainsKey[(int)genre]
                var x = genre;
                x1 = x1 + App.MovieGenre.Where(w => w.Key == genre).FirstOrDefault().Value.ToString() + (genreArray.Count() > 1 ? " | " : "");
                //+ (genreArray.Count()>1? "|":"");
            }
            if (genreArray.Count() > 1)
            {
                x1 = x1.Trim().Substring(0, x1.Trim().Length - 1);
            }

            return x1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
