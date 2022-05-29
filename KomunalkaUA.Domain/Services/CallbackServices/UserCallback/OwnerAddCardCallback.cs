using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class OwnerAddCardCallback:IOwnerAddCardCallback
{
    private string _callback = "owner-add-card";
    private readonly IRepository<State> _stateRepository;

    public OwnerAddCardCallback(IRepository<State> stateRepository)
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
        if (callbackData.Split()[0] ==_callback)
        {
            return true;
        }
        return false;
    }
}

public interface IOwnerAddCardCallback:ICallback
{
}