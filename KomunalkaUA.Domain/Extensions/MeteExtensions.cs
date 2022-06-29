using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Extensions;

public static class MeterExtensions
{
    public static (string,int) GetPaymentString(this Meter meter,PreMeterCheckout preMeterCheckout)
    {
        string str ="";
        if (meter.Provider == null)
        {
            return ("provider is null",0);
        }
        if (meter.Provider.MeterTypeId == (int) MeterTypeEnum.Gas)
        {
            str = "<b>Газ:</b> ";
        }
        else if (meter.Provider.MeterTypeId == (int) MeterTypeEnum.Watter)
        {
            str = "<b>Вода:</b>";
        }
        else if (meter.Provider.MeterTypeId == (int) MeterTypeEnum.Electric)
        {
            str = "<b>Світло:</b>";
        }
        
            return ((string, int)) ($"{str}<code>{meter.Value} - {preMeterCheckout.EndValue} = {preMeterCheckout.EndValue - meter.Value}</code> " + 
                                    $"- {(int) ((preMeterCheckout.EndValue - meter.Value) * meter.Provider.Rate)}",
                (preMeterCheckout.EndValue-meter.Value)* meter.Provider.Rate);
    }
}