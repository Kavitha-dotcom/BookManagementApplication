using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.test.Model
{
    public class Owner
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Book> Books { get; set; }
    }
}
