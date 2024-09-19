using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMobile.Object.ViewModels.Profile
{
    public class ProfileViewModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="Enter your age")]
        [Range(0,101,ErrorMessage ="Age must be big than 0 and little than 101")]

        public short Age { get; set; }

        [Required(ErrorMessage ="Enter your address")]
        [MinLength(5,ErrorMessage ="Symbols must be more than 3")]
        [MaxLength(200,ErrorMessage ="Symbols must be less than 200")]
        public string Address { get; set; }
    }
}
