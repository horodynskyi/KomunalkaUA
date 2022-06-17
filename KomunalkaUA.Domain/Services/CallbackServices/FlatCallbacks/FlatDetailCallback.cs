using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatDetailCallback:IFlatDetailCallback
{
    private readonly IRepository<Flat> _flatRepository;
    private readonly string _callback = "flat-detail";
    private readonly IKeyboardService _keyboardService;
    private int _flatId;
    public FlatDetailCallback(
        IRepository<Flat> repository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = repository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(_flatId));
        var text = $"Квартира:{flat.Address.Street}\nЩо ви хочете зробити з квартирою?";
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:text,
            replyMarkup:(InlineKeyboardMarkup)_keyboardService.GetKeys(new OwnerFlatDetailKeyboardCommand(_flatId))
        );
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