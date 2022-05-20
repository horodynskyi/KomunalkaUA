using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Commands;

public class ListCommand:IListCommand
{
    private readonly List<ITelegramCommand> _commands;
    public List<ITelegramCommand> Get() => _commands;
    private ITelegramCommand _currentCommand;
    public ListCommand(IRepository<State> stateRepository,IRepository<Models.User> userRepository)
    {
        _commands = new List<ITelegramCommand>
        {
            new StartCommand(stateRepository,userRepository)
        };
    }

    public bool Contains(Message message)
    {
        foreach (var command in _commands)
        {
            if (command.Contains(message))
            {
                _currentCommand = command;
                return true;
            }
        }
        return false;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        await _currentCommand.Execute(message, client);
    }
}