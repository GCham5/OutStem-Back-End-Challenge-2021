using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Frank_Workshop
{
    public class Recipe
    {
        public Recipe()
        {

        }
        //public Recipe(Guid Author, bool IsPrivate, bool IsPremium )
        //{
        //    this.Author = Author;
        //    this.IsPrivate = IsPrivate;
        //    this.IsPremium = IsPremium;
        //}

        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        public Guid Author { get; set; }
        [Required]
        public string Content { get; set; }
        public string Category { get; set; }
        public bool IsPrivate { get; set; } = true;
        public bool IsPremium { get; set; } = false;
        public bool IsDeleted { get; set; }

    }
}
