using System;
using System.Collections.Generic;
using System.Text;

using System.Net.Http;
using Newtonsoft.Json;
using SATMovieBrowser.Models;
using Plugin.Connectivity;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections;
using Acr.UserDialogs;

namespace SATMovieBrowser.WebServices
{
    public class WebService
    {
        public string BaseUrl = App.BaseUrl;

        public async Task<string> Login(string[] Params)
        {
            var jsonRequest = new { ClientCode = Params[0], MobileNo = Params[3], loginName = Params[1], password = Params[2] };
            return await Result("POST", "MobileUserLogin", jsonRequest);
        }
        public async Task<string> Logout(string[] Params)
        {
            var jsonRequest = new { SessionId = Params[0] };
            return await Result("POST", "Logout", jsonRequest);
        }
        public async Task<string> GetMovieList(string[] Params = null,string shortby="",bool includeAdult=false,bool include_video=false,long page=1,long with_genres=0)
        {
            var jsonRequest = "";
            //https://api.themoviedb.org/3/discover/movie?api_key=063f08b3f72be1c6376635991af1ece7&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_genres=28
            //string GetString = "3/discover/movie?page=1&include_video=false&include_adult=false&sort_by=popularity.desc&language=en-US&api_key=" + App.ApiKey;
            string GetString = "3/discover/movie?api_key=" + App.ApiKey + "&language=en-US&sort_by="+shortby+ "&include_adult="+includeAdult+ "&include_video="+include_video+ "&page="+page+ "&with_genres="+with_genres;

            var result = await Result("GET", "" + GetString, jsonRequest);
            return result;
        }

        public async Task<string> GetMovieDetail(long movieID)
        {
            var jsonRequest = "";
            string GetString = "3/movie/" + movieID.ToString() + "?api_key=" + App.ApiKey + "&language=en-US";
            var result = await Result("GET", "" + GetString, jsonRequest);
            return result;
        }
        public async Task<string> MovieSearch(string searchText)
        {

            ///search/movie?api_key=063f08b3f72be1c6376635991af1ece7&language=en-US&query=007&page=1&include_adult=false
            var jsonRequest = "";
            string GetString = "3/search/movie/?api_key=" + App.ApiKey + "&language=en-US&query=" + searchText +"&page=1&include_adult=false";
            var result = await Result("GET", "" + GetString, jsonRequest);
            return result;
        }


        //**************************** Reuslt Section *****************************
        private async Task<string> Result(string ServiceType, string XUri, Object jsonRequest)
        {
            if (CrossConnectivity.Current.IsConnected == false) { return "Network Error:No Internet connection available.\n !Turn ON your data connection or connect using wifi then try again."; }


            HttpClient client = new HttpClient();

            try
            {
                client.BaseAddress = new Uri(BaseUrl + XUri);
                var serializedJsonRequest = JsonConvert.SerializeObject(jsonRequest, new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                });
                HttpContent content = new StringContent(serializedJsonRequest, Encoding.UTF8, "application/json");
                var response = ServiceType.Equals("POST") ? await client.PostAsync(client.BaseAddress, content) :
                                                            await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    return result.ToString();
                }
                else
                {
                    return response.IsSuccessStatusCode.ToString();
                }
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message;
            }
            finally
            {
                client.Dispose();
            }

        }

    }
}
