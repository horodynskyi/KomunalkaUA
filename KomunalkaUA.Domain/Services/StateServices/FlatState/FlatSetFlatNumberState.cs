using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatSetFlatNumberState:IFlatSetFlatNumberState
{
    private readonly IRepository<State> _stateRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatSetFlatNumberState(
        IRepository<State> stateRepository,
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _stateRepository = stateRepository;
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(flatId));
        if (flat != null)
        {
            if (flat.Address is null)
                flat.Address = new Address();
            flat.Address.FlatNumber = update.Message.Text;
            await _flatRepository.UpdateAsync(flat);
            await _stateRepository.DeleteAsync(state);
            await client.SendTextMessageAsync(
                update.Message.From.Id,
                $"Вулицю: {update.Message.Text} збережено!",
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(
                    new FlatAddressKeyboardCommand(flatId, flat.Address))
            );
        }
        
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.FlatFlatNumber)
            return true;
        return false;
    }
}