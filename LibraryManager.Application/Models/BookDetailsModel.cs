using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Manager.Application.Models
{
    internal class BookDetailsModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public string AuthorName { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
