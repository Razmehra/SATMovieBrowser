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


        //private void FilterByMostPopular_Toggled(object sender, ToggledEventArgs e)
        //{
        //        FilterByHighestRated.IsToggled = false;
        //}

        //private void FilterByHighestRated_Toggled(object sender, ToggledEventArgs e)
        //{
   
        //        FilterByMostPopular.IsToggled = false;

            
        //}

        //private void FilterByHighestRated_Focused(object sender, FocusEventArgs e)
        //{
        //    VM.SwitchFilterByHighestRatedIsfocused = e.IsFocused;
        //}
        //private void FilterByMostPopular_Focused(object sender, FocusEventArgs e)
        //{
        //    VM.SwitchFilterByMostPopularIsfocused = e.IsFocused;
        //}

        private void ChkFilterByMostPopular_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if(e.Value)
            ChkFilterByHighestRated.Checked = false;
        }

        private void ChkFilterByHighestRated_CheckedChanged(object sender, XLabs.EventArgs<bool> e)
        {
            if(e.Value)
            ChkFilterByMostPopular.Checked = false;
        }
    }
}