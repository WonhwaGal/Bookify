namespace Bookify.Models.Results
{
    /// <summary>
    /// Ошибка при выполнении запроса
    /// </summary>
    public class ErrorResult
    {
        /// <summary>
        /// Код ошибки
        /// </summary>
        public int? ErrorCode { get; init; }

        /// <summary>
        /// Описание ошибки
        /// </summary>
        public IList<string> ErrorMessages { get; init; } = null!;
    }
}
