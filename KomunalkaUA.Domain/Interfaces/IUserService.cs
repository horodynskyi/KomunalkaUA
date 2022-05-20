using KomunalkaUA.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface IUserService
{
    Task RegistrationAsync(ITelegramBotClient client, Update update, State state);
    Task AddPhoneNumberAsync(ITelegramBotClient client, Update update, State state);
    Task ChooseRoleAsync(ITelegramBotClient client, Update update, State state);
}