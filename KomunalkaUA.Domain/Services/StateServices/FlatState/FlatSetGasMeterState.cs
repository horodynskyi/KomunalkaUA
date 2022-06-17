using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Domain.Specifications.MeterSpec;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatSetMeterState : IFlatSetMeterState
{
    private readonly IRepository<Meter> _meterRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatSetMeterState(
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
        if (flatId ==null)
            return;
        meter.Number = update.Message.Text;
        await _meterRepository.UpdateAsync(meter);
        await client.SendTextMessageAsync(
            update.Message.From.Id,
            "Номер рахунку збережено!"
            );
        await _stateRepository.DeleteAsync(state);
        var meterValueState = new State()
        {
            UserId = update.Message.From.Id,
            Value = JsonConvert.SerializeObject(meterId),
            StateType = StateType.ValueMeter
        };
        await _stateRepository.AddAsync(meterValueState);
        await client.SendTextMessageAsync(
            update.Message.From.Id,
            "Введіть значення показнику:",
            replyMarkup: InlineKeyboardMarkup.Empty());
        /*await client.SendTextMessageAsync(
            update.Message.From.Id,
            "Заповніть інформацію про показники:",
            replyMarkup: _keyboardService.GetKeys(new FlatMeterKeyboardCommand((int) flatId))
        );*/

    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.GasMeter)
            return true;
        return false;
    }
}