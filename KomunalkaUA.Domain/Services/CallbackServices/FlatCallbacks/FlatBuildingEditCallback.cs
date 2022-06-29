using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatBuildingEditCallback:IFlatBuildingEditCallback
{
    private int _flatId;
    private readonly string _callback = "flat-building-edit";
    private readonly IRepository<State> _stateRepository;

    public FlatBuildingEditCallback(
        IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            StateType = StateType.FlatBuilding,
            Value = JsonConvert.SerializeObject(_flatId)
        };
        await _stateRepository.AddAsync(state);
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Введіть номер будинку:",
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