namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface INotificationService
    {
        Task CreateNotificationAsync(
            string subject, string message, int senderId, string senderIPAddress, int menuId, string eventDesc = "", int parameterColumn0 = 0
            , int parameterColumn1 = 0, int parentColumnId = 0, string referenceValue = "", string refNo = "", int readable = 0, int notifyFor = 0
            , int itemSubGroupId = 0, int supplierId = 0, int styleMasterId = 0, int exportOrderId = 0, int bomMasterId = 0, int bookingId = 0
            , int spoMasterId = 0, int piMasterId = 0);
    }
}
