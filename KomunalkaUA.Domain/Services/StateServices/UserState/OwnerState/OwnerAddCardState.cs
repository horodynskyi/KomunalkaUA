using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.UserState.OwnerState.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.Domain.Services.StateServices.UserState.OwnerState;

public class OwnerAddCardState : IOwnerAddCardState
{
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<State> _stateRepository;
 
    public OwnerAddCardState(
        IRepository<Flat> flatRepository, 
        IRepository<State> stateRepository)
    {
        _flatRepository = flatRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var cardNumber = update.Message.Text;
        var flatId = Int32.Parse(JsonConvert.DeserializeObject<string>(state.Value));
        var flat = await _flatRepository.GetByIdAsync(flatId);
        flat.CardNumber = cardNumber;
        await _flatRepository.UpdateAsync(flat);
        await _stateRepository.DeleteAsync(state);
        await client.SendTextMessageAsync(update.Message.Chat.Id, "Номер карти збережено");
        if (flat.TenantId != null)
        {
            await client.SendTextMessageAsync(
                flat.TenantId,
                $"Власник відправив вам номер карти!" +
                $"\n<code>{flat.CardNumber}</code>",
                ParseMode.Html);
        }
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.AddCardNumber)
            return true;
        return false;
    }
}