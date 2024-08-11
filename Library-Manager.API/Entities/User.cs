namespace Library_Manager.API.Entities
{
    public class User : BaseEntity
    {
        public User(int id, string name, string email, string password, string phone)
            : base()
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Phone = phone;

            Books = new List<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone {  get; set; }

        //Relacionamento com Book como autor
        public ICollection<Book> Books { get; set; }

        // Relacionamento com Loan
        public ICollection<Loan> Loans { get; set; }
    }
}
