using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatMeterKeyboardCommand:IKeyboardCommand
{
    private int _flatId;

    public FlatMeterKeyboardCommand(int flatId)
    {
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Газ",$"flat-gas-meter {_flatId}"), 
                InlineKeyboardButton.WithCallbackData("Вода",$"flat-watter-meter {_flatId}"), 
                InlineKeyboardButton.WithCallbackData("Світло",$"flat-electrical-meter {_flatId}"), 
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("Повернутись",$"flat-edit {_flatId}"),
            },
            
        });
    }
}