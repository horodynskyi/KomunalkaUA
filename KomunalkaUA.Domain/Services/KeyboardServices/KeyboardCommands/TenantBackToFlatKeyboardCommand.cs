using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantBackToFlatKeyboardCommand:IKeyboardCommand
{
    private readonly long _tenantId;

    public TenantBackToFlatKeyboardCommand(long tenantId)
    {
        _tenantId = tenantId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Повернутись до квартири"),
        });
    }
}