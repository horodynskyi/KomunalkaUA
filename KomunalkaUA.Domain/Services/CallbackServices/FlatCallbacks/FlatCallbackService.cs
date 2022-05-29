using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback;
using KomunalkaUA.Domain.Services.Lists;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatCallbackService:IFlatCallBackService
{
    private ListFlatCallback _flatListCallback;
    public FlatCallbackService(ListFlatCallback flatListCallback)
    {
        _flatListCallback = flatListCallback;
    }
    
    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await _flatListCallback.ExecuteAsync(callbackQuery, client);
    }

    public bool Contains(string callbackData)
    {
        return  _flatListCallback.Contains(callbackData);
    }
}

public interface IFlatCallBackService:ICallBackService
{
}