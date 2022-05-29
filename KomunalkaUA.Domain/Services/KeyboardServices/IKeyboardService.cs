using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices;

public interface IKeyboardService
{
    IReplyMarkup GetKeys(IKeyboardCommand keyboardCommand);
}