using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class MeterInfoKeyboardCommand:IKeyboardCommand
{
    private int _meterId;
    private int _flatId;
    

    public MeterInfoKeyboardCommand(int meterId, int flatId)
    {
        _meterId = meterId;
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Редагувати", $"flat-meter-edit {_meterId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-meters {_flatId}"),
            }
        });
    }
}