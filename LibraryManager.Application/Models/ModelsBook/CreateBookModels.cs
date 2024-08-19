namespace Models.ModelsBook
{
    public class CreateBookModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string ISBN { get; set; }
        public int YearOfPublication { get; set; }
    }
}
