using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantCardKeyboardCommand:IKeyboardCommand
{
    public long? OwnerId;

    public TenantCardKeyboardCommand(long? ownerId)
    {
        OwnerId = ownerId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Повернутись до квартири", "tenant-start"),
        });
    }
}