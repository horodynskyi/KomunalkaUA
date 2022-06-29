using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.OwnerCallback;
using KomunalkaUA.Domain.Services.StateServices.UserState.OwnerState.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListUserCallback
{
    private readonly List<ICallback> _callbacks;
    private ICallback _currentCallback;
    public ListUserCallback(
        ITenantRequestToOwnerAllowCallback tenantRequestToOwnerCallback,
        IOwnerChooseFlatToAutorizeCallback ownerChooseFlatToAutorizeCallback,
        ITenantRequestToOwnerDeniedCallback tenantRequestToOwnerDeniedCallback,
        ITenantStartCallback tenantStartCallback,
        ITenantRequestCardCallBack tenantRequestCardCallBack,
        ITenantRequestCardIfEmptyCallBack tenantRequestCardIfEmptyCallBack,
        IOwnerAddCardCallback ownerAddCardCallback)
    {
        _callbacks = new List<ICallback>()
        {
            tenantRequestToOwnerCallback,
            ownerChooseFlatToAutorizeCallback,
            tenantRequestToOwnerDeniedCallback,
            tenantStartCallback,
            tenantRequestCardCallBack,
            tenantRequestCardIfEmptyCallBack,
            ownerAddCardCallback
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