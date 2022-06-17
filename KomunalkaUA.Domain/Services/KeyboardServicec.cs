using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services;

public class KeyboardServicec
{
  

    public static InlineKeyboardMarkup CreateListFlatInlineKeyboardMarkup(List<Flat> flats)
    {
        var keys = new InlineKeyboardMarkup(
            flats
                .Where(x => x.Address != null)
                .Select(x => InlineKeyboardButton.WithCallbackData($"{x.Address.Street} {x.Address.Building} {x.Address.FlatNumber}",$"flat-detail {x.Id}"))
                .Chunk(2)
            );
        return keys;
    }

    public static InlineKeyboardMarkup CreateFlatDetailInlineKeyboardMarkup(Flat flat)
    {
        return new InlineKeyboardMarkup(
            new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Редагувати квартиру",$"flat-edit {flat.Id}"), 
                    InlineKeyboardButton.WithCallbackData($"Картка оплати",$"flat-card {flat.Id}"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Повернутись до квартир",$"flat-list"),
                }
              
            }
        );
    }

    public static InlineKeyboardMarkup CreateUpdateFlatInlineKeyboardButton(Flat flat)
    {
        return new InlineKeyboardMarkup(
            new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Адрес",$"flat-edit-address {flat.Id}"), 
                    InlineKeyboardButton.WithCallbackData($"Картка оплати",$"flat-cardnumber {flat.Id}"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Видалити квартиру",$"flat-delete {flat.Id}"),
                    InlineKeyboardButton.WithCallbackData($"Додати фото",$"flat-photo-add {flat.Id}")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData($"Повернутись до квартир",$"flat-list")
                }
            }
        );
    }

    public static InlineKeyboardMarkup CreateFlatBackToDetailInlineKeyboardMarkup(Flat flat)
    {
        return new InlineKeyboardMarkup(
            new []
            {
                InlineKeyboardButton.WithCallbackData("Повернутись до квартири",$"flat-detail {flat.Id}"), 
            }
        );
    }
    public static InlineKeyboardMarkup CreateFlatCardInlineKeyboardButton(Flat flat,CardType cardType)
    {
        string text;
        string callBackData;
        switch (cardType)
        {
            case CardType.Add:
                text = "Додати карту";
                callBackData = "flat-card-add";
                break;
            case CardType.Edit:
                text = "Редагувати карту";
                callBackData = "flat-card-edit";
                break;
            case CardType.Delete:
                text = "Видалити карту:";
                callBackData = "flat-card-delete";
                break;
            default: 
                text = "Виникли проблеми!";
                callBackData = "none";
                break;
            
        }
        return new InlineKeyboardMarkup(
            new []
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text,$"{callBackData} {flat.Id}"),
                }
            }
        );
    }


    

    public static ReplyKeyboardMarkup GetStartTenantButtons()
    {
        var keys = new ReplyKeyboardMarkup(
            new []
            {
                new []
                {
                    new KeyboardButton("Авторизувати квартиру"),
                }
            }
        );
        keys.ResizeKeyboard = true;
        keys.Selective = true;
        keys.OneTimeKeyboard = true;
        return keys;
    }
}