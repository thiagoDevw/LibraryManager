namespace Library_Manager.API.Models
{
    public class CreateLoanModel
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime LoanDate { get; set; }
    }
}
