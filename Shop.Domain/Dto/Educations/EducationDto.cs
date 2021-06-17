using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Dto.Educations
{
    public class EducationDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public List<EducationFileDto> Files { get; set; }
    }
}
