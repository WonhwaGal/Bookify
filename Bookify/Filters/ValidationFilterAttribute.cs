using Microsoft.AspNetCore.Mvc;

namespace Bookify.Filters
{
    public class ValidationFilterAttribute : TypeFilterAttribute
    {
        public ValidationFilterAttribute(int errorCode) : base(typeof(ValidationAsyncFilter))
        {
            Arguments = new object[] { errorCode };
        }
    }
}