using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using CarCare.Infrastructure;
using Scalar.AspNetCore;
using System.Text;
using CarCare.Application.Authentication;
using CarCare.Server;
using CarCare.Server.Services;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
//builder.Services.AddAuthorization();
builder.Services.AddMudServices();
builder.Services.AddServices();
builder.Services.AddResponseCompression(opts => 
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/octet-stream"]));

#region Authentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.RequireHttpsMetadata = false;
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:JwtSecurityKey"]!)),
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:JwtIssuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:JwtAudience"],
            ClockSkew = TimeSpan.Zero
        };
    });

#endregion

#region DBContext

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

var app = builder.Build();

app.MapControllers();
app.MapRazorPages();

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("CarCare API")
               .WithLayout(ScalarLayout.Modern)
               .WithTheme(ScalarTheme.DeepSpace)
               .WithModels(false)
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();
app.MapFallbackToFile("index.html");

app.Run();
