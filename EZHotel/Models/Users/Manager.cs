namespace EZHotel.Models.Users
{
    public class Manager : Staff
    {
        public decimal ApprovalLimit { get; set; }
        public decimal PerformanceBonusRate { get; set; }
        public int AuthorityLevel { get; set; } = 0;
    }
}
