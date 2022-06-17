using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatRentKeyboardCommand:IKeyboardCommand
{
    private int _flatId;
    private string _rentType;
    public FlatRentKeyboardCommand(int flatId, string rentType)
    {
        _flatId = flatId;
        _rentType = rentType;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData($"{_rentType}", $"flat-rent-add {_flatId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-edit {_flatId}"),
            }
        });
    }
}