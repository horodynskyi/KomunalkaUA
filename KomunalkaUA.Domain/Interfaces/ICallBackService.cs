using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface ICallBackService
{
    Task Execute(CallbackQuery callbackQuery, ITelegramBotClient client);
    bool Contains(string callbackData);
}