using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.WEB.Controllers;

[ApiController]
[Route("api/message/update")]
public class BotController : Controller
{
    private readonly ITelegramBotClient _client;
    private readonly IListCommand _listCommand;
    private readonly IStateService _stateService;
    public BotController(
        ITelegramBotClient client,
        IListCommand listCommand, 
        IStateService stateService)
    {
        _client = client;
        _listCommand = listCommand;
        _stateService = stateService;
    }
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
            if (_listCommand.Contains(message))
            {
                await _listCommand.Execute(message,_client);
            }
            else if (await _stateService.HasState(update))
            {
               await _stateService.Execute(update, _client);
            }
        }
        return Ok();
    }
}
