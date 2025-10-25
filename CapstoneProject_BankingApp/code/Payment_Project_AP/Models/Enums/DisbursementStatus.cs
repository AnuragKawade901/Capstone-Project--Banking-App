namespace Payment_Project_AP.Models.Enums
{
    public enum DisbursementStatus
    {
        Pending,
        Processing,
        Completed,
        PartiallyCompleted, // in case some payments fail
        Failed
    }
}
