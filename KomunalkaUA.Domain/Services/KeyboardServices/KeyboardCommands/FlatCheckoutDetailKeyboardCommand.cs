using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatCheckoutDetailKeyboardCommand:IKeyboardCommand
{
    private readonly Checkout _checkout;

    public FlatCheckoutDetailKeyboardCommand(Checkout checkout)
    {
        _checkout = checkout;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new []
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Докладніше",$"flat-checkout-detail {_checkout.Id}"), 
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Повернутись",$"flat-list-checkouts {_checkout.FlatId}")
            }
        });
    }
}