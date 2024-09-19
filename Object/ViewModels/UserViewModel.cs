using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iMobile.Object.ViewModels.User
{
    public class UserViewModel
    {
        [Display(Name ="Id")]
        public long Id { get; set; }

        [Required(ErrorMessage ="Enter role")]
        [Display(Name ="Role")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Enter login")]
        [Display(Name = "Login")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
