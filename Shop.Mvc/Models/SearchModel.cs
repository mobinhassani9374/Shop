using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models
{
    public class SearchModel<TSearch, TData>
    {
        public SearchModel(TSearch searchModel, TData model)
        {
            Search = searchModel;
            Data = model;
        }
        public TSearch Search { get; set; }
        public TData Data { get; set; }
    }
}
