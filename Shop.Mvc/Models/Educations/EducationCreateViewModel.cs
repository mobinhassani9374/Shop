using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Mvc.Models.Educations
{
    public class EducationCreateViewModel
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
