using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantCheckoutKeyboardCommand:IKeyboardCommand
{
    private readonly long _tenantId;
    private readonly int _checkoutId;
    private readonly string _back;
    private readonly int _flatId;

    public TenantCheckoutKeyboardCommand(long tenantId, int checkoutId, string back, int flatId =0)
    {
        _tenantId = tenantId;
        _checkoutId = checkoutId;
        _back = back;
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData($"Переглянути виписку", $"tenant-checkout-detail {_checkoutId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData($"Повернутись до квартири", $"{_back} {_flatId}"),
            }
        });
    }
}

