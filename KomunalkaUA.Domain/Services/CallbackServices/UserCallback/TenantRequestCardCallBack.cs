using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class TenantRequestCardCallBack:ITenantRequestCardCallBack
{
    private string _callback = "tenant-request-card";
    private IRepository<Flat> _flatRepository;
    private IRepository<User> _userRepository;
    private IKeyboardService _keyboardService;

    public TenantRequestCardCallBack(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService, 
        IRepository<User> userRepository)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var tenantId = Int64.Parse(callbackQuery.Data.Split()[1]);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByTenantIdIncludeAddressSpec(tenantId));
        if (string.IsNullOrEmpty(flat.CardNumber))
        {
            await client.EditMessageTextAsync(
                tenantId,
                callbackQuery.Message.MessageId,
                "Власник ще не вказав карту для оплати!",
                replyMarkup: (InlineKeyboardMarkup)_keyboardService.GetKeys(new TenantRequestOwnerCardKeyboardCommand(flat.OwnerId))
            );
        }
        else
        {
            var tenant = await _userRepository.GetByIdAsync(tenantId);
                await client.EditMessageTextAsync(
                    tenantId,
                    callbackQuery.Message.MessageId,
                    $"Картка для оплати:" +
                    $"\n<code>{flat.CardNumber}</code>",
                    ParseMode.Html,
                    replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(new TenantCardKeyboardCommand(tenant.Id))
                );
        }
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