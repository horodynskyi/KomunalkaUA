using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.PreMeterCheckoutSpec;
using NodaTime;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.OwnerCallback;

public class OwnerAccessPreMetersCheckoutCallback:IOwnerAccessPreMetersCheckoutCallback
{
    private readonly string _callback = "owner-access-pre-meters-checkout";
    private  long _tenantId;
    private readonly IRepository<PreMeterCheckout> _preMeterCheckoutRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<CheckoutPreMeterCheckout> _checkoutPreMeterCheckoutRepository;

    public OwnerAccessPreMetersCheckoutCallback(
        IRepository<PreMeterCheckout> preMeterCheckoutRepository, 
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService, 
        IRepository<CheckoutPreMeterCheckout> checkoutPreMeterCheckoutRepository)
    {
        _preMeterCheckoutRepository = preMeterCheckoutRepository;
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
        _checkoutPreMeterCheckoutRepository = checkoutPreMeterCheckoutRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var preMeterCheckouts =
            await _preMeterCheckoutRepository.ListAsync(
                new PreMeterCheckoutGetByTenantIdIncludeMeterIsNotApproved(_tenantId));
        if (preMeterCheckouts.Any())
        {
            await preMeterCheckouts.SetApprovedAsync(_preMeterCheckoutRepository);
            var flat = await _flatRepository.GetByIdAsync(preMeterCheckouts[0].FlatId.Value);
            if (flat != null)
            {
                var checkout = preMeterCheckouts.CreateCheckoutsAsync(flat);
                var checkoutPreMeters = preMeterCheckouts.Select(x => new CheckoutPreMeterCheckout()
                {
                    Checkout = checkout,
                    PreMeterCheckoutId = x.Id
                }).ToList();
                foreach (var item in checkoutPreMeters)
                {
                    await _checkoutPreMeterCheckoutRepository.AddAsync(item);
                }
                await client.SendTextMessageAsync(
                    flat.TenantId,
                    $"\n<b>Номер виписки:</b> <code>№{checkout.Id}</code>" +
                    $"\n<b>Сума для оплати:</b> <code>{checkout.PaymentSum} грн.</code>" +
                    $"\n<b>Картка для оплати:</b> <code>{checkout.Flat.CardNumber}</code>"+
                    $"\n<b>Дата формування:</b> <code>{checkout.Date.Value.InZone(DateTimeZone.Utc).ToCheckoutDate()}</code>",
                    ParseMode.Html,
                    replyMarkup:_keyboardService.GetKeys(new TenantCheckoutKeyboardCommand(_tenantId,checkout.Id,"tenant-start"))
                );
                await client.TryEditMessageTextAsync(
                    flat.OwnerId.Value,
                    callbackQuery.Message.MessageId,
                    $"\n<b>Номер виписки:</b> <code>№{checkout.Id}</code>" +
                    $"\n<b>Сума для оплати:</b> <code>{checkout.PaymentSum} грн.</code>" +
                    $"\n<b>Дата формування:</b> <code>{checkout.Date.Value.InZone(DateTimeZone.Utc).ToCheckoutDate()}</code>",
                    callbackQuery.Message,
                    ParseMode.Html,
                    replyMarkup:(InlineKeyboardMarkup?) _keyboardService.GetKeys(new TenantCheckoutKeyboardCommand(flat.OwnerId.Value,checkout.Id,"flat-detail",flat.Id))
                );
            }
            
        }
       
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg[0] ==_callback)
        {
            _tenantId = Int64.Parse(msg[1]);
            return true;
        }
        return false;
    }
}