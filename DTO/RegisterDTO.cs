using System.ComponentModel.DataAnnotations;

namespace DTO;

public class RegisterDTO
{
    [Required(ErrorMessage ="Nome obrigatório",AllowEmptyStrings=false)]
    [Display(Name ="Nome de usário")]
    public string UserName { get; set; }

    [Required(ErrorMessage ="E-mail obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage ="E-mail is not valid")]
    public string Email { get; set; }

    [Required(ErrorMessage ="Senha obrigatória")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public string ConfirmPassword { get; set; }
}
