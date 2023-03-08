using System.ComponentModel.DataAnnotations;

namespace DTO;

public class UserDTO
{
    [Required(ErrorMessage ="E-mail obrigatório")]
    [DataType(DataType.EmailAddress, ErrorMessage ="E-mail is not valid")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="Senha obrigatória")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
