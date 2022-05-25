using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices;

public interface IStateService
{
    Task Execute(Update update, ITelegramBotClient client);
    Task<bool> Contains(Update update);
}