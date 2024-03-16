using System.ComponentModel.DataAnnotations;

namespace Bookify.Models.Requests
{
    public class RegisterUserRequest
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Имя клиента должно быть указано")]
        public string? FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Фамилия клиента должна быть указана")]
        public string? LastName { get; set; }

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

        /// <summary>
        /// Повтор пароля
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Повторный пароль клиента должен быть указан")]
        [Compare(nameof(Password), ErrorMessage = "Повторный пароль не совпадает с указанным выше")]
        public string? ConfirmPassword { get; set; }
    }
}
