using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Models;
using NodaTime;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatListCheckoutsKeyboardCommand:IKeyboardCommand
{
    private readonly List<Checkout> _checkouts;

    public FlatListCheckoutsKeyboardCommand(List<Checkout> checkouts)
    {
        _checkouts = checkouts;
    }

    public IReplyMarkup Get()
    {
        var checkKeys = _checkouts
            .Select(x =>
                InlineKeyboardButton.WithCallbackData($"{x.Date.Value.InZone(DateTimeZone.Utc).ToCheckoutDate()}",
                    $"flat-checkout {x.Id}"))
            .Chunk(2)
            .ToList();
        checkKeys.Add(new []{InlineKeyboardButton.WithCallbackData($"Повернутись",$"flat-detail {_checkouts.FirstOrDefault().FlatId}")});
        var keys = new InlineKeyboardMarkup(checkKeys);
        return keys;
    }
}