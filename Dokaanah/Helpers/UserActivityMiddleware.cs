using Dokaanah.Models;
using Microsoft.AspNetCore.Identity;

namespace Dokaanah.PresentationLayer.Helpers
{
    public class UserActivityMiddleware
    {  
        private readonly RequestDelegate _next;

        public UserActivityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserManager<Customer> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(context.User);
                var user = await userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.LastActiveAt = DateTime.UtcNow;
                    user.IsOnline = true;
                    await userManager.UpdateAsync(user);
                }
            }

            await _next(context);
        }
    
    }
}
