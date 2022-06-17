using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatAddressWithBackFieldKeyboardCommand:IKeyboardCommand
{
    private int _flatId;
    private readonly Address _address;

    public FlatAddressWithBackFieldKeyboardCommand(int flatId, Address address)
    {

        _flatId = flatId;
        _address = address;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"{_address.Street}", $"flat-street {_flatId}"),
                InlineKeyboardButton.WithCallbackData($"Будинок:{_address.Building}", $"flat-building {_flatId}"),
            }, 
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Номер квартири:{_address.FlatNumber}", $"flat-flatNumber {_flatId}"),
                InlineKeyboardButton.WithCallbackData(_address.City.Name, $"flat-city {_flatId}"),
            }, 
            new []
            {
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-edit {_flatId}"),
            }
        });
    }
}