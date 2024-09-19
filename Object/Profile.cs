using iMobile.Object.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMobile.Object.Entity
{
    public class Profile
    {
        public long Id { get; set; }
        public string Address { get; set; } 
        public short Age { get; set; }
        public User User { get; set; }
        public Gender Gender { get; set; }
    }
}
