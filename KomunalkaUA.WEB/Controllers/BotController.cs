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
    [HttpGet ("/carinfo")]

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Started");
    }
        
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Update update)
    {
        if (update == null) return Ok();

        var message = update.Message;
        var callback = update.CallbackQuery;
            
        if (message != null)
        {
           
        }
        else if (callback != null)
        {
           
        }
          
        return Ok();
    }
}
