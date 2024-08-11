namespace Library_Manager.API.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public DateTime LoanDate { get; set; } // Data do Empréstimo
        public DateTime? ReturnDate { get; set; } // Data da devolução (pode ser nula se ainda nao tiver devolvido)
    }
}
