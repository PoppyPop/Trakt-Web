using System;
using System.ComponentModel.DataAnnotations;

namespace TraktDl.Business.Database.SqLite
{
    public class BlackListShowSqLite
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public uint TraktShowId { get; set; }

        [Required]
        public bool Entire { get; set; }

        public string SerieName { get; set; }

        public int? Season { get; set; }

        public BlackListShowSqLite()
        {

        }

        public BlackListShowSqLite(Shared.Database.BlackListShow show)
        {
            Id = show.Id ?? Guid.NewGuid();

            TraktShowId = show.TraktShowId;
            SerieName = show.SerieName;

            Season = show.Season;
            Entire = show.Entire;
        }

        public Shared.Database.BlackListShow Convert()
        {
            Shared.Database.BlackListShow show = new Shared.Database.BlackListShow();

            show.Id = Id;

            show.TraktShowId = TraktShowId;
            show.SerieName = SerieName;

            show.Season = Season;
            show.Entire = Entire;

            return show;
        }
    }
}
