namespace EPYSLSAILORAPP.Domain.Statics
{
    public class AppConstants
    {
        public const string DB_CONNECTION = "DBConnection";
        public const string SYMMETRIC_SECURITY_KEY = "8b327b47-2e48-4116-9134-dcbcd5aff40b";
        public const string CorsOriginsSettingKey = "CorsOriginsSettingKey";
        public const decimal KD_VALUE = 0.00255m;
        public const int PARTIAL_SHIPMENT_ALLOW = 20;
        public const string PI_FILE_PATH = "/Uploads/PI";
        public const string LC_FILE_PATH = "/Uploads/LC";
        public const string CI_FILE_PATH = "/Uploads/CI";
        public const string NEW = "<<--New-->>";
        public const string QUERY_FOLDER = "/App_Data";
        public const string SPECIAL_CHARACTER_TO_REMOVE_IN_UPLOAD = "['<>&]|[\n]{2}";
    }

    public static class UserRoles
    {
        public const string SUPER_USER = "SuperUser";
        public const string ADMIN = "Admin";
        public const string GENERAL = "User";
    }

    public static class StatusConstants
    {
        public const string PENDING = "Pending";
        public const string ACKNOWLEDGE = "Acknowledge";
        public const string PROPOSED = "Proposed";
        public const string ACCEPT = "Accept";
        public const string REJECT = "Reject";
        public const string CANCEL = "Cancel";
        public const string RETURN_PROPOSE_PRICE = "Return Propose Price";
    }
    public static class DbNames
    {
        public const string EPYSLSAILOR = "EPYSLSAILOR";
    }

    public static class DateFormats
    {
        /// <summary>
        /// Default date format
        /// </summary>
        public const string DEFAULT_DATE_FORMAT = "MM/dd/yyyy";

        /// <summary>
        /// Example: 02-Mar-2020
        /// </summary>
        public const string DATE_FORMAT_1 = "dd-MMM-yyyy";
    }

    public static class ContactCategoryNames
    {
        public const string BUYER = "Buyer";
        public const string SUPPLIER = "Supplier";
        public const string FORWARDER = "Forwarder";
        public const string INSURANCE_COMPANY = "Insurance Company";
        public const string NOTIFY_PARTY = "Notify Party";
        public const string SHIPPING_LINE = "Shipping Line";
        public const string LOCAL_AGENT = "Local Agent";
        public const string CLEARING_AGENT = "Clearing Agent";
        public const string CNF = "CnF";
        public const string CARRYING_CONTRACTOR = "Carrying Contractor";
        public const string CUSTOMER = "Customer";
        public const string CONSIGNEE = "Consignee";
        public const string TREADER = "Treader";
        public const string SPINNER = "Spinner";
    }
    public static class EntityTypeValueNameConstants
    {
        public const string Pending = "Pending";
        public const string PARTIALLY_COMPLETE = "Partially Complete";
        public const string COMPLETE = "Complete";
    }

    public static class ContactsName_ShortName
    {
        public const string Name = "Name";
        public const string ShortName = "ShortName";
    }

}
