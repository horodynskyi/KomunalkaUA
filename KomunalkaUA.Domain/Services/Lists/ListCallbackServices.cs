using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListCallbackServices
{
    private List<ICallBackService> _callbackServices;
    private ICallBackService _currentService;

    public ListCallbackServices(
        IFlatCallBackService flatCallBackService,
        IUserCallbackService userCallbackService
    )
    {
        _callbackServices = new List<ICallBackService>()
        {
            flatCallBackService,
            userCallbackService
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