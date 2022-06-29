using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CommandService;
using KomunalkaUA.Domain.Services.CommandService.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListCommand:IListCommand
{
    private readonly List<ITelegramCommand> _commands;
    private ITelegramCommand _currentCommand;
    public ListCommand(
        IAddFlatCommand addFlatCommand,
        IFlatCommand flatCommand,
        IStartCommand startCommand
        
        )
    {
        _commands = new List<ITelegramCommand>
        {
            addFlatCommand,
            flatCommand,
            startCommand
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

    public async Task ExecuteAsync(Message message, ITelegramBotClient client)
    {
        await _currentCommand.Execute(message, client);
    }
}