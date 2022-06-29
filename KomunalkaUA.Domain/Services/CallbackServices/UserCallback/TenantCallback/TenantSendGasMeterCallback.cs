using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Specifications.FlatMeterSpec;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;

public class TenantSendGasMeterCallback:ITenantSendGasMeterCallback
{
    private int _tenantId;
    private string _callback ="tenant-send-gas-meter";
    private readonly IRepository<FlatMeter> _flatMeterRepository;
    private readonly IRepository<State> _stateRepository;

    public TenantSendGasMeterCallback(
        IRepository<FlatMeter> flatMeterRepository,
        IRepository<State> stateRepository)
    {
        _flatMeterRepository = flatMeterRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatMeter = await _flatMeterRepository.GetBySpecAsync(new FlatMeterGetByTenantIdIncludeMeterFlatSpec(_tenantId,(int) MeterTypeEnum.Gas));
        var preMeterCheckout = new PreMeterCheckout()
        {
            FlatId = flatMeter.FlatId,
            TenantId = flatMeter.Flat.TenantId,
            MeterId = flatMeter.MetterId,
        };
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            Value = JsonConvert.SerializeObject(preMeterCheckout),
            StateType = StateType.PreMeterCheckout
        };
        await _stateRepository.AddAsync(state);
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"Введіть поточні показник газу (попередні - {flatMeter.Meter.Value}):",
            callbackQuery.Message,
            replyMarkup: InlineKeyboardMarkup.Empty()
        );
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