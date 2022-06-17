using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices.FlatState;

public class FlatSetAddressState:IFlatSetAddressState
{
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatSetAddressState(
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
        var addressMsg = update.Message.Text.Split();
        var text = $"Адрес записано\nДля подальшого користування заповніть інформацію про комунальні послуги:";
        (string street, string building, string flatNumber) = (addressMsg[0], addressMsg[1], addressMsg[2]);
       // var flat = JsonConvert.DeserializeObject<Flat>(state.Value);
       var flat = new Flat()
       {
           OwnerId = update.Message.From.Id,
           Address = new()
           {
               Building = building,
               Street = street,
               FlatNumber = flatNumber
           }
       };
       var entity = await _flatRepository.AddAsync(flat);
        var keys = _keyboardService.GetKeys(new FlatMeterKeyboardCommand(entity.Id));
        await _stateRepository.DeleteAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text,
            replyMarkup:keys);
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.FlatAddress)
            return true;
        return false;
    }
}