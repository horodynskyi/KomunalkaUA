using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatEditCallback:IFlatEditCallback
{
    private string _callback = "flat-edit";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatEditCallback(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatId = Int32.Parse(callbackQuery.Data.Split()[1]);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(flatId));
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"Квартира:<b>{flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}</b>" +
            $"\nВиберіть, що ви хочете змінити:",
            ParseMode.Html,
            replyMarkup:(InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatEditKeyboardCommand(flatId))
        );
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