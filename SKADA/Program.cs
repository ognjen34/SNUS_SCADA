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

