using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

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
            Value = JsonConvert.SerializeObject(
                new Flat
                {
                    OwnerId = message.Chat.Id
                }),
            StateType = StateType.FlatAddress
        };
        await _stateRepository.AddAsync(state);
        await client.SendTextMessageAsync(
            message.Chat.Id,
            text: $"Ведіть повний адрес квартири",
            replyMarkup:new ReplyKeyboardRemove());
    }

    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}