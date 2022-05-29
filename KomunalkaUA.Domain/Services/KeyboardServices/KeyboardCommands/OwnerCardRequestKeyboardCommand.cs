using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class OwnerCardRequestKeyboardCommand:IKeyboardCommand
{
    private int _flatId;

    public OwnerCardRequestKeyboardCommand(int flatId)
    {
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Додати карту", $"owner-add-card {_flatId}"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Скасувати", "owner-request-card-denied"),
            }
        });
    }
}