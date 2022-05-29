using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListFlatCallback
{
    private readonly List<ICallback> _callbacks;
    private ICallback _currentCallback;
    public ListFlatCallback(
        IFlatDetailCallback flatDetailCallback,
        IFlatListCallback flatListCallback,
        IFlatEditCallback flatEditCallback,
        IFlatRentCallback flatRentCallback,
        IFlatRentAddCallback flatRentAddCallback,
        IFlatCardCallback flatCardCallback)
    {
        _callbacks = new List<ICallback>()
        {
            flatDetailCallback,
            flatListCallback,
            flatEditCallback,
            flatRentCallback,
            flatRentAddCallback,
            flatCardCallback
        };
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await _currentCallback.ExecuteAsync(callbackQuery, client);
    }

    public bool Contains(string callbackData)
    {
        foreach (var callback in _callbacks)
        {
            if (callback.Contains(callbackData))
            {
                _currentCallback = callback;
                return true;
            }
        }
        return false;
    }
}
