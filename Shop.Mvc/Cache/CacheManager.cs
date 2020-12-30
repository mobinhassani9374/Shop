using Shop.Mvc.Models.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Cache
{
    public static class CacheManager
    {
        public static void SetInfo(InfoViewModel data) => CacheData.Info = data;

    }
}
