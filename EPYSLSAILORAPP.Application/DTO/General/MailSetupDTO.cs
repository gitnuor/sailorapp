namespace EPYSLSAILORAPP.Application.DTO.General
{
    public class BuyerTeamWiseEmployeeMailSetupDTO
    {
        public string? ToMailId { get; set; }
        public string? CcMailId { get; set; }
        public string? BccMailId { get; set; }
    }

    public class UserEmailInfoDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? EmailPassword { get; set; }
        public string? Designation { get; set; }
        public string? Department { get; set; }
    }
}
