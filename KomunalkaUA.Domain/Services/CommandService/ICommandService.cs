using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CommandService;

public interface ICommandService
{
    Task Execute(Message message, ITelegramBotClient client);
    bool Contains(Message message);
}