using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.StateServices.UserState;

public class UserAddPhoneNumberState:IUserAddPhoneNumberState
{
    private IRepository<State> _stateRepository;

    public UserAddPhoneNumberState(IRepository<State> stateRepository)
    {
        _stateRepository = stateRepository;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
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
            replyMarkup: KeyboardServicec.GetRolesButtons());
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.PhoneNumber)
            return true;
        return false;
    }
}