namespace Library_Manager.API.Entities
{
    public class Book : BaseEntity
    {
        public Book() { }
        public Book(int id, string title, string iSBN, int year, int authorId, User author)
        {
            Id = id;
            Title = title;
            ISBN = iSBN;
            Year = year;
            AuthorId = authorId;
            Author = author;
            Loans = new List<Loan>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }

        // Relacionamento com Loan
        public ICollection<Loan> Loans { get; set; }
    }
}
