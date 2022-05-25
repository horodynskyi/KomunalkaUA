using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Callback.FlatCallbacks;

public class FlatDetailCallback:IFlatDetailCallback
{
    private readonly IRepository<Models.Flat> _flatRepository;
    private readonly string _callback = "flat-detail";
    private int _flatId;
    public FlatDetailCallback(IRepository<Models.Flat> repository)
    {
        _flatRepository = repository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(_flatId));
        var text = $"Квартира:{flat.Address.Street}\nЩо ви хочете зробити з квартирою?";
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:text,
            replyMarkup:KeyboardService.CreateFlatDetailInlineKeyboardMarkup(flat)
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