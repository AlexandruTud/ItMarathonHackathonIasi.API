using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Net;
using System.Text;
using Trading_API;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration));
//Jwt configuration starts here
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidateIssuer = true,
         ValidateAudience = true,
         ValidateLifetime = true,
         ValidateIssuerSigningKey = true,
         ValidIssuer = jwtIssuer,
         ValidAudience = jwtIssuer,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
     };
 });
//Jwt configuration ends here

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMyDependencyGroup();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.SetIsOriginAllowed(origin =>
        {
            var host = new Uri(origin).Host;
            var ipAddresses = Dns.GetHostAddresses(host);

            var allowedIPs = new List<IPAddress>
            {
                IPAddress.Parse("192.168.170.64"),
                IPAddress.Parse("192.168.1.2")
            };

            return ipAddresses.Any(ip => allowedIPs.Contains(ip));
        })
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});
builder.Services.AddCors(options => options.AddPolicy(name: "FrontendUI",

    policy =>

    {

        policy.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader();
        policy.WithOrigins("http://localhost:5500").AllowAnyMethod().AllowAnyHeader();

    }

));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontendUI");
app.UseAuthorization();

app.MapControllers();

app.Run();
