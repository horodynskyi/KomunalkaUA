using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantCheckoutDetailKeyboardCommand:IKeyboardCommand
{
    private readonly Checkout _checkout;

    public TenantCheckoutDetailKeyboardCommand(Checkout checkout)
    {
        _checkout = checkout;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new []
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Докладніше",$"tenant-checkout-detail {_checkout.Id}"), 
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Повернутись",$"tenant-list-checkout {_checkout.FlatId}")
            }
        });
    }
}