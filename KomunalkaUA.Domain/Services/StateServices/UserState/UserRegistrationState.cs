using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.StateServices.UserState;

public class UserRegistrationState:IUserRegistrationState
{
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public UserRegistrationState(
        IRepository<State> stateRepository, 
        IKeyboardService keyboardService)
    {
        _stateRepository = stateRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
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
            replyMarkup: _keyboardService.GetKeys(new UserGetShareButtonKeyboardCommand()));
    }

    public  bool Contains(StateType stateType)
    {
        if (stateType == StateType.Registration)
            return true;
        return false;
    }
}