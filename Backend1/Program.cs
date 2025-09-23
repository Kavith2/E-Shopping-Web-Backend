using System.Text;
using Backend1.Data;
using Backend1.Exceptions;
using Backend1.IService;
using Backend1.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<MongoDBContext>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddExceptionHandler<AppExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Secret"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseCors("AllowAngularDevClient");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();
