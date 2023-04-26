using HrInboostTestBot.Data.Tracks;
using Microsoft.EntityFrameworkCore;
using Viber.Bot.NetCore.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddViberBotApi(opts =>
{
    opts.Token = "50e60e18f3a7dfe3-50f57cf9c7c14d6b-55bebe6c340e4f7f";
    opts.Webhook = "https://92a7-109-207-199-125.ngrok-free.app/viber";
});

builder.Services.AddDbContext<TracksDbContext>(o => 
    o.UseSqlServer(@"Server=localhost;Database=Track;User Id=admin;Password=;Trusted_Connection=True;TrustServerCertificate=True;")
);

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();