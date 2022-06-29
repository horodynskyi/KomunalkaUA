using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatAddPhotoCallback:IFlatAddPhotoCallback
{
    private readonly IRepository<Flat> _flatRepository;
    private readonly string _callback = "flat-photo-add";
    private int _flatId;
    private readonly IRepository<State> _stateRepository;

    public FlatAddPhotoCallback(IRepository<Flat> flatRepository, IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
        _flatRepository = flatRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var msg = callbackQuery.Data.Split();
        var state = new State()
        {
            UserId = callbackQuery.From.Id,
            Value = JsonConvert.SerializeObject(msg[1]),
            StateType = StateType.AddPhoto
        };
    
        await _stateRepository.AddAsync(state);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:"Відправьте фото квартири",
            replyMarkup:InlineKeyboardMarkup.Empty()
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