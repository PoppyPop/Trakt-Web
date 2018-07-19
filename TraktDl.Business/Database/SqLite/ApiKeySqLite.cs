using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class ApiKeySqLite
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual string Id { get; set; }

        [Required]
        public virtual string ApiData { get; set; }

        public ApiKeySqLite()
        {

        }

        public ApiKeySqLite(ApiKey apiKey)
        {
            Id = apiKey.Id;
            ApiData = apiKey.ApiData;
        }

        public Shared.Database.ApiKey Convert()
        {
            Shared.Database.ApiKey key = new Shared.Database.ApiKey();

            key.Id = Id;
            key.ApiData = ApiData;

            return key;
        }
    }
}
