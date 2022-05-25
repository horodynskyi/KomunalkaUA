using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Lists;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services;

public class CallBackService:ICallBackService
{
    private ListCallbackServices _callBackServices;
    public CallBackService(
        ListCallbackServices callBackServices)
    {
        _callBackServices = callBackServices;
    }

    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        
        await _callBackServices.Execute(callbackQuery, client);
        
    }

    public bool Contains(string callbackData)
    {
        
        return _callBackServices.Contains(callbackData);
    }
}