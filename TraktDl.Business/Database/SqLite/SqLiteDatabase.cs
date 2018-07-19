using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TraktDl.Business.Shared.Database;

namespace TraktDl.Business.Database.SqLite
{
    public class SqLiteDatabase : IDatabase
    {
        private SqLiteContext context { get; }

        public SqLiteDatabase()
        {
            context = new SqLiteContext();

            context.Database.Migrate();
        }

        public List<BlackListShow> BlackLists
        {
            get { return context.BlackListShow.Select(b => b.Convert()).ToList(); }
        }

        public void AddBlackList(BlackListShow blackListShow)
        {
            var exist = context.BlackListShow.Any(b => b.TraktShowId == blackListShow.TraktShowId && b.Season == blackListShow.Season && b.Entire == blackListShow.Entire);

            if (!exist)
            {
                context.BlackListShow.Add(new BlackListShowSqLite(blackListShow));
                context.SaveChanges();
            }
        }

        public void RemoveBlackList(BlackListShow blackListShow)
        {
            var toRemove = context.BlackListShow.SingleOrDefault(b => b.Id == blackListShow.Id);

            if (toRemove != null)
            {
                context.BlackListShow.Remove(toRemove);
                context.SaveChanges();
            }
        }

        public void ClearBlackList()
        {
            context.BlackListShow.ToList().ForEach(x => context.BlackListShow.Remove(x));
            context.SaveChanges();
        }
    }
}
