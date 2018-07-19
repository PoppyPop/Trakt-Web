using System;

namespace TraktDl.Business.Shared.Database
{
    public class BlackListShow
    {
        public Guid? Id { get; set; }

        public uint TraktShowId { get; set; }

        public string SerieName { get; set; }

        public bool Entire { get; set; }

        public int? Season { get; set; }
    }
}
