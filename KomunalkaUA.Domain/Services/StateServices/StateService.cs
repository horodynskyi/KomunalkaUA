using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.Lists;
using KomunalkaUA.Domain.Services.StateServices.UserState;
using KomunalkaUA.Domain.Specifications.StateSpec;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices;

public class StateService:IStateService
{
    private readonly IRepository<State> _stateRepository;
    private readonly ListState _listState;
    private State _state;
    public StateService(
        IRepository<State> repository,
        ListState listState)
    {
        _stateRepository = repository;
        _listState = listState;
    }

    public async Task Execute(Update update, ITelegramBotClient client)
    {
        var state = await _stateRepository.GetBySpecAsync(new StateGetByUserId(update.Message.Chat.Id));
        await _listState.ExecuteAsync(update, client, state);
    }
    
    public async Task<bool> Contains(Update update)
    {
        _state = await _stateRepository.GetBySpecAsync(new StateGetByUserIdAndStateTypeNotNone(update.Message.Chat.Id));
        if (_state != null && _state.StateType == StateType.None)
            return false;
        if (_state != null && _listState.Contains(_state.StateType))
            return true;
        return false;
    }
}