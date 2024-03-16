namespace Bookify.Models.Results
{
    public class Error
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Error(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public static Error None = new Error(string.Empty, string.Empty);

        // must not return null всегда возвращать осознанное значение
        public static Error NullValue = new Error("Error.NoneValue", "Null value was provided");
    }
}
