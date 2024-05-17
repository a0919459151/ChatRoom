using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ChatRoom.GeneralSerivces;

public class AlertService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

    private string IsSuccess => "IsSuccess";
    private string Message => "Message";

    public AlertService(
        IHttpContextAccessor httpContextAccessor,
        ITempDataDictionaryFactory tempDataDictionaryFactory)
    {
        _tempDataDictionaryFactory = tempDataDictionaryFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    public void Success(string message = "成功")
    {
        var tempData = GetTempData();

        tempData[IsSuccess] = true;
        tempData[Message] = message;
    }

    public void Error(string message = "失敗")
    {
        var tempData = GetTempData();

        tempData[IsSuccess] = false;
        tempData[Message] = message;
    }

    public (bool IsSuccess, string Message) GetAlert()
    {
        var tempData = GetTempData();

        var isSuccess = tempData[IsSuccess] as bool? ?? false;
        var message = tempData[Message] as string;
        return (isSuccess, message!);
    }

    private ITempDataDictionary GetTempData()
    {
        return _tempDataDictionaryFactory.GetTempData(_httpContextAccessor.HttpContext);
    }
}