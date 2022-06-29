using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantFlatKeyboardCommand:IKeyboardCommand
{
    private long _tenantId;
    private int _flatId;
    public TenantFlatKeyboardCommand(long tenantId, int flatId)
    {
        _tenantId = tenantId;
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Відправити показники",$"tenant-send-meters {_tenantId}"), 
                InlineKeyboardButton.WithCallbackData($"Виписки",$"tenant-list-checkout {_flatId}"), 
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Отримати карту для оплати",$"tenant-request-card {_tenantId}"), 
            }
        });
    }
}