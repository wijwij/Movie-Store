using System;
using System.Collections.Generic;

namespace MovieStore.Core.Models.Response
{
    public class MovieDetailResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  PosterUrl { get; set; }
        public string Tagline { get; set; }
        public int? RunTime { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }
        public string Overview { get; set; }
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public bool IsFavoriteByUser { get; set; }
        public bool IsPurchasedByUser { get; set; }

        public IEnumerable<GenreResponseModel> Genres { get; set; }
        public IEnumerable<CastOverviewResponseModel> Casts { get; set; }
    }
}