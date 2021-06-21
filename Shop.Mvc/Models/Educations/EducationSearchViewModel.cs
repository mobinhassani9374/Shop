using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Educations
{
    public class EducationSearchViewModel : PagingViewModel
    {
        public EducationSearchViewModel()
        {
            this.PageSize = 12;
        }
    }
}
