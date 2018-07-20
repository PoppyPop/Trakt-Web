//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace TraktDl.Web.Models
//{
//    public class Episode
//    {
//        public int SeasonNumber { get; set; }

//        public int EpisodeNumber { get; set; }

//        public string EpisodeName { get; set; }

//        public string EpisodePosterUrl { get; set; }

//        public string ShowPosterUrl { get; set; }

//        public string ShowName { get; set; }

//        public static implicit operator Episode(TraktDl.Business.Shared.Remote.Episode e)
//        {
//            var episode = new Episode
//            {
//                EpisodeNumber = e.EpisodeNumber,
//                SeasonNumber = e.Season.SeasonNumber,
//                EpisodeName = e.Name,
//                EpisodePosterUrl = e.
//            };
//            return episode;
//        }
//    }
//}
