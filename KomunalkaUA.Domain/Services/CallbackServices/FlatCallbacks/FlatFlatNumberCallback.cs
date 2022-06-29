using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatFlatNumberCallback:IFlatFlatNumberCallback
{
    private int _flatId;
    private readonly string _callback = "flat-flatNumber";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatFlatNumberCallback(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(_flatId));
        string buttonText = "Вказати";
        string text = "Квартира: Не вказано";
        if (flat.Address is not null)
        {
            if (!string.IsNullOrEmpty(flat.Address.FlatNumber))
            {
                text = $"Квартира: {flat.Address.FlatNumber}";
                buttonText = "Змінити";
            }
        }
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text,
            callbackQuery.Message,
            replyMarkup:(InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatStreetKeyboardCommand(_flatId,buttonText,"flat-flatNumber-edit"))
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