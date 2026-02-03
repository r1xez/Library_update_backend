using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_update.DAL.Entities
{
    public class Book
    {
        public int Id { get; set; }


        public string Title { get; set; }


        public string ISBN { get; set; }


        public int PublishYear { get; set; }


        public decimal? Price { get; set; }


        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
