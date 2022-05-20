using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services;

public class UserService:IUserService
{
    private IRepository<State> _stateRepository;
    private IRepository<User> _repository;

    public UserService(IRepository<State> stateRepository, IRepository<User> repository)
    {
        _stateRepository = stateRepository;
        _repository = repository;
    }

    public async Task RegistrationAsync(ITelegramBotClient client,Update update, State state)
    {
        var text = "Дякую! Ваші дані збережено. 💾\n" +
               "Тепер напишіть ваш номер телефону:";
        User user;
        if (state.Value == null) 
            user = new User();
        else 
            user = JsonConvert.DeserializeObject<User>(state.Value);
        (string firstName, string secondName) = (update.Message.Text.Split()[0],update.Message.Text.Split()[1]);
        user.FirstName = firstName;
        user.SecondName = secondName;
        user.Username = update.Message.Chat.Username;
        user.Id = update.Message.Chat.Id;
        state.Value = JsonConvert.SerializeObject(user);
        state.StateType = StateType.PhoneNumber;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(update.Message.Chat.Id, text,
            replyMarkup: KeyboardService.GetShareContactButton());
    }

    public async Task AddPhoneNumberAsync(ITelegramBotClient client, Update update, State state)
    {
        var text = $"Дякуємо! Ваш номер телефону - {update.Message.Contact.PhoneNumber} ☎ успішно збережено. 💾\n Тепер выберіть вашу роль:";
        var user = JsonConvert.DeserializeObject<User>(state.Value);
        user.PhoneNumber = update.Message.Contact.PhoneNumber;
        state.Value = JsonConvert.SerializeObject(user);
        state.StateType = StateType.ChoseRole;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id, 
            text, 
            replyMarkup: KeyboardService.GetRolesButtons());
    }
    public async Task ChooseRoleAsync(ITelegramBotClient client, Update update, State state)
    {
        string text;
        var user = JsonConvert.DeserializeObject<User>(state.Value);
        IReplyMarkup keys;
        switch (update.Message.Text)
        {
            case "Власник":
                user.RoleId = (int?) RoleType.Owner;
                text = $"Круто роль обрано!{update.Message.Text}\n" +
                       $"Тепер Ви можете додавати квартири і також переглядати інформацію про них:";
                keys = KeyboardService.GetStartOwnerButtons();
                
                break;
            case "Орендувальник":
                user.RoleId = (int?) RoleType.Tenant;
                text = $"Круто роль обрано!{update.Message.Text}";
                keys = KeyboardService.GetStartTenantButtons();
                break;
            default: return;
        }

        await _repository.UpdateAsync(user);
        state.Value = JsonConvert.SerializeObject(user);
        state.StateType = StateType.None;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id, 
            text, 
            replyMarkup: keys);
    }
}