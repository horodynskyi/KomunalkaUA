using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class ProviderListKeyboardCommand:IKeyboardCommand
{
    private readonly List<Provider> _providers;
    private readonly int _flatId;

    public ProviderListKeyboardCommand(
        List<Provider> cities, 
        int flatId)
    {
        _providers = cities;
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        var keys = new InlineKeyboardMarkup(
            _providers
                .Select(x => InlineKeyboardButton.WithCallbackData(x.Name,$"flat-provider-list {x.Id} {_flatId}"))
                .Chunk(2)
        );
        return keys;
    }
}