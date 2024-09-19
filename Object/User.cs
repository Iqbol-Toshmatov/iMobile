using iMobile.Object.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMobile.Object.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public Role Role { get; set; }
        public Profile Profile { get; set; }
    }
}
