using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.Lists;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.Callback.FlatCallbacks;

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

