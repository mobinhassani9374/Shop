﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public ICollection<EducationFile> Files { get; set; }
    }
}
