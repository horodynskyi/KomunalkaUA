using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Extensions;

public static class TelegramExtensions
{
    public static async Task<Message> TryEditMessageTextAsync(this ITelegramBotClient client,
        long chatId,
        int messageId, 
        string text,
        Message? message,
        ParseMode? parseMode = default,
        InlineKeyboardMarkup? replyMarkup = default,
        CancellationToken cancellationToken = default)
    {
        if (message.Text == text)
        {
            return message;
           
        }
        return await client.EditMessageTextAsync(
            chatId,
            messageId,
            text,
            parseMode:parseMode,
            replyMarkup: replyMarkup,
            cancellationToken:cancellationToken);
    }
}