using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using KomunalkaUA.Shared;
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

    public UserChooseRoleState(
        IRepository<User> userRepository, 
        IRepository<State> stateRepository)
    {
        _userRepository = userRepository;
        _stateRepository = stateRepository;
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
                keys = KeyboardService.GetStartOwnerButtons();
                break;
            case "Орендувальник":
                user.RoleId = (int?) RoleType.Tenant;
                text = $"Круто роль обрано!{update.Message.Text}";
                keys = KeyboardService.GetStartTenantButtons();
                break;
            default: return;
        }

        await _userRepository.UpdateAsync(user);
        state.Value = JsonConvert.SerializeObject(user);
        state.StateType = StateType.None;
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