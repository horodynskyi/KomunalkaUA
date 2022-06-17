using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatStreetKeyboardCommand:IKeyboardCommand
{
    private readonly int _flatId;
    private readonly string _buttonText;
    private readonly string _buttonCallback;

    public FlatStreetKeyboardCommand(
        int flatId,
        string buttonText, 
        string buttonCallback)
    {
        _flatId = flatId;
        _buttonText = buttonText;
        _buttonCallback = buttonCallback;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(_buttonText, $"{_buttonCallback} {_flatId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-address {_flatId}"),
            }
        });
    }
}