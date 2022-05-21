namespace KomunalkaUA.Domain.Services;

public class ReplyTextService
{
    public static string GetGasStateMessage()
    {
        return "Введіть номер рахунку і показник газу формат:" +
               "\n'Номер рахунку' 'показник'";
    }
    public static string GetElectricStateMessage()
    {
        return "Введіть номер рахунку і показник води формат:" +
               "\n'Номер рахунку' 'показник'";
    }
    public static string GetWatterStateMessage()
    {
        return "Введіть номер рахунку і показник світла формат:" +
               "\n'Номер рахунку' 'показник':";
    }
}