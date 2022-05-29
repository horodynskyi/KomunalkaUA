using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatRentCallback:IFlatRentCallback
{
    private string _callback = "flat-rent";
    private readonly IRepository<State> _stateRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatRentCallback(
        IRepository<State> stateRepository, 
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _stateRepository = stateRepository;
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatId = Int32.Parse(callbackQuery.Data.Split()[1]);
        var flat = await _flatRepository.GetByIdAsync(flatId);
        if (flat.Rent == null)
        {
            await client.EditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                "Ви ще не вказали вартість оренди!",
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatRentKeyboardCommand(flatId,"Додати вартість"))
            );
        }
        else
        {
            await client.EditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                $"Вартість оренди:" +
                $"\n<code>{flat.Rent} грн</code>",
                ParseMode.Html,
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatRentKeyboardCommand(flatId, "Змінити вартість")));
        }
        /*var state = new State
        {
            UserId = callbackQuery.From.Id,
            StateType = StateType.Rent,
            Value = JsonConvert.SerializeObject(flat)
        };
        await _stateRepository.AddAsync(state);*/
        
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