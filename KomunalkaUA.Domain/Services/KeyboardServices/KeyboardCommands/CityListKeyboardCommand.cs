using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class CityListKeyboardCommand:IKeyboardCommand
{
    private readonly List<City> _cities;
    private readonly int _flatId;

    public CityListKeyboardCommand(List<City> cities,
        int flatId)
    {
        _cities = cities;
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        var keys = new InlineKeyboardMarkup(
            _cities
                .Select(x => InlineKeyboardButton.WithCallbackData(x.Name,$"flat-city-list {_flatId} {x.Id}"))
                .Chunk(2)
        );
        return keys;
    }
}