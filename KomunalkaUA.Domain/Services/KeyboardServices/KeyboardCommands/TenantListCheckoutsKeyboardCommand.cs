using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Models;
using NodaTime;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantListCheckoutsKeyboardCommand:IKeyboardCommand
{
    private readonly List<Checkout> _checkouts;

    public TenantListCheckoutsKeyboardCommand(List<Checkout> checkouts)
    {
        _checkouts = checkouts;
    }

    public IReplyMarkup Get()
    {
        var checkKeys = _checkouts
            .Select(x =>
                InlineKeyboardButton.WithCallbackData($"{x.Date.Value.InZone(DateTimeZone.Utc).ToCheckoutDate()}",
                    $"tenant-checkout {x.Id}"))
            .Chunk(2)
            .ToList();
        checkKeys.Add(new []{InlineKeyboardButton.WithCallbackData($"Повернутись",$"tenant-start")});
        var keys = new InlineKeyboardMarkup(checkKeys);
        return keys;
    }
}