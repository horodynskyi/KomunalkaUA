using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.Lists;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class UserCallbackService:IUserCallbackService
{
    private ListUserCallback _listUserCallback;
    public UserCallbackService(ListUserCallback listUserCallback)
    {
        _listUserCallback = listUserCallback;
    }
    
    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await _listUserCallback.ExecuteAsync(callbackQuery, client);
    }

    public bool Contains(string callbackData)
    {
        return  _listUserCallback.Contains(callbackData);
    }
}