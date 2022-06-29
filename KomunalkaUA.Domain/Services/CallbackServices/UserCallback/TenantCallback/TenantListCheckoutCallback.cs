using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.CheckoutSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;

public class TenantListCheckoutCallback:ITenantListCheckoutCallback
{
    private string _callback="tenant-list-checkout";
    private int _flatId;
    private readonly IRepository<Checkout> _checkoutRepository;
    private readonly IKeyboardService _keyboardService;

    public TenantListCheckoutCallback(IRepository<Checkout> checkoutRepository, IKeyboardService keyboardService)
    {
        _checkoutRepository = checkoutRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var checkouts = await _checkoutRepository.ListAsync(new CheckoutGetByFlatIdSpec(_flatId));
        if (checkouts.Any())
        {
            await client.TryEditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                $"Виписки:",
                callbackQuery.Message,
                replyMarkup: (InlineKeyboardMarkup?) _keyboardService.GetKeys(
                    new TenantListCheckoutsKeyboardCommand(checkouts))
            );
        }
        else
        {
            await client.AnswerCallbackQueryAsync(callbackQuery.Id, "Ви ще не створили жодної виписки!");
        }
       
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg[0] ==_callback)
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }
        return false;
    }
}