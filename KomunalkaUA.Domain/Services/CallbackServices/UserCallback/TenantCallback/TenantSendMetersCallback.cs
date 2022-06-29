using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.CheckoutSpec;
using KomunalkaUA.Domain.Specifications.PreMeterCheckoutSpec;
using NodaTime;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;

public class TenantSendMetersCallback:ITenantSendMetersCallback
{
    private int _tenantId;
    private readonly string _callback = "tenant-send-meters";
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<PreMeterCheckout> _preMeterCheckoutRepository;
    private readonly IRepository<Checkout> _checkoutRepository;

    public TenantSendMetersCallback(
        IKeyboardService keyboardService, 
        IRepository<PreMeterCheckout> preMeterCheckoutRepository, 
        IRepository<Checkout> checkoutRepository)
    {
        _keyboardService = keyboardService;
        _preMeterCheckoutRepository = preMeterCheckoutRepository;
        _checkoutRepository = checkoutRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var checkouts = await _checkoutRepository.ListAsync(new CheckoutIncludeFlatGetByTenantId(_tenantId));
        if (!checkouts.Any(x =>
                x.Date.Value.InZone(DateTimeZone.Utc).Date.Month == DateTime.Now.Month && x.Date.Value.InZone(DateTimeZone.Utc).Date.Year == DateTime.Now.Year))
        {
            var preCheckouts =
                await _preMeterCheckoutRepository.ListAsync(new PreMeterCheckoutGetByTenantIdIncludeMeterIsNotApproved(_tenantId));
            if (!preCheckouts.Any())
            {
                await client.TryEditMessageTextAsync(
                    callbackQuery.From.Id,
                    callbackQuery.Message.MessageId,
                    $"Виберіть показник, який хочете вказати:",
                    callbackQuery.Message,
                    replyMarkup:(InlineKeyboardMarkup) _keyboardService.GetKeys(new TenantMetersKeyboardCommand(_tenantId,preCheckouts))
                );
            }
        }
        else
        {
            await client.AnswerCallbackQueryAsync(callbackQuery.Id, "Відправляти показники можна лише раз на місяць!");
        }
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Split()[0]==_callback)
        {
            _tenantId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}