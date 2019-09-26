using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
//using Plugin.CurrentActivity;
using Xamarin.Forms;
using Acr.UserDialogs;
using FFImageLoading.Forms.Platform;
//using Xamarin.Essentials;

namespace SATMovieBrowser.Droid
{
    [Activity(Label = "TMBrowser", Icon = "@drawable/TMBLogo", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            UserDialogs.Init(this);
            base.OnCreate(savedInstanceState);
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.InitImageViewHandler();
            LoadApplication(new App());
        }
    }
}