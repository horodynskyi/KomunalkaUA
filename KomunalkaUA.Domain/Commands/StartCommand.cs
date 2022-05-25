using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Commands;

public class StartCommand:ITelegramCommand
{
    private readonly string _name = "/start";
    private readonly IRepository<State> _repository;
    private readonly IRepository<User> _userRepository;

    public StartCommand(IRepository<State> repository, IRepository<User> userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        var text = $"Ласкаво просимо до бота KomunalkaUA 🇺🇦 \n " +
                   $"Наш бот дозволяє автоматизувати спілкування між орендодавцем і орендувальником квартир \n " +
                   $"Для початку введіть ваше призвіще та ім'я:";
        var user = await _userRepository.GetByIdAsync(message.Chat.Id);
        if (user == null)
        {
            user = new User()
            {
                Id = message.Chat.Id
            };
            await _userRepository.AddAsync(user);
           
        }
        else if (user.RoleId ==(int?) RoleType.Owner)
        {
            
        }
        var state = new State
        {

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