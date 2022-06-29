using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;


namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;

public class TenantRequestToOwnerAllowCallback:ITenantRequestToOwnerAllowCallback
{
    private string _callback = "tenant-request-to-authorize-flat-allow";
    
    private readonly IRepository<User> _userRepository;
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<Flat> _flatRepository;

    public TenantRequestToOwnerAllowCallback(
        IRepository<User> userRepository,
        IKeyboardService keyboardService, IRepository<Flat> flatRepository)
    {
        _userRepository = userRepository;
        _keyboardService = keyboardService;
        _flatRepository = flatRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var tenantId = Int64.Parse(callbackQuery.Data.Split()[1]);
        var tenant = await _userRepository.GetByIdAsync(tenantId);
        var text = $"Виберіть квартиру яку хочете авторизувати для {tenant.FirstName} {tenant.SecondName}";
        var flats = await _flatRepository.ListAsync(new FlatGetByOwnerIncludeAddressIdSpec(callbackQuery.From.Id));
        var keys = _keyboardService.GetKeys(new OwnerGetListFlatKeyboardCommand(flats, $"owner-flat-list-allow {tenantId}"));
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text,
            replyMarkup: (InlineKeyboardMarkup)keys
        );
    }
    
    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Contains(_callback))
        {
            return true;
        }
        return false;
    }
}
