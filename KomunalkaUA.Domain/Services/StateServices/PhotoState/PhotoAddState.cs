using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices.PhotoState.Interfaces;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.StateServices.PhotoState;

public class PhotoAddState : IPhotoAddState
{
    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var photoId = update.Message.Photo[0].FileId;
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var flatPhoto = new FlatPhoto
        {
            FlatId = flatId,
            PhotoId = photoId
        };
        await client.DeleteMessageAsync(
            update.Message.Chat.Id,
            update.Message.MessageId);
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.AddPhoto)
            return true;
        return false;
    }
}