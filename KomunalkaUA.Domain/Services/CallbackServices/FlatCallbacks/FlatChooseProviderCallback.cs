using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatMeterSpec;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatChooseProviderCallback:IFlatChooseProviderCallback
{
    private readonly string _callback = "flat-provider-list";
    private  int _flatId;
    private  int _providerId;
    private readonly IRepository<State> _stateRepository;
    private readonly IRepository<FlatMeter> _flatMeterRepository;
    private readonly IRepository<Meter> _meterRepository;

    public FlatChooseProviderCallback(
        IRepository<State> stateRepository,
        IRepository<FlatMeter> flatMeterRepository,
        IRepository<Meter> meterRepository)
    {
        _stateRepository = stateRepository;
        _flatMeterRepository = flatMeterRepository;
        _meterRepository = meterRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var meter = new Meter()
        {
            ProviderId = _providerId
        };
        await _meterRepository.AddAsync(meter);
        var flatMeter = new FlatMeter()
        {
            FlatId = _flatId,
            MetterId = meter.Id
        };
        await _flatMeterRepository.AddAsync(flatMeter);
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            Value = JsonConvert.SerializeObject(flatMeter.Meter.Id),
            StateType = StateType.GasMeter
        };
        await _stateRepository.AddAsync(state);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Введіть номер рахунку показника:",
            replyMarkup:InlineKeyboardMarkup.Empty()
        );
        
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=3) 
            return false;
        if (callbackData.Contains(_callback))
        {
            _providerId = Int32.Parse(msg[1]);
            _flatId = Int32.Parse(msg[2]);
            return true;
        }

        return false;
    }
}