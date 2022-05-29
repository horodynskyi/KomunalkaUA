using KomunalkaUA.Domain;
using KomunalkaUA.Infrastracture;
using KomunalkaUA.Infrastracture.Database;
using KomunalkaUA.WEB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
var conn = builder.Configuration;
builder.Services.AddDbContext<DataContext>(opt => opt.UseNpgsql(conn.GetConnectionString("Postgres")));
builder.Services
    .AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(conn.GetSection("Configuration"));
builder.Services.AddTransient(ser => ser.GetService<IOptions<AppSettings>>().Value);
builder.Services
    .AddInfrastracture()
    .AddDomain();
builder.Services.AddSingleton<ITelegramBotClient>(
    x =>
    {
        var settings = x.GetRequiredService<AppSettings>();
        return new TelegramBotClient(settings.BotToken);
    });

var app = builder.Build();
app.UseTelegramBotWebhook();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
