namespace Bookify.Models.Results
{
    public static class UserError
    {
        public static Error NotFound = new(
            "User.Found",
            "The user with the specified identifier was not found");
    }
}
