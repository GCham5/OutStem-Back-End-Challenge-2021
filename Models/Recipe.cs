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
        public Guid Id;
        public DateTime DateCreated;
        public DateTime DateLastUpdated;
        public Guid Author;
        public string Content;
        public string Category;
        public bool IsPrivate;
        public bool IsPremium;
        public bool IsDeleted;

    }
}
