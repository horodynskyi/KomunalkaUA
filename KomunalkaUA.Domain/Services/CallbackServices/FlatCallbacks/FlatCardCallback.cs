using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatCardCallback:IFlatCardCallback
{
    private string _callback = "flat-card";
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<Flat> _flatRepository;

    public FlatCardCallback(
        IKeyboardService keyboardService, 
        IRepository<Flat> flatRepository)
    {
        _keyboardService = keyboardService;
        _flatRepository = flatRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatId = Int32.Parse(callbackQuery.Data.Split()[1]);
        var flat = await _flatRepository.GetByIdAsync(flatId);
        if (flat.CardNumber == null)
        {
            await client.EditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                $"Ви ще не додали карту для оплати!",
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(
                    new FlatAddCardKeyboardCommand(flatId, "Додати карту"))
            );
        }
        else
        {
            await client.EditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                $"Ваша карта:" +
                $"\n<code>{flat.CardNumber}</code>",
                ParseMode.Html,
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(
                    new FlatAddCardKeyboardCommand(flatId, "Змінити карту"))
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