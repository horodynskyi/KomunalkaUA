using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;

public class TenantRequestCardIfEmptyCallBack:ITenantRequestCardIfEmptyCallBack
{
    private string _callback = "tenant-request-card-if-empty";
    private IRepository<Flat> _flatRepository;
    private IKeyboardService _keyboardService;

    public TenantRequestCardIfEmptyCallBack(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByTenantIdIncludeTanandAddressSpec(callbackQuery.From.Id));
        await client.SendTextMessageAsync(
            flat.OwnerId,
            $"Користувач: {flat.Tenant.FirstName} {flat.Tenant.SecondName} запрошує карту для оплати!" +
            $"\nВи ще не вказали карту для оплати!",
            replyMarkup:_keyboardService.GetKeys(new OwnerCardRequestKeyboardCommand(flat.Id))
        );
        await client.DeleteMessageAsync(callbackQuery.From.Id, callbackQuery.Message.MessageId);
        await client.SendTextMessageAsync(
            callbackQuery.From.Id, 
            $"Ваша квартира {flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}:",
            replyMarkup:_keyboardService.GetKeys(new TenantFlatKeyboardCommand(callbackQuery.From.Id,flat.Id)));
    }

    public bool Contains(string callbackData)
    {
        if (callbackData.Split()[0] ==_callback)
        {
            return true;
        }
        return false;
    }
}