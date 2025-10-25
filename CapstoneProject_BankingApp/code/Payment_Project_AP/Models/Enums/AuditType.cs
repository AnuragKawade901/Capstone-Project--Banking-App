namespace Payment_Project_AP.Models.Enums
{
    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        LoginSuccess = 4,
        LoginFailed = 5,
        Logout = 6,
        Approve = 7,
        Reject = 8
    }
}
