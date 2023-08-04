using Authorization.Business;
using Authorization.Business.Interfaces;
using Authorization.DAL;
using Authorization.Middlewares;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<IUFEService, UFEService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<AuthorizationDbContext>(options =>
    options.UseInMemoryDatabase("AuthorizationDb"));

//builder.Services.AddHsts(options =>
//{
//    options.Preload = true;
//    options.IncludeSubDomains = true;
//    options.MaxAge = TimeSpan.FromDays(365);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SeedMiddleware>();
app.UseMiddleware<AuthenticationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
