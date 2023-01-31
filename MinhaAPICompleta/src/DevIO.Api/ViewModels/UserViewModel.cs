using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DevIO.Api.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage ="O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatorio")]
        [StringLength(100, ErrorMessage ="O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength =6)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string ConfirmPassword { get; set; }

    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

    }

    public class userTokenViewModel
    {
        public string Id { get; set; }  
        public string Email { get; set; }   
        public string Name { get; set; }
        public IEnumerable<ClaimViewModel> claims { get; set; }
    }


    public class LoginResponseViewModel
    {
        public string AcessToken { get; set; }  
        public double ExpiresIn { get; set; }
        public userTokenViewModel User { get; set; }

    }

    public class ClaimViewModel
    {
        public string Value { get; set; }   
        public string Type { get; set; }   
    }


}
