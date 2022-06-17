using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatAddressKeyboardCommand:IKeyboardCommand
{
    private int _flatId;
    private string _street = "Вулиця:";
    private string _flatNumber = "";
    private string  _building = "";
    private string _city = "Місто";

    public FlatAddressKeyboardCommand(int flatId, Address? address)
    {
        if (address != null)
        {
            if (address.City !=null)
            {
                _city = address.City.Name;
            }
            if (string.IsNullOrEmpty(address.Street))
                _street = "Вулиця:";
            else
            {
                _street = address.Street;
            }
            if (string.IsNullOrEmpty(address.FlatNumber))
                _flatNumber = "";
            else
            {
                _flatNumber = address.FlatNumber;
            }
            if (string.IsNullOrEmpty(address.Building))
                _building = "";
            else
            {
                _building = address.Building;
            }
        }
        
        _flatId = flatId;
      
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData($"{_street}", $"flat-street {_flatId}"),
                InlineKeyboardButton.WithCallbackData($"Будинок:{_building}", $"flat-building {_flatId}"),
            }, 
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Номер квартири:{_flatNumber}", $"flat-flatNumber {_flatId}"),
                InlineKeyboardButton.WithCallbackData(_city, $"flat-city {_flatId}"),
            }
        });
    }
}