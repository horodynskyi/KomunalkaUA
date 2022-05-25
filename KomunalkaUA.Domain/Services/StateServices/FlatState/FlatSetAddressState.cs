using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatSetAddressState:IFlatSetAddressState
{
    private IRepository<Flat> _flatRepository;
    private IRepository<State> _stateRepository;

    public FlatSetAddressState(
        IRepository<Flat> flatRepository, 
        IRepository<State> stateRepository)
    {
        _flatRepository = flatRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var addressMsg = update.Message.Text.Split();
        var text = $"Адрес записано\n{ReplyTextService.GetGasStateMessage()}";
        (string street, string building, string flatNumber) = (addressMsg[0], addressMsg[1], addressMsg[2]);
        var flat = JsonConvert.DeserializeObject<Flat>(state.Value);
        flat.Address = new()
        {
            Building = building,
            Street = street,
            FlatNumber = flatNumber
        };
        
        state.StateType = StateType.GasMeter;
        var entity = await _flatRepository.AddAsync(flat);
        state.Value = JsonConvert.SerializeObject(entity.Id);
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text);
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.FlatAddress)
            return true;
        return false;
    }
}