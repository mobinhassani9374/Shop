using Shop.Domain.Dto.UserAccess;
using Shop.Mvc.Models.UserAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Mapping
{
    public static class UserAccessMapping
    {
        public static List<UserAccessGroupingViewModel> ToViewModel(this List<UserAccessGroupingDto> sources)
        {
            var result = new List<UserAccessGroupingViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static UserAccessGroupingViewModel ToViewModel(this UserAccessGroupingDto source)
        {
            return new UserAccessGroupingViewModel
            {
                Title = source.Title,
                Items = source.Items.ToViewModel()
            };
        }
        public static List<UserAccessItemViewModel> ToViewModel(this List<UserAccessItemDto> sources)
        {
            var result = new List<UserAccessItemViewModel>();
            foreach (var source in sources)
                result.Add(source.ToViewModel());
            return result;
        }
        public static UserAccessItemViewModel ToViewModel(this UserAccessItemDto source)
        {
            return new UserAccessItemViewModel
            {
                Checked = source.Checked,
                Code = source.Code,
                Title = source.Title
            };
        }
    }
}
