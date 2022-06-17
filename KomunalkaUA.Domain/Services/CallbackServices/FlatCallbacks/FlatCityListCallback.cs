using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatCityListCallback:IFlatCityListCallback
{
    private readonly string _callback = "flat-city-list";
    private int _flatId;
    private int _cityId;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatCityListCallback(
        IRepository<Flat> flatRepository, 
        IKeyboardService keyboardService)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var updateflat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressThenIncludeCitySpec(_flatId));
        updateflat.Address.CityId = _cityId;
        await _flatRepository.UpdateAsync(updateflat);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressThenIncludeCitySpec(_flatId));
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            $"Місто обрано!",
            replyMarkup:(InlineKeyboardMarkup)_keyboardService.GetKeys(new FlatAddressKeyboardCommand(_flatId,flat.Address))
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=3) 
            return false;
        if (callbackData.Contains(_callback))
        {
            _flatId = Int32.Parse(msg[1]);
            _cityId = Int32.Parse(msg[2]);
            return true;
        }

        return false;
    }
}