using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class OwnerGetListFlatKeyboardCommand:IKeyboardCommand
{
    private List<Flat> _flats;
    private string callback;

    public OwnerGetListFlatKeyboardCommand(List<Flat> flats, string callback)
    {
        _flats = flats;
        this.callback = callback;
    }

    public IReplyMarkup Get()
    {
        var keys = new InlineKeyboardMarkup(
            _flats
                    .Select(x => InlineKeyboardButton.WithCallbackData($"{x.Address.Street} {x.Address.Building} {x.Address.FlatNumber}",$"{callback} {x.Id}"))
                    .Chunk(2)
            );
            return keys;
        }
}