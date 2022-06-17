using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class UserGetShareButtonKeyboardCommand:IKeyboardCommand
{
    public IReplyMarkup Get()
    {
        var keys = new ReplyKeyboardMarkup(
                new []
                {
                    new []
                    {
                        KeyboardButton.WithRequestContact("Надіслати номер"),
                    }
                }
            );
            keys.ResizeKeyboard = true;
            keys.Selective = true;
            keys.OneTimeKeyboard = true;
            return keys;
    }
}
public class UserGetStartOwnerButtonsKeyboardCommand:IKeyboardCommand
{
    public IReplyMarkup Get()
    {
        var keys = new ReplyKeyboardMarkup(
                new []
                {
                    new []
                    {
                        new KeyboardButton("Мої квартири"),
                        new KeyboardButton("Додати квартиру")
                    }
                }
            );
            keys.ResizeKeyboard = true;
            keys.Selective = true;
            keys.OneTimeKeyboard = true;
            return keys;
    }
}