using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantRequestOwnerCardKeyboardCommand:IKeyboardCommand
{
    public long? OwnerId;

    public TenantRequestOwnerCardKeyboardCommand(long? ownerId)
    {
        OwnerId = ownerId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Запросити карту", "tenant-request-card-if-empty"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Повернутись до квартири", "tenant-start"),
            },
        });
    }
}