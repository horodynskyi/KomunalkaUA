using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services;

public class KeyboardService
{
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
    public static ReplyKeyboardMarkup GetRolesButtons()
    {
        
        var keys = new ReplyKeyboardMarkup(
            new []
            {
                new []
                {
                  new KeyboardButton("Власник"),
                  new KeyboardButton("Орендувальник")
                }
            }
        );
        keys.ResizeKeyboard = true;
        keys.Selective = true;
        keys.OneTimeKeyboard = true;
        return keys;
    }

    public static ReplyKeyboardMarkup GetStartOwnerButtons()
    {
        var keys = new ReplyKeyboardMarkup(
            new []
            {
                new []
                {
                    new KeyboardButton("Мої квартири"),
                    new KeyboardButton("Додати квартиту")
                }
            }
        );
        keys.ResizeKeyboard = true;
        keys.Selective = true;
        keys.OneTimeKeyboard = true;
        return keys;
    }

    public static ReplyKeyboardMarkup GetStartTenantButtons()
    {
        var keys = new ReplyKeyboardMarkup(
            new []
            {
                new []
                {
                    new KeyboardButton("Авторизувати квартиру"),
                }
            }
        );
        keys.ResizeKeyboard = true;
        keys.Selective = true;
        keys.OneTimeKeyboard = true;
        return keys;
    }
}