using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatAttributesKeyboardCommand:IKeyboardCommand
{
    private readonly int _flatId;

    public FlatAttributesKeyboardCommand(int flatId)
    {
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Адрес",$"flat-address {_flatId}"), 
                InlineKeyboardButton.WithCallbackData("Комунальні рахунки",$"flat-meters {_flatId}"), 
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("Картка",$"flat-card {_flatId}"), 
                InlineKeyboardButton.WithCallbackData("Виписки",$"flat-checkouts {_flatId}"), 
            },
        });
    }
}