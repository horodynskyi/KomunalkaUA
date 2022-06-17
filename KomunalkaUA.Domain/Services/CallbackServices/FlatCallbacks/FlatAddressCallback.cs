using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatAddressCallback:IFlatAddressCallback
{
    private int _flatId;
    private readonly string _callback = "flat-address";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;
    private int _backToDetail =0;
    

    public FlatAddressCallback(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressThenIncludeCitySpec(_flatId));
        InlineKeyboardMarkup keys;
        if (flat.Address != null)
        {
            if (flat.Address.City != null 
                && !string.IsNullOrEmpty(flat.Address.Street)
                && !string.IsNullOrEmpty(flat.Address.Building)
                && !string.IsNullOrEmpty(flat.Address.FlatNumber))
            {

                keys = (InlineKeyboardMarkup) _keyboardService.GetKeys(
                    new FlatAddressWithBackFieldKeyboardCommand(_flatId, flat.Address));
            }
            else
            {
                keys = (InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatAddressKeyboardCommand(
                    _flatId,
                    flat.Address));
         
            }
            await client.TryEditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                "Адрес квартири:",
                callbackQuery.Message,
                replyMarkup:keys);
        }
        
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Contains(_callback))
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}