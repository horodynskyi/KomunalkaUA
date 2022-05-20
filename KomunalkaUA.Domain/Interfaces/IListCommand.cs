using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface IListCommand
{
    bool Contains(Message message);
    Task Execute(Message message, ITelegramBotClient client);
}