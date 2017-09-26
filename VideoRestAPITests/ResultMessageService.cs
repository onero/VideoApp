using Microsoft.AspNetCore.Mvc;

namespace VideoRestAPITests
{
    internal static class ResultMessageService
    {
        public static string GetMessage(IActionResult result)
        {
            return ((ObjectResult) result).Value.ToString();
        }
    }
}