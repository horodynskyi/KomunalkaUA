using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.Callback.FlatCallbacks;

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
            replyMarkup:KeyboardService.CreateListFlatInlineKeyboardMarkup(flats)
        );
    }

    public bool Contains(string callbackData)
    {
        if (callbackData.Contains(_callback))
        {
            return true;
        }
        return false;
    }
}