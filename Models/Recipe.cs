using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frank_Workshop
{
    public class Recipe
    {  
        [Key]
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid Author { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsPremium { get; set; }
        public bool IsDeleted { get; set; }
    }
}
