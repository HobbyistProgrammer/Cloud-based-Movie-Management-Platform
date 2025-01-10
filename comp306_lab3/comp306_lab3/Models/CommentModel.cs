using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace comp306_lab3.Models
{
    public class CommentModel
    {
        public int Id { get; set; }
        public int Movie_Id { get; set; }
        public string Comment { get; set; }
        public string Comment_Time { get; set; }
        public int Comment_User { get; set; }
    }
}
