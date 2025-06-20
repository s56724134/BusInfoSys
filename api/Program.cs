using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Dependency Injection
builder.Services.AddScoped<ITDXTokenService, TDXTokenService>();
builder.Services.AddHttpClient<IBusInfoService, BusInfoService>();
builder.Services.AddHttpClient<ILineIDTokenService, LineIDTokenService>();
// builder.Services.AddScoped<IRemindRepository, RemindRepository>();

// Register DBContext that reference "appsetting.Development.json"
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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

