using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public interface IFlatRentState:IState
{
}
public class FlatRentState:IFlatRentState
{
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatRentState(
        IRepository<Flat> flatRepository, 
        IRepository<State> stateRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _stateRepository = stateRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var rent = Int32.Parse(update.Message.Text);
        var flat = JsonConvert.DeserializeObject<Flat>(state.Value);
        flat.Rent = rent;
        await _flatRepository.UpdateAsync(flat);
        await _stateRepository.DeleteAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            "Вартість оренди збережено!");
        if (flat.Address == null)
        {
            flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(flat.Id));
        }
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            $"Квартира:<b>{flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}</b>" +
            $"\nВиберіть, що ви хочете змінити:",
            ParseMode.Html,
            replyMarkup: _keyboardService.GetKeys(new FlatEditKeyboardCommand(flat.Id))
        );
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.Rent)
            return true;
        return false;
    }
}

