using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CommandService.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CommandService;

public class AddFlatCommand:IAddFlatCommand
{
    private readonly string _name = "Додати квартиру";
    private readonly IKeyboardService _keyboardService;
    private IRepository<Flat> _flatRepository;

    public AddFlatCommand(
        IKeyboardService keyboardService,
        IRepository<Flat> flatRepository)
    {
        _keyboardService = keyboardService;
        _flatRepository = flatRepository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        var flat = new Flat()
        {
            OwnerId = message.From.Id,
        };
        await _flatRepository.AddAsync(flat);
        await client.SendTextMessageAsync(
            message.From.Id,
            $"Заповніть інформацію про квартиру:",
            replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(new FlatAddressKeyboardCommand(flat.Id,flat.Address))
        );
    }

    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}