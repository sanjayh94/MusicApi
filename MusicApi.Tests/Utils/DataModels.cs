using MusicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MusicApi.Tests.Utils
{
    static class DataModels
    {
        private static string LoadJsonStringFromFile(string filepath)
        {
            try
            { 
                var path = Path.GetFullPath(filepath);
                var json =  JArray.Parse(File.ReadAllText(path));
                return json.ToString();
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private static List<TrackDTO>? GetTracksDtoFromJson(string jsonString)
        {
            var tracks = JsonConvert.DeserializeObject<List<TrackDTO>>(jsonString);

            return tracks;
        }

        /// <summary>
        /// Used for Tracks Mock data.
        /// Static method that Loads json from sampledata.json and return a List<TrackDTO> object
        /// </summary>
        /// <returns>List<TrackDTO> object</returns>
        public static List<TrackDTO> GetSampleTracksDtoList()
        {
   
            var jsonString = LoadJsonStringFromFile(@"../../../sampledata.json");
            var tracks = GetTracksDtoFromJson(jsonString);

            return tracks;
        }
    }
}
