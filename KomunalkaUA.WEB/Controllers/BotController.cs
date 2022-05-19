using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.WEB.Controllers;

[ApiController]
[Route("api/message/update")]
public class BotController : Controller
{
    private readonly ITelegramBotClient _client;
    
    public BotController(ITelegramBotClient client)
    {
        _client = client;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Started");
    }
        
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Update update)
    {
      //  if (update.Message.Text == "/start") await _client.SendTextMessageAsync(update.Message.Chat.Id,"Hello");

          
        return Ok();
    }
}
