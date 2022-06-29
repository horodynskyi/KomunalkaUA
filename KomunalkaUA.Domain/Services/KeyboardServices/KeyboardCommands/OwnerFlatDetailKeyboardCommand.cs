using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class OwnerFlatDetailKeyboardCommand : IKeyboardCommand
{
    private int _flatId;

    public OwnerFlatDetailKeyboardCommand(int flatId)
    {
        _flatId = flatId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(
            new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Редагувати квартиру",$"flat-edit {_flatId}"), 
                    InlineKeyboardButton.WithCallbackData($"Картка оплати",$"flat-card {_flatId}"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Виписки",$"flat-list-checkouts {_flatId}"),
                    InlineKeyboardButton.WithCallbackData($"Повернутись до квартир",$"flat-list"),
                }
              
            }
        );
    }
}