using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatRentAddCallback:IFlatRentAddCallback
{
    private string _callback = "flat-rent-add";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<State> _stateRepository;

    public FlatRentAddCallback(
        IRepository<Flat> flatRepository, 
        IRepository<State> stateRepository)
    {
        _flatRepository = flatRepository;
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatId = Int32.Parse(callbackQuery.Data.Split()[1]);
        var flat = await _flatRepository.GetByIdAsync(flatId);
        var state = new State
        {
            UserId = callbackQuery.From.Id,
            StateType = StateType.Rent,
            Value = JsonConvert.SerializeObject(flat)
        };
        await _stateRepository.AddAsync(state);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Вкажіть суму орендної сплати:",
            replyMarkup: InlineKeyboardMarkup.Empty()
        );
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
public interface IFlatRentAddCallback:ICallback
{
}