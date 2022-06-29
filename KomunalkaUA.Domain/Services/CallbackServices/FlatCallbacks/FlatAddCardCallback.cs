using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatAddCardCallback:IFlatAddCardCallback
{
    private int _flatId;
    private string _callback = "flat-add-card";
    private IRepository<State> _stateRepository;

    public FlatAddCardCallback(IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            StateType = StateType.AddCardNumber,
            Value = JsonConvert.SerializeObject(callbackQuery.Data.Split()[1])
        };
        await _stateRepository.AddAsync(state);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Введіть номер карти:",
            replyMarkup: InlineKeyboardMarkup.Empty());
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (msg[0]==_callback)
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}