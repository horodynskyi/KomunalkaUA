using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices;

public interface IKeyboardCommand
{
    IReplyMarkup Get();
}