using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CommandService.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CommandService;

public class FlatCommand:IFlatCommand
{
    private readonly string _name = "Мої квартири";
    private readonly IRepository<Flat> _repository;
    private readonly IKeyboardService _keyboardService;

    public FlatCommand(
        IRepository<Flat> repository,
        IKeyboardService keyboardService)
    {
        _repository = repository;
        _keyboardService = keyboardService;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        string text;
        IReplyMarkup replyMarkup;
        var flats = await _repository.ListAsync(new FlatGetByOwnerIdIncludeAddressSpec(message.Chat.Id),CancellationToken.None);
        if (flats.Count==0) 
        {
            text = "Ви ще не додали жодної квартири, щоб додати квартиру натистіть кнопку Додати квартиру";
            replyMarkup = _keyboardService.GetKeys(new UserGetStartOwnerButtonsKeyboardCommand());
        }
        else
        {
            text = $"Квартири {message.Chat.Username}:";
            replyMarkup = KeyboardServicec.CreateListFlatInlineKeyboardMarkup(flats);
        }
        await client.SendTextMessageAsync(
            message.Chat.Id,
            text,
            replyMarkup: replyMarkup);
    }

    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}