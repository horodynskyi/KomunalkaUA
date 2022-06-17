using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class FlatAddCardKeyboardCommand:IKeyboardCommand
{
    private int _flatId;
    private string _text;

    public FlatAddCardKeyboardCommand(int flatId, string text)
    {
        _flatId = flatId;
        _text = text;
    }

    public IReplyMarkup Get()
    {
        return new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(_text, $"edididiididid"),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Повернутись", $"flat-detail {_flatId}"),
            }
        });
    }}