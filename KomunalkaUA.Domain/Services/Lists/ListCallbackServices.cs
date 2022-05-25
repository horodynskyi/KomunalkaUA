using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListCallbackServices
{
    private List<ICallBackService> _callbackServices;
    private ICallBackService _currentService;

    public ListCallbackServices(
        IFlatCallBackService flatCallBackService
    )
    {
        _callbackServices = new List<ICallBackService>()
        {
            flatCallBackService
        };
    }

    public async Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await _currentService.Execute(callbackQuery, client);
    }

    public bool Contains(string callbackData)
    {
        foreach (var callback in _callbackServices)
        {
            if (callback.Contains(callbackData))
            {
                _currentService = callback;
                return true;
            }
        }
        return false;
    }
}