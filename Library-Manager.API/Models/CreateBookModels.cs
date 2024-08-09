namespace Library_Manager.API.Models
{
    public class CreateBookModels
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int yearOfPublication { get; set; }
    }
}
