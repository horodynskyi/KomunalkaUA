using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;
using KomunalkaUA.Domain.Services.CommandService.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.CommandService;

public class StartCommand:IStartCommand
{
    private readonly string _name = "/start";
    private readonly IRepository<State> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public StartCommand(
        IRepository<State> repository, 
        IRepository<User> userRepository, 
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _repository = repository;
        _userRepository = userRepository;
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
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
        if (user.RoleId == (int?) RoleType.Owner)
        {
            await client.SendTextMessageAsync(message.From.Id,"Виберіть, що ви хочете зробити: ",replyMarkup:_keyboardService.GetKeys(new UserGetStartOwnerButtonsKeyboardCommand ()));
        }
        else if(user.RoleId == (int?) RoleType.Tenant)
        {
            var flat = await _flatRepository.GetBySpecAsync(new FlatGetByTenantIdIncludeAddressSpec(user.Id));
            if (flat == null)
            {
                await client.SendTextMessageAsync(
                    message.From.Id,
                    "Щоб авторизувати квартиру, яку ви хочете орендувати потрібно ввести номер телефона власника квартири:");
              
            }
            else
            {
                await client.SendTextMessageAsync(
                    message.From.Id,
                    $"Ваша квартрира {flat.Address.Street} {flat.Address.Building} {flat.Address.FlatNumber}:",
                    replyMarkup: _keyboardService.GetKeys(new TenantFlatKeyboardCommand(user.Id,flat.Id))
                );
            }
        }
        else
        {
            var state = new State
            {
                UserId = message.Chat.Id,
                StateType = StateType.Registration
            };
            await _repository.AddAsync(state);
            await client.SendTextMessageAsync(message.Chat.Id, text);
        }
        
    }
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}