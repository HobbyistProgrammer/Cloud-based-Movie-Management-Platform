using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int Rating { get; set; }
        public int Duration { get; set; }
        public string Release_Date { get; set; }
        public int User_Uploaded { get; set; }
        public string Director { get; set; }
    }
}
