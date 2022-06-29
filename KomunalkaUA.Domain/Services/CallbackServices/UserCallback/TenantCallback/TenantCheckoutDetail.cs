using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;
using KomunalkaUA.Domain.Specifications.CheckoutPreMeterCheckoutSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;


public class TenantCheckoutDetailCallback:ITenantCheckoutDetailCallback
{
    private string _callback="tenant-checkout-detail";
    private int _checkoutId;
    private IRepository<CheckoutPreMeterCheckout> _checkoutPreMeterCheckout;

    public TenantCheckoutDetailCallback(IRepository<CheckoutPreMeterCheckout> checkoutPreMeterCheckout)
    {
        _checkoutPreMeterCheckout = checkoutPreMeterCheckout;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var checkouts =
            await _checkoutPreMeterCheckout.ListAsync(
                new CheckoutPreMeterCheckoutGetByIdIncludeFlatAddressPreMeterCheckoutMeterSpec(_checkoutId));
        var checkoutDto = new CheckoutDto(checkouts);
        
        (string gasString, int gasSum) = checkoutDto.GasCheckout.GetPaymentString();
        (string watterString, int watterSum) = checkoutDto.WatterCheckout.GetPaymentString();
        (string elecString, int elecSum) = checkoutDto.ElectricCheckout.GetPaymentString();
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"<b>Виписка №{checkoutDto.Checkout.Id}</b>" +
            $"\n{gasString}"+
            $"\n{watterString}"+
            $"\n{elecString}" +
            $"\n<b>Оренда:</b> <code>{checkoutDto.Checkout.PaymentSum-(gasSum+watterSum+elecSum)}</code>" +
            $"\n<b>Сума:</b> <code>{checkoutDto.Checkout.PaymentSum} грн.</code>",
            callbackQuery.Message,
            ParseMode.Html,
            replyMarkup:new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData($"Повернутись",$"tenant-checkout {checkoutDto.Id}"))
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg[0] ==_callback)
        {
            _checkoutId = Int32.Parse(msg[1]);
            return true;
        }
        return false;
    }
}