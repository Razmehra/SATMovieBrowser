using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SATMovieBrowser.HelperClasses
{
    public static class Utils
    {
        public static string SerializeToJson(object obj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                // var json= ToJson(this LoginInfo[] self) => JsonConvert.SerializeObject(self, Converter1.Settings1);
                return json;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static T DeserializeFromJson<T>(string jsonObj)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(jsonObj);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //internal static class Converter1
        //{
        //    public static readonly JsonSerializerSettings Settings1 = new JsonSerializerSettings
        //    {
        //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
        //        DateParseHandling = DateParseHandling.None,
        //        Converters =
        //    {
        //        new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
        //    },
        //    };
        //}

    }
}
