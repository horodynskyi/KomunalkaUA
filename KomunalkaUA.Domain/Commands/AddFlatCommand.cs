using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.Domain.Commands;

public class AddFlatCommand:ITelegramCommand
{
    private readonly string _name = "Додати квартиру";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<State> _stateRepository;

    public AddFlatCommand(
        IRepository<Flat> flatRepository, 
        IRepository<State> stateRepository)
    {
        _flatRepository = flatRepository;
        _stateRepository = stateRepository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        var state = new State
        {
            UserId = message.Chat.Id,
            Value = JsonConvert.SerializeObject(new Flat()),
            StateType = StateType.FlatAddress
        };
        await _stateRepository.AddAsync(state);
    }

    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}