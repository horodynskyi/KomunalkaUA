using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;

namespace KomunalkaUA.Domain.Extensions;

public static class PreMeterCheckoutExtensions
{
    public static async Task SetApprovedAsync(this List<PreMeterCheckout> preMeterCheckouts,
        IRepository<PreMeterCheckout> repository)
    {
        foreach (var preCheck in preMeterCheckouts)
        {
            preCheck.IsApproved = true;
            preCheck.StartValue = preCheck.Meter.Value;
            preCheck.Meter.Value = preCheck.EndValue;
            await repository.UpdateAsync(preCheck);
            await repository.SaveChangesAsync();
        }
    }

    public static (string,int) GetPaymentString(this PreMeterCheckout preMeterCheckout)
    {
        string str ="";
        if (preMeterCheckout.Meter.Provider == null)
        {
            return ("provider is null",0);
        }
        if (preMeterCheckout.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Gas)
        {
            str = "<b>Газ:</b> ";
        }
        else if (preMeterCheckout.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Watter)
        {
            str = "<b>Вода:</b>";
        }
        else if (preMeterCheckout.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Electric)
        {
            str = "<b>Світло:</b>";
        }
        
        return ((string, int)) ($"{str}<code>{preMeterCheckout.StartValue} - {preMeterCheckout.EndValue} = {preMeterCheckout.EndValue - preMeterCheckout.StartValue}</code> " + 
                                $"- {(int) ((preMeterCheckout.EndValue - preMeterCheckout.StartValue) * preMeterCheckout.Meter.Provider.Rate)}",
            (preMeterCheckout.EndValue-preMeterCheckout.StartValue)* preMeterCheckout.Meter.Provider.Rate);
    }

    public static Checkout CreateCheckoutsAsync(this List<PreMeterCheckout> preMeterCheckouts,Flat flat)
    {
        var checkout = new Checkout();
        checkout.FlatId = flat.Id;
        checkout.Flat = flat;
        checkout.PaymentSum = flat.Rent;
        foreach (var item in preMeterCheckouts)
        {
            checkout.PaymentSum += (int)((item.EndValue - item.StartValue)*item.Meter.Provider.Rate);
        }
        return checkout;
    }
}