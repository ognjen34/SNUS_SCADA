using SKADA.Models.Users.Model;
using SKADA.Models.Users.Repository;
using SKADA.Models.Users.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<AppDbContext>();

builder.Services.AddTransient<IUserRepository<User>, UserRepository<User>>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddAuthentication(Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            },
            OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return Task.CompletedTask;
            }
        };
        options.Cookie.SameSite = SameSiteMode.None;
        options.SlidingExpiration = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireRole(UserType.ADMIN.ToString(),UserType.CLIENT.ToString());
    });
    options.AddPolicy("ClientOnly", policy =>
    {
        policy.RequireRole(UserType.CLIENT.ToString());
    });

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.InitDataBase();
    Console.WriteLine(dbContext.Users.First().Name);
    Console.WriteLine(dbContext.Users.Last().Name);
}
app.Run();

