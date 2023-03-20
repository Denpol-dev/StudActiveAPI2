using Microsoft.EntityFrameworkCore;
using StudActiveAPI.Models;

namespace StudActiveAPI.Services
{
    public class Middleware
    {
        private readonly RequestDelegate _next;
        private Context _con;
        public Middleware(RequestDelegate next) { _next = next; }

        public async Task InvokeAsync(HttpContext context, Context con)
        {
            _con = con;
            string authHeader = context.Request.Headers["userid"];
            if (authHeader != null)
            {
                User user = _con.Users.FirstOrDefault(it => it.Id == Guid.Parse(authHeader));

                var method = context.Request.Method;
                if (user != null && user.isValid)
                {
                    List<PermissionsUser> permissionsUser = _con.PermissionsUsers.Where(p => p.UserId == user.Id).ToList();
                    List<int> permissionsUserIds = permissionsUser.Select(p => p.PermissionId).ToList();
                    if (permissionsUserIds != null)
                    {
                        Permission permissions = _con.Permissions.Where(p => permissionsUserIds.Contains(p.Id)).FirstOrDefault();

                        if (method == "GET" && permissions.Title.Contains("read") || method == "POST" && permissions.Title.Contains("create"))
                        {
                            await _next(context);
                        }
                        else
                        {
                            context.Response.StatusCode = 401;
                            return;
                        }
                    }
                }
                else context.Response.StatusCode = 401; return;
            }
            else
            {
                // no authorization header
                context.Response.StatusCode = 426; //Unauthorized
                return;
            }
        }
    }
}
