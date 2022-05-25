using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatSetGasMeterState : IFlatSetGasMeterState
{
    private readonly IRepository<FlatMeter> _flatMeterRepository;
    private readonly IRepository<State> _stateRepository;

    public FlatSetGasMeterState(
        IRepository<FlatMeter> flatMeterRepository, 
        IRepository<State> stateRepository)
    {
        _flatMeterRepository = flatMeterRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var numberMsg = update.Message.Text.Split();
        var text = "Введіть номер рахунку і показник води формат:" +
                   "\nНомер рахунку показник";
        if (numberMsg.Length != 2)
        {
            await client.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Введено некоретно дані!\n{ReplyTextService.GetGasStateMessage()}");
            return;
        }
        (string number, string value) = (numberMsg[0],  numberMsg[1]);
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var meter = new Meter
        {
            Number = number,
            Value = Int32.Parse(value),
            MeterType = MeterType.Gas
        };
        await _flatMeterRepository.AddAsync(new FlatMeter
        {
            FlatId = flatId,
            Meter = meter
        });
        state.StateType = StateType.WatterMeter;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text);
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.GasMeter)
            return true;
        return false;
    }
}