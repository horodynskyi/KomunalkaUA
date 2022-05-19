using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services;

public class UserService:IUserService
{
    private IRepository<State> _repository;

    public UserService(IRepository<State> repository)
    {
        _repository = repository;
    }

    public async Task RegistrationAsync(ITelegramBotClient client,Update update, State state)
    {
        var text = "Дякую! Ваші дані збережено. 💾\n" +
               "Тепер напишіть ваш номер телефону:";
        var user = JsonConvert.DeserializeObject<User>(state.Value);
        (string firstName, string secondName) = (update.Message.Text.Split()[0],update.Message.Text.Split()[1]);
        user.FirstName = firstName;
        user.SecondName = secondName;
        user.Username = update.Message.Chat.Username;
        user.Id = update.Message.Chat.Id;
        state.Value = JsonConvert.SerializeObject(user);
        state.StateType = StateType.PhoneNumber;
        await _repository.UpdateAsync(state);
        await client.SendTextMessageAsync(update.Message.Chat.Id, text,
            replyMarkup: KeyboardService.GetShareContactButton());
    }

    public async Task AddPhoneNumberAsync(ITelegramBotClient client, Update update, State state)
    {
        var text = $"Дякуємо! Ваш номер телефону - {update.Message.Contact.PhoneNumber} ☎ успішно збережено. 💾\n Тепер выберіть вашу роль:";
        var user = JsonConvert.DeserializeObject<User>(state.Value);
        user.PhoneNumber = update.Message.Contact.PhoneNumber;
        state.Value = JsonConvert.SerializeObject(user);
    }
}

public interface IUserService
{
    Task RegistrationAsync(ITelegramBotClient client, Update update, State state);
    Task AddPhoneNumberAsync(ITelegramBotClient client, Update update, State state);
}