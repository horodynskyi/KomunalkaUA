using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.Domain.Commands;

public class StartCommand:ITelegramCommand
{
    private readonly string _name = "/start";
    private readonly IRepository<State> _repository;

    public StartCommand(IRepository<State> repository)
    {
        _repository = repository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        var text = $"Ласкаво просимо до бота KomunalkaUA 🇺🇦 \n " +
                   $"Наш бот дозволяє автоматизувати спілкування між орендодавцем і орендувальником квартир \n " +
                   $"Для початку введіть ваше призвіще та ім'я:";
        var state = new State
        {
            Id = Guid.NewGuid(),
            UserId = message.Chat.Id,
            StateType = StateType.Registration
        };
        await _repository.AddAsync(state);
        await client.SendTextMessageAsync(message.Chat.Id, text);

    }
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}