using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoBookAPI.Models.Interfaces
{
    public interface IUser: IId
    {
        [Required(ErrorMessage = "First name is required")]
        string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        string LastName { get; set; }

        [Required(ErrorMessage = "Username Can't be empty.")]
        string Username { get; set; }

        [Required(ErrorMessage = "Password Can't be empty.")]
        string Password { get; set; }
        string Token { get; set; }
    }
}
