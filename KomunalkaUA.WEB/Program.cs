using KomunalkaUA.Domain;
using KomunalkaUA.Infrastracture;
using KomunalkaUA.WEB;
using Microsoft.Extensions.Options;
using Telegram.Bot;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var conn = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<AppSettings>(conn.GetSection("Configuration"));
builder.Services
    .AddInfrastracture()
    .AddDomain();
builder.Services.AddTransient(ser => ser.GetService<IOptions<AppSettings>>().Value);
builder.Services.AddSingleton<ITelegramBotClient>(
    x =>
    {
        var settings = x.GetRequiredService<AppSettings>();
        return new TelegramBotClient(settings.BotToken);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();