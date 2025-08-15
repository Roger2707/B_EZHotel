namespace EZHotel.Models.Users
{
    public class Staff : User
    {
        public string EmployeeCode { get; set; } = null!;
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
        public string BankAccount { get; set; }
        public string EmergencyContact { get; set; }
        public string Certifications { get; set; }
    }
}
