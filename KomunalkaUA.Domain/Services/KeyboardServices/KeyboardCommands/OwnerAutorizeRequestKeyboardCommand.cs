using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class OwnerAutorizeRequestKeyboardCommand:IKeyboardCommand
{
    private readonly User _tenant;

    public OwnerAutorizeRequestKeyboardCommand(User tenant)
    {
        _tenant = tenant;
    }

    public IReplyMarkup Get()
    {
        return  new InlineKeyboardMarkup(new []
         {
             InlineKeyboardButton.WithCallbackData($"Авторизувати",$"tenant-request-to-authorize-flat-allow {_tenant.Id}"),
             InlineKeyboardButton.WithCallbackData($"Скасувати",$"tenant-request-to-authorize-flat-denied {_tenant.Id}"),
         });
    }
}