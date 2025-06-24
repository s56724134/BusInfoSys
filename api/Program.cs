using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using api.Interfaces;
using api.Services;
using api.Data;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BusInfoSys API", Version = "v1" });

    // 加入 JWT Bearer 支援
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please enter a valid token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    // 套用到所有需要授權的 API
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference{
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

// Register logging Service
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Dependency Injection setting
builder.Services.AddScoped<ITDXTokenService, TDXTokenService>();
builder.Services.AddScoped<IRemindRepository, RemindRepository>();
builder.Services.AddHttpClient<IBusInfoService, BusInfoService>();
builder.Services.AddHttpClient<ILineIDTokenService, LineIDTokenService>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<ILineMessageService, LineMessageService>();

// Background Service
builder.Services.AddHostedService<ReminderBackgroundService>();
// Register DBContext that reference "appsetting.Development.json"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Authenticate Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // ✅ 加上這行才能保留原本的 claim 名稱
        options.MapInboundClaims = false;
        options.Authority = "https://access.line.me";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://access.line.me",
            ValidateAudience = true,
            ValidAudience = "2007424668", // ✅ 請替換成你的 LINE channel ID
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
            {
                // 取得 LINE 的 JWK 公鑰（只載一次）
                var client = new HttpClient();
                var jwks = client.GetStringAsync("https://api.line.me/oauth2/v2.1/certs").Result;
                var keys = new JsonWebKeySet(jwks).GetSigningKeys();
                return keys;
            }
        };
    });


builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// That Can use the static resource
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
        ctx.Context.Response.Headers.Append("Expires", "0");
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/businfoliff", () => Results.Redirect("/BusRouteInput.html"));

// test============================================================
app.MapPost("/linebot/webhook", async (HttpRequest request) =>
{
    using var reader = new StreamReader(request.Body);
    var body = await reader.ReadToEndAsync();
    Console.WriteLine("Line Bot 收到訊息：" + body);
    return Results.Ok();
});


// this can map what controller you have
app.MapControllers();

app.Run();

