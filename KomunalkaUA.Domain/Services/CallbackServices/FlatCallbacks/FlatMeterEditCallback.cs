using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatMeterSpec;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Domain.Specifications.ProviderSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatMeterEditCallback:IFlatMeterEditCallback
{
    private readonly string _callback = "flat-meter-edit";
    private int _metterId;
    private readonly IRepository<FlatMeter> _flatMeterRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatMeterEditCallback(
        IKeyboardService keyboardService, 
        IRepository<FlatMeter> flatMeterRepository)
    {
        _keyboardService = keyboardService;
        _flatMeterRepository = flatMeterRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flatMetter = await _flatMeterRepository.GetBySpecAsync(new FlatMeterGetByIdIncludeMeterProviderSpec(_metterId));
        if (flatMetter!=null)
        {
            await client.TryEditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                "Виберіть, що ви хочете змінити:",
                callbackQuery.Message,
                replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(
                    new FlatMeterEditInfoKeyboardCommand(flatMetter.Meter,(int) flatMetter.FlatId)));
        }
        
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Contains(_callback))
        {
            _metterId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}