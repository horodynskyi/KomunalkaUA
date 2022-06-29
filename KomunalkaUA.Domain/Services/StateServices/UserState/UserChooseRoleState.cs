using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.StateServices.UserState;

public class UserChooseRoleState:IUserChooseRoleState
{
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<State> _stateRepository;
    private readonly IKeyboardService _keyboardService;

    public UserChooseRoleState(
        IRepository<User> userRepository, 
        IRepository<State> stateRepository, 
        IKeyboardService keyboardService)
    {
        _userRepository = userRepository;
        _stateRepository = stateRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
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
                keys = _keyboardService.GetKeys(new UserGetStartOwnerButtonsKeyboardCommand());
                break;
            case "Орендувальник":
                user.RoleId = (int?) RoleType.Tenant;
                text = "Круто, Ваша роль Орендувальник!" +
                           "\nЩоб авторизувати квартиру, яку ви хочете орендувати потрібно ввести номер телефона власника квартири:";
                var stateTenant = new State()
                {
                    UserId = update.Message.Chat.Id,
                    StateType = StateType.TenantAuthorizeFlat
                };
                await _stateRepository.AddAsync(stateTenant);
                keys = new ReplyKeyboardRemove();
                break;
            default:
                await client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    "Виберість свою роль!");
                return;
        }

        await _userRepository.UpdateAsync(user);
        state.Value = JsonConvert.SerializeObject(user);
      
        await _stateRepository.DeleteAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id, 
            text, 
            replyMarkup: keys);
    }
    

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.ChoseRole)
            return true;
        return false;
    }
}