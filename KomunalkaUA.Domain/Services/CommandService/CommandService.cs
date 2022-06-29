using KomunalkaUA.Domain.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.CommandService;

public class CommandService:ICommandService
{
    private readonly IListCommand _listCommand;

    public CommandService(IListCommand listCommand)
    {
        _listCommand = listCommand;
    }
    
    public async Task Execute(Message message, ITelegramBotClient client)
    {
        
        await _listCommand.ExecuteAsync(message, client);
    }

    public bool Contains(Message message)
    {
        return _listCommand.Contains(message);
    }
}