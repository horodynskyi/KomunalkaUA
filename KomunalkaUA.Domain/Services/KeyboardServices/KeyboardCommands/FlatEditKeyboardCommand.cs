using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatEditKeyboardCommand:IKeyboardCommand
{
    private int _flatId;

    public FlatEditKeyboardCommand(int flatId)
    {
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Вартість оренди", $"flat-rent {_flatId}"),
                InlineKeyboardButton.WithCallbackData("Показники", $"flat-meters {_flatId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Адрес", $"flat-address {_flatId}"),
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-detail {_flatId}"),
                
            }
        });
    }
}