using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

/*public class FlatCardNumberCallback:IFlatCardNumberCallback
{
    private IRepository<Flat> _flatRepository;
    private readonly string _callback = "flat-card";
    private int _flatId;
    public FlatCardNumberCallback(IRepository<Flat> flatRepository)
    {
        _flatRepository = flatRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        InlineKeyboardMarkup keys;
        string text;
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(_flatId));
        if (string.IsNullOrEmpty(flat.CardNumber))
        {
            text = "Ви не додали ще карту для перевода коштів!";
            keys = KeyboardServicec.CreateFlatCardInlineKeyboardButton(flat, CardType.Add);
        }
        else
        {
            text = "Ваша карта для перевода коштів:";
            keys = KeyboardServicec.CreateFlatBackToDetailInlineKeyboardMarkup(flat);
        }
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:text,
            replyMarkup:keys
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
}*/