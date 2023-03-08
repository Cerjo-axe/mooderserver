using System.ComponentModel.DataAnnotations;

namespace DTO;

public class RegisterDTO : UserDTO
{
    [Required(ErrorMessage ="Nome obrigatório",AllowEmptyStrings=false)]
    [Display(Name ="Nome de usário")]
    public string? UserName { get; set; }

    [Required]
    public string? ConfirmPassword { get; set; }
}
