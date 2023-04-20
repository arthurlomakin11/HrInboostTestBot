using Viber.Bot.NetCore.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddViberBotApi(opts =>
{
    opts.Token = "50e60e18f3a7dfe3-50f57cf9c7c14d6b-55bebe6c340e4f7f";
    opts.Webhook = "http://1981-109-207-199-125.ngrok-free.app/viber";
});

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();