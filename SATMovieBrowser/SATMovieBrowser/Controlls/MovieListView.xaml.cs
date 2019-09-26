using SATMovieBrowser.HelperClasses;
using SATMovieBrowser.Models;
using SATMovieBrowser.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SATMovieBrowser.Controlls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieListView : ContentView
    {
        public long MVLVID { get; set; }
        MovieDiscoverViewModel VM { get; set; }// = new MovieDiscoverViewModel();

        public MovieListView(long gid = 0)
        {
            MVLVID = gid;
            VM = new MovieDiscoverViewModel(gid, this);
            this.BindingContext = VM;
            InitializeComponent();
        }

        private void MyList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Result tappedMovie = (Result)((ListView)sender).SelectedItem;
            VM.OnTapped(tappedMovie);

        }



        private void ChkFilterByMostPopular_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                VM.FilterByMostPopular_Toggled = true;
                VM.FilterByHighestRated_Toggled = false;
                ChkFilterByHighestRated.IsChecked = false;
            }
        }

        private void ChkFilterByHighestRated_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
            {
                VM.FilterByMostPopular_Toggled = false;
                VM.FilterByHighestRated_Toggled = true;
                ChkFilterByMostPopular.IsChecked = false;
            }
        }

        private void PageNavigator_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            VM.UpdatePageInfo();
        }

    }
}