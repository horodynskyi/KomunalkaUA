using KomunalkaUA.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services;

public class CallBackService:ICallBackService
{
    private readonly IFlatService _flatService;

    public CallBackService(IFlatService service)
    {
        _flatService = service;
    }

    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        switch (callbackQuery.Data)
        {
            case { } a when a.Contains("flat"):
                await _flatService.ProccessCallbackAsync(client, callbackQuery);
                break;
        }
    }


}

public interface ICallBackService
{
    Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client);
}