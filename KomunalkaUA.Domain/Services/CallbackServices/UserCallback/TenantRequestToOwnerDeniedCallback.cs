using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.CallbackServices.UserCallback;

public class TenantRequestToOwnerDeniedCallback:ITenantRequestToOwnerDeniedCallback
{
    private string _callback = "tenant-request-to-authorize-flat-denied";
    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var tenantId = Int64.Parse(callbackQuery.Data.Split()[1]);
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Авторизацію відхилено!",
            replyMarkup: InlineKeyboardMarkup.Empty());
        await client.SendTextMessageAsync(
            tenantId,
            "Авторизацію відхилено!\n" +
            "Введіть номер телефона власника:");
    }
    
    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Contains(_callback))
        {
            return true;
        }
        return false;
    }
}