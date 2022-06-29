using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatListCallback:IFlatListCallback
{
    private readonly IRepository<Models.Flat> _flatRepository;
    private readonly string _callback = "flat-list";

    public FlatListCallback(IRepository<Models.Flat> flatRepository)
    {
        _flatRepository = flatRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flats = await _flatRepository.ListAsync(new FlatGetByOwnerIdIncludeAddressSpec(callbackQuery.Message.Chat.Id),CancellationToken.None);
        var text = $"Квартири {callbackQuery.Message.Chat.Username}:";
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:text,
            replyMarkup:KeyboardServicec.CreateListFlatInlineKeyboardMarkup(flats)
        );
    }

    public bool Contains(string callbackData)
    {
        if (callbackData.Split()[0]==_callback)
        {
            return true;
        }
        return false;
    }
}