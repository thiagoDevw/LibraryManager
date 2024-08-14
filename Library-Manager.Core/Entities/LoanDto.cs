namespace Library_Manager.Core.Entities
{
    public class LoanDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } // Nome do usuário

        public int BookId { get; set; }
        public string BookTitle { get; set; } // Título do livro

        public DateTime LoanDate { get; set; } // Data do Empréstimo
        public DateTime? ReturnDate { get; set; } //Data da devolução (pode ser nula se ainda não tiver devolvido)

    }
}
