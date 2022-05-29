using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices;

public class KeyboardService:IKeyboardService
{
    public IReplyMarkup GetKeys(IKeyboardCommand keyboardCommand)
    {
        return keyboardCommand.Get();
    }
}