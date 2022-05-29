using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class TenantStartCallback : ITenantStartCallback
{
    private string _callback = "tenant-start";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public TenantStartCallback(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByTenantIdIncludeAddressSpec(callbackQuery.From.Id));
        await client.EditMessageTextAsync(
            callbackQuery.From.Id, 
            callbackQuery.Message.MessageId,
            $"Ваша квартира {flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}:",
            replyMarkup: (InlineKeyboardMarkup)_keyboardService.GetKeys(new TenantFlatKeyboardCommand(callbackQuery.From.Id)));
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