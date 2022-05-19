using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface ITelegramCommand
{
    Task Execute(Message message, ITelegramBotClient client);

    bool Contains(Message message);
}