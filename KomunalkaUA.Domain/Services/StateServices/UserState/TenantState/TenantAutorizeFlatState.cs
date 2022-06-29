using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.StateSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.StateServices.UserState.TenantState;

public class TenantAutorizeFlatState : ITenantAutorizeFlatState
{
    private readonly IRepository<User> _repository;
    private readonly IKeyboardService _keyboardService;

    public TenantAutorizeFlatState(
        IRepository<User> repository, 
        IKeyboardService keyboardService)
    {
        _repository = repository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var number = update.Message.Text;
        var owner = await _repository.GetBySpecAsync(new UserGetByPhoneSpec(number));
        var tenant = await _repository.GetByIdAsync(update.Message.Chat.Id);
        string text;
        if (owner == null)
        {
            text = "Некоректно введений номер!";
           
        }
        else
        {
            text = "Запит на авторизацію відправлено!\nОчікуйте підтвердження!";
        }
        await client.SendTextMessageAsync(tenant.Id,text);
        
        await client.SendTextMessageAsync(
            owner.Id,
            $"Користувач {tenant.FirstName} {tenant.SecondName} відправив заявку на підтвердження!",
            replyMarkup:_keyboardService.GetKeys(new OwnerAutorizeRequestKeyboardCommand(tenant)));
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.TenantAuthorizeFlat)
            return true;
        return false;
    }
}