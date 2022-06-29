using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatFlatNumberEditCallback:IFlatFlatNumberEditCallback
{
    private int _flatId;
    private readonly string _callback = "flat-flatNumber-edit";
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatFlatNumberEditCallback(
        IRepository<State> stateRepository, 
        IKeyboardService keyboardService)
    {
        _stateRepository = stateRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            StateType = StateType.FlatFlatNumber,
            Value = JsonConvert.SerializeObject(_flatId)
        };
        await _stateRepository.AddAsync(state);
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Введіть номер квартири:",
            callbackQuery.Message,
            replyMarkup: InlineKeyboardMarkup.Empty()
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Split()[0]==_callback)
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}