using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    /// <summary>
    /// Набор параметров, необходимый для проведения процедуры
    /// аутентификации пользователя в системе
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Электронная почта клиента должна быть указана")]
        [EmailAddress]
        public string? Email { get; set; }


        /// <summary>
        /// Пароль
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Пароль клиента должен быть указан")]
        [StringLength(maximumLength: 40, MinimumLength = 5, ErrorMessage = "Длина пароля должна находиться в пределах от 5 до 40 символов")]
        public string? Password { get; set; }
    }
}