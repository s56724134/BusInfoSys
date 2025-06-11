using api.Interfaces;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddScoped<ITDXTokenService, TDXTokenService>();
builder.Services.AddHttpClient<ILineLiffBusService, LineLiffBusService>();


builder.Services.AddDbContext<AppDbContext>(options =>
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


// app.MapGet("/api/businfoliff", async(HttpRequest request) =>
// {
//     using var reader = new StreamReader(request.Body);
//     var body = await reader.ReadToEndAsync();
//     Console.WriteLine("公車頁面 收到liff" + body);
//      // 回傳一個 HTML 頁面
//     string htmlContent = "<html><body><h2>這是公車LIFF頁面</h2></body></html>";
//     return Results.Content(htmlContent, "text/html; charset=utf-8");
// });
// test============================================================


// this can map what controller you have
app.MapControllers();

app.Run();

