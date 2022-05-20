using KomunalkaUA.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface IFlatService
{
    Task SetAddressAsync(ITelegramBotClient client, Update update, State state);
    Task SetMetterAsync(ITelegramBotClient client, Update update, State state);
}