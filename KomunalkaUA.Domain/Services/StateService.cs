using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Specifications;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services;

public class StateService:IStateService
{
    private readonly IRepository<State> _stateRepository;
    private readonly IUserService _userService;


    public StateService(
        IRepository<State> repository, 
        IUserService userService)
    {
        _stateRepository = repository;
        _userService = userService;
    }

    public async Task Execute(Update update, ITelegramBotClient client)
    {
        var text = "";
        var state = await _stateRepository.GetBySpecAsync(new StateGetByUserId(update.Message.Chat.Id));
        switch (state.StateType)
        {
            case StateType.Registration:
                await _userService.RegistrationAsync(client, update, state);
                break;
            case StateType.PhoneNumber:
                text = $"Дякуємо! Ваш номер телефону - {update.Message.Contact.PhoneNumber} ☎ успішно збережено. 💾\n Тепер выберіть вашу роль:";
                var user = JsonConvert.DeserializeObject<User>(state.Value);
                break;
        }
    }
    
}

public interface IStateService
{
    Task Execute(Update update, ITelegramBotClient client);
}