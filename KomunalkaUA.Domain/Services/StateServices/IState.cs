using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices;

public interface IState
{
    Task ExecuteAsync(ITelegramBotClient client, Update update, State state);
    bool Contains(StateType stateType);
}