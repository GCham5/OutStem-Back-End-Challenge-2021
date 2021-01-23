using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frank_Workshop.Models
{
    public class User : IdentityUser<Guid>
    {

        public string FirstName;
        public string LastName;
        public bool IsPremium;
    }
}
