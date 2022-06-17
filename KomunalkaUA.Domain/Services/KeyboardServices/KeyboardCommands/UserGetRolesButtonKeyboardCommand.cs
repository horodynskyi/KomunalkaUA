using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class UserGetRolesButtonKeyboardCommand:IKeyboardCommand
{
    public IReplyMarkup Get()
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
}