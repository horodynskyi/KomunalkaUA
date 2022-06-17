using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Domain.Specifications.MeterSpec;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatValueMeterState:IFlatValueMeterState
{
    private readonly IRepository<Meter> _meterRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatValueMeterState(
        IRepository<Meter> meterRepository,
        IRepository<State> stateRepository,
        IKeyboardService keyboardService)
    {
        _meterRepository = meterRepository;
        _stateRepository = stateRepository;
        _keyboardService = keyboardService;
    }
    
    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var meterId = JsonConvert.DeserializeObject<int>(state.Value);
        var meter = await _meterRepository.GetByIdAsync(meterId);
        var meterInclude = await _meterRepository.GetBySpecAsync(new MeterGetFlatByMeterId(meter.Id));
        var flatId = meterInclude.FlatMeters.FirstOrDefault().FlatId;
        if (flatId == null)
            return;
        meter.Value = Int32.Parse(update.Message.Text);
        await _meterRepository.UpdateAsync(meter);
        await client.SendTextMessageAsync(
            update.Message.From.Id,
            "Значення лічильнику збережено!"
        );
        await _stateRepository.DeleteAsync(state);

        await client.SendTextMessageAsync(
            update.Message.From.Id,
            "Інформація про показники:",
            replyMarkup: _keyboardService.GetKeys(new FlatMeterKeyboardCommand((int) flatId))
        );

    }
    
    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.ValueMeter)
            return true;
        return false;
    }
    }
    