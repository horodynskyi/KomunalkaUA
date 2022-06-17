using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Lists;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CallbackServices;

public class CallBackService:ICallBackService
{
    private readonly ListCallback _listCallback;
    public CallBackService(ListCallback listCallback)
    {
        _listCallback = listCallback;
    }

    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        
        await _listCallback.ExecuteAsync(callbackQuery, client);
    }

    public bool Contains(string callbackData)
    {
        return _listCallback.Contains(callbackData);
    }
}