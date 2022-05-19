using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services;

public class KeyboardService
{
    public ReplyKeyboardMarkup GetRoleButtons()
    {
        var keys = new ReplyKeyboardMarkup(
            new []
            {
                new []
                {
                    new KeyboardButton("Власник"),
                    new KeyboardButton("Орендатор")
                }
            }
        );
        keys.ResizeKeyboard = true;
        keys.Selective = true;
        keys.OneTimeKeyboard = true;
        return keys;
    }

    public static ReplyKeyboardMarkup GetShareContactButton()
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