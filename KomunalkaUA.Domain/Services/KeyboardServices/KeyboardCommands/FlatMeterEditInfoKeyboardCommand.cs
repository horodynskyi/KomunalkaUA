using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatMeterEditInfoKeyboardCommand:IKeyboardCommand
{
    private readonly Meter _meter;
    private string _backCallback;
    private int _flatId;

    public FlatMeterEditInfoKeyboardCommand(Meter meter, int flatId)
    {
        _meter = meter;
        _flatId = flatId;
        _backCallback = meter.Provider.MeterTypeId == 1 ? $"flat-gas-meter {_flatId}" :
            meter.Provider.MeterTypeId == 2 ? $"flat-watter-meter {_flatId}" :
            meter.Provider.MeterTypeId == 3 ? $"flat-electrical-meter {_flatId}":$"flat-meters {_flatId}";
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData($"Постачальник: {_meter.Provider.Name}", "flat-provider-edit"),
                InlineKeyboardButton.WithCallbackData($"Рахунок: {_meter.Number}", $"flat-meter-number-edit"),
               
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData($"Показник:{_meter.Value ?? 0} m³", $"flat-meter-value-edit {_meter.Id}"),
                InlineKeyboardButton.WithCallbackData($"Повернутись", _backCallback),
            }
        });
    }
}