using Microsoft.EntityFrameworkCore;
using StudActiveAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectString = builder.Configuration.GetConnectionString("ApiAppCon");
builder.Services.AddDbContext<Context>(op => op.UseSqlServer(connectString));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
app.UseRouting();
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/user"), appBuilder => { appBuilder.UseMiddleware<Middleware>(); });
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

[STAThread]
static void Main()
{
}