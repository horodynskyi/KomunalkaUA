using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services;

public class FlatService:IFlatService
{
    private readonly IRepository<State> _stateRepository;

    public FlatService(
        IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task SetAddressAsync(ITelegramBotClient client, Update update, State state)
    {
        var addressMsg = update.Message.Text.Split();
        (string street, string building, string flatNumber) = (addressMsg[0], addressMsg[1], addressMsg[2]);
        var flat = JsonConvert.DeserializeObject<Flat>(state.Value);
        flat.Address = new Address
            {
                Building = building,
                Street = street,
                FlatNumber = flatNumber
            };
        state.Value = JsonConvert.SerializeObject(flat);
        state.StateType = StateType.WatterMetter;
        await _stateRepository.UpdateAsync(state);
    }

    public async Task SetMetterAsync(ITelegramBotClient client, Update update, State state)
    {
        
    }
}