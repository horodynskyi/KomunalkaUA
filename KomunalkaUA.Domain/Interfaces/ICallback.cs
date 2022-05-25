using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface ICallback
{
    Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client);
    bool Contains(string callbackData);
}