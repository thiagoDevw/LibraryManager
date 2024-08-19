using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsBook
{
    public class BookDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public string AuthorName { get; set; }
        public int AuthorId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
