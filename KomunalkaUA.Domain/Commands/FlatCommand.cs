using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Commands;

public class FlatCommand:ITelegramCommand
{
    private readonly string _name = "Мої квартири";
    private readonly IRepository<Flat> _repository;

    public FlatCommand(IRepository<Flat> repository)
    {
        _repository = repository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        string text;
        IReplyMarkup replyMarkup;
        var flats = await _repository.ListAsync(new FlatGetByOwnerIdIncludeAddressSpec(message.Chat.Id),CancellationToken.None);
        if (flats.Count==0) 
        {
            text = "Ви ще не додали жодної квартири, щоб додати квартиру натистіть кнопку Додати квартиру";
            replyMarkup = KeyboardServicec.GetStartOwnerButtons();
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