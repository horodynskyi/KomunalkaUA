using NodaTime;

namespace KomunalkaUA.Domain.Extensions;

public static class NodaTimeExtensions
{
    public static string ToCheckoutDate(this ZonedDateTime time)
    {
        string month = time.Date.Month switch
        {
            1 => "Січень",
            2 => "Лютий",
            3 => "Березень",
            4 => "Квітень",
            5 => "Травень",
            6 => "Червень",
            7 => "Липень",
            8 => "Серпень",
            9 => "Вересень",
            10 => "Жовтень",
            11 => "Листопад",
            12 => "Грудень",
            _ => throw new ArgumentOutOfRangeException()
        };
        return $"{time.Date.Year} {month}";
    }
}