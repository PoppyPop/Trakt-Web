using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TraktDl.Business.Database.SqLite
{
    public class ShowSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public uint TraktShowId { get; set; }

        [Required]
        public virtual bool Blacklisted { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }

        public ShowSqLite()
        {

        }

        public ShowSqLite(Shared.Remote.Show show)
        {
            TraktShowId = show.Id;

            Name = show.SerieName;

            // TODO Seasons / Episodes
        }


    }
    public class SeasonSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Show")]
        public Guid ShowID { get; set; }
        public virtual ShowSqLite Show { get; set; }

        public virtual uint SeasonNumber { get; set; }

        [Required]
        public virtual bool Blacklisted { get; set; }


        public SeasonSqLite()
        {

        }
    }

    public class EpisodeSqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("Season")]
        public Guid SeasonID { get; set; }
        public virtual SeasonSqLite Season { get; set; }

        public virtual uint EpisodeNumber { get; set; }

        [Required]
        public virtual bool Blacklisted { get; set; }

        public virtual string Name { get; set; }

        public virtual string PosterUrl { get; set; }


        public EpisodeSqLite()
        {

        }
    }

}
