﻿using System.Collections.Generic;

namespace TraktDl.Business.Shared.Database
{
    public interface IDatabase
    {
        List<BlackListShow> BlackLists { get; }

        void AddBlackList(BlackListShow blackListShow);

        void RemoveBlackList(BlackListShow blackListShow);

        void ClearBlackList();
    }
}