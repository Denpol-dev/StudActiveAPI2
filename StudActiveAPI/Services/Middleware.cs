using Microsoft.EntityFrameworkCore;
using StudActiveAPI.Models;
using System.Diagnostics;

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

            string? authHeader = context.Request.Headers["userid"];
            string? loginuser = context.Request.Headers["username"];
            if (authHeader != null)
            {
                var user = _con.Users.FirstOrDefault(it => it.Id == Guid.Parse(authHeader));

                var method = context.Request.Method;
                if (user != null && user.isValid)
                {
                    var permissionsUser = _con.PermissionsUsers.Where(p => p.UserId == user.Id).ToList();
                    var permissionsUserIds = permissionsUser.Select(p => p.PermissionId).ToList();
                    if (permissionsUserIds != null)
                    {
                        var permissions = _con.Permissions.Where(p => permissionsUserIds.Contains(p.Id)).ToList();
                        var permissionsTitle = new List<string>();
                        permissions.ForEach(p=> permissionsTitle.Add(p.Title));
                        if (method == "GET" && permissionsTitle.Contains("read") || method == "POST" && permissionsTitle.Contains("create"))
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
            else if (loginuser != null)
            {
                var user = _con.Users.FirstOrDefault(it => it.UserName == loginuser);

                if (user != null)
                {
                    await _next(context);
                }
            }
            else
            {
                context.Response.StatusCode = 426; //Unauthorized
                return;
            }
        }
    }
}
