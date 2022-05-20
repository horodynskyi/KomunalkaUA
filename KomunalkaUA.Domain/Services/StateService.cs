using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Specifications;
using KomunalkaUA.Domain.Specifications.StateSpec;
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
    private readonly IFlatService _flatService;
    
    public StateService(
        IRepository<State> repository, 
        IUserService userService, 
        IFlatService flatService)
    {
        _stateRepository = repository;
        _userService = userService;
        _flatService = flatService;
    }

    public async Task Execute(Update update, ITelegramBotClient client)
    {
        var state = await _stateRepository.GetBySpecAsync(new StateGetByUserId(update.Message.Chat.Id));
        switch (state.StateType)
        {
            case StateType.Registration:
                await _userService.RegistrationAsync(client, update, state);
                break;
            case StateType.PhoneNumber:
                await _userService.AddPhoneNumberAsync(client, update, state);
                break;
            case StateType.ChoseRole:
                await _userService.ChooseRoleAsync(client, update, state);
                break;
            case StateType.FlatAddress:
                await _flatService.SetAddressAsync(client, update, state);
                break;
            case StateType.WatterMetter:
                await _flatService.SetMetterAsync(client, update, state);
                break;
            case StateType.None:
                return;
        }
    }

    public async Task<bool> HasState(Update update)
    {
        var user = await _stateRepository.GetBySpecAsync(new StateGetByUserIdAndStateTypeNotNone(update.Message.Chat.Id));
        if (user != null) return true;
        return false;
    }
}

public interface IStateService
{
    Task Execute(Update update, ITelegramBotClient client);
    Task<bool> HasState(Update update);
}