using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ModelsBook
{
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public int AuthorId { get; set; }
        public int YearOfPublication { get; set; }
    }
}
