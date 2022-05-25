using System.Net.Mime;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services;
using KomunalkaUA.Domain.Services.StateServices;
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
    private readonly ICallBackService _callBackService;
    public BotController(
        ITelegramBotClient client,
        IListCommand listCommand, 
        IStateService stateService, 
        ICallBackService callBackService)
    {
        _client = client;
        _listCommand = listCommand;
        _stateService = stateService;
        _callBackService = callBackService;
    }
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Started");
    }
        
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]Update? update)
    {
        if (update == null) return Ok();
        if (update.Message != null && (update.Message.Chat.Id==632067948 ||update.Message.Chat.Id == 470696076))
        {
            await _client.SendTextMessageAsync(update.Message.Chat.Id, "Маєш гарний хуй");
            return Ok();
        }
        var message = update.Message;
        var callback = update.CallbackQuery;
        if (message != null)
        {
            if (_listCommand.Contains(message))
            {
                await _listCommand.Execute(message,_client);
            }
            else if (await _stateService.Contains(update))
            {
               await _stateService.Execute(update, _client);
            }

            return Ok();
        }

        if (callback != null)
        {
            if (_callBackService.Contains(callback.Data))
            {
                await _client.AnswerCallbackQueryAsync(callback.Id);
                await _callBackService.Execute(callback, _client);
            }
        }
              
        return Ok();
    }
}
