using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatMeterValueEditCallback:IFlatMeterValueEditCallback
{
    private readonly IRepository<Meter> _meterRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly string _callback = "flat-meter-value-edit";
    private int _meterId;

    public FlatMeterValueEditCallback(
        IRepository<Meter> meterRepository, 
        IRepository<State> stateRepository)
    {
        _meterRepository = meterRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var meter = await _meterRepository.GetByIdAsync(_meterId);
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            Value = JsonConvert.SerializeObject(_meterId),
            StateType = StateType.ValueMeter
        };
        await _stateRepository.AddAsync(state);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Введіть значення показнику:",
            replyMarkup:InlineKeyboardMarkup.Empty()
        );
        
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (msg[0]==(_callback))
        {
            _meterId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}