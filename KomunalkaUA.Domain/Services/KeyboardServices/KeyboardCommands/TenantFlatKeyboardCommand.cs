using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantFlatKeyboardCommand:IKeyboardCommand
{
    private long _tenantId;
    public TenantFlatKeyboardCommand(long tenantId)
    {
        _tenantId = tenantId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Відправити показники",$"tenant-send-watter-meter {_tenantId}"), 
                InlineKeyboardButton.WithCallbackData($"Отримати карту для оплати",$"tenant-request-card {_tenantId}"), 
            }
        });
    }
}