using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.CheckoutSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class FlatCheckoutCallback:IFlatCheckoutCallback
{
    private string _callback="flat-checkout";
    private int _checkoutId;
    private readonly IRepository<Checkout> _checkoutRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatCheckoutCallback(
        IRepository<Checkout> checkoutRepository, 
        IKeyboardService keyboardService)
    {
        _checkoutRepository = checkoutRepository;
        _keyboardService = keyboardService;
    }
   
    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var checkout = await _checkoutRepository.GetBySpecAsync(new CheckoutGetByIdIncludeFlatAddress(_checkoutId));
        if (checkout != null)
        {
            await client.TryEditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                $"<b>Виписка №{checkout.Id}</b>" +
                $"\n<b>Квартира:</b> <code>{checkout.Flat.Address.Street} {checkout.Flat.Address.Building} {checkout.Flat.Address.FlatNumber}</code>" +
                $"\n<b>Сума оплати:</b> <code>{checkout.PaymentSum} грн.</code>",
                callbackQuery.Message,
                ParseMode.Html,
                replyMarkup:(InlineKeyboardMarkup?) _keyboardService.GetKeys(new FlatCheckoutDetailKeyboardCommand(checkout))
            );
        }
       
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