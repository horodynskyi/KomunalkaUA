using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatMetersCallback:IFlatMetersCallback
{
    private int _flatId;
    private readonly string _callback = "flat-meters";
    private readonly IKeyboardService _keyboardService;

    public FlatMetersCallback(IKeyboardService keyboardService)
    {
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"Виберіть показник який хочете вказати:",
            callbackQuery.Message,
            replyMarkup: (InlineKeyboardMarkup?) _keyboardService.GetKeys(new FlatMeterKeyboardCommand(_flatId))
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Contains(_callback))
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}

public interface IFlatMetersCallback:ICallback
{
}