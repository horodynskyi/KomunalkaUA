using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatApprovePreMeterCheckoutsKeyboardCommand:IKeyboardCommand
{
    private readonly long _tenantId;

    public FlatApprovePreMeterCheckoutsKeyboardCommand(long tenantId)
    {
        _tenantId = tenantId;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            InlineKeyboardButton.WithCallbackData("Підтвердити",$"owner-access-pre-meters-checkout {_tenantId}"),
        });
    }
}