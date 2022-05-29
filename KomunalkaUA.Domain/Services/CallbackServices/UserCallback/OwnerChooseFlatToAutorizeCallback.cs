using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Domain.Specifications.StateSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class OwnerChooseFlatToAutorizeCallback:IOwnerChooseFlatToAutorizeCallback
{
    private string _callback = "owner-flat-list-allow";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public OwnerChooseFlatToAutorizeCallback(
        IRepository<Flat> flatRepository,
        IKeyboardService keyboardService,
        IRepository<User> userRepository, IRepository<State> stateRepository)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
        _userRepository = userRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var msg = callbackQuery.Data.Split();
        var tenantId = Int64.Parse(msg[1]);
        var tenant = await _userRepository.GetByIdAsync(tenantId);
        var flatId = Int32.Parse(msg[2]);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(flatId));
        flat.TenantId = tenantId;
        await _flatRepository.UpdateAsync(flat);
        await _stateRepository.DeleteAsync(await _stateRepository.GetBySpecAsync(new StateGetByUserIdAndStateTypeNotNone(tenantId)));
        var keys = (InlineKeyboardMarkup) _keyboardService.GetKeys(new TenantFlatKeyboardCommand(tenant.Id));
        await SendToTenant(client, tenantId, flat, keys);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"Квартиру авторизовано!",
            replyMarkup: InlineKeyboardMarkup.Empty()
        );
    }

    private async Task SendToTenant(ITelegramBotClient client, long tenantId,Flat flat,InlineKeyboardMarkup keyboardMarkup)
    {
        await client.SendTextMessageAsync(
            tenantId, 
            $"Ваша квартира {flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}:",
            replyMarkup:keyboardMarkup);
    }
    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=3) 
            return false;
        if (callbackData.Contains(_callback))
        {
            return true;
        }
        return false;
    }
}

public interface IOwnerChooseFlatToAutorizeCallback:ICallback
{
    
}